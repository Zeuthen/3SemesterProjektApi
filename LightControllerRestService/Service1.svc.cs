#region References

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

#endregion

namespace LightControllerRestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        //DMI / YR vejrudsigt
        private const string Uri = "http://www.yr.no/sted/Danmark/Sjælland/Roskilde/varsel.xml";

        //Connectionstring til database
        private const string ConnectionString =
                "Data Source=laentver.database.windows.net;Initial Catalog=3Semester;Persist Security Info=True;User ID=MLicious;Password=3Semester"
            ;

        public List<Lumen> Lumens = new List<Lumen>();

        public Lumen AddLumen(Lumen lumen)
        {
            const string sqlquery = "INSERT INTO [Lumen] (Amount) OUTPUT Inserted.ID VALUES (@Amount)";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlquery, connection))
                {
                    //command.Parameters.AddWithValue("@ID", lumen.ID);
                    command.Parameters.AddWithValue("@Amount", lumen.Amount);
                    //command.Parameters.AddWithValue("@Date", lumen.Date);         

                    //Returns the first column (ID) of the first row in the insert 
                    lumen.ID = (int)command.ExecuteScalar();
                    return lumen;
                }
            }
        }

        public IList<Lumen> GetLumen()
        {
            const string sqlquery = "SELECT * FROM [Lumen] ORDER BY Date";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlquery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Lumen> lumenList = new List<Lumen>();
                        while (reader.Read())
                        {
                            //Lumen lumen = ReadLumen(reader);
                            Lumen lumen = new Lumen();
                            lumen.ID = reader.GetInt32(0);
                            lumen.Amount = reader.GetInt32(1);
                            lumen.Date = reader.GetDateTime(2);

                            lumenList.Add(lumen);
                        }
                        return lumenList;
                    }
                }
            }
        }

        public IList<Lumen> GetLumenByID(string id)
        {
            string sqlquery = "SELECT * FROM [Lumen] WHERE ID=" + int.Parse(id) + " ORDER BY ID";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlquery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Lumen> lumenList = new List<Lumen>();
                        while (reader.Read())
                        {
                            //Lumen lumen = ReadLumen(reader);
                            Lumen lumen = new Lumen();
                            lumen.ID = reader.GetInt32(0);
                            lumen.Amount = reader.GetInt32(1);
                            lumen.Date = reader.GetDateTime(2);

                            lumenList.Add(lumen);
                        }
                        return lumenList;
                    }
                }
            }
        }

        public IList<Lumen> GetLumenByDate(string dateFrom, string dateTo)
        {
            //Hvordan den skal laves?         
            throw new NotImplementedException();
        }

        public Lumen UpdateLumen(Lumen lumen, string id)
        {
            string sqlquery = "UPDATE [Lumen] SET Amount=@Amount, Date=@Date WHERE ID=" + int.Parse(id);

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlquery, connection))
                {
                    //command.Parameters.AddWithValue("@ID", lumen.ID);
                    command.Parameters.AddWithValue("@Amount", lumen.Amount);
                    command.Parameters.AddWithValue("@Date", lumen.Date);
                    //if (command.ExecuteNonQuery() > 0)
                    //{
                    //    //success
                    //    return lumen;
                    //}
                    //return null;

                    //If row affected ( row updated )
                    return command.ExecuteNonQuery() > 0 ? lumen : null;
                }
            }
        }

        public IList<Lumen> DeleteLumen()
        {
            string sqlquery = "DELETE FROM [Lumen]";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlquery, connection))
                {
                    //if (command.ExecuteNonQuery() > 0)
                    //{
                    //    //success
                    //    GetLumen()
                    //}
                    //return null;

                    //If row affected ( row deleted )
                    return command.ExecuteNonQuery() > 0 ? GetLumen() : null;
                }
            }
        }

        public IList<Lumen> DeleteLumenByID(string id)
        {
            string sqlquery = "DELETE FROM [Lumen] WHERE ID=" + int.Parse(id);

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlquery, connection))
                {
                    //if (command.ExecuteNonQuery() > 0)
                    //{
                    //    //success
                    //    GetLumen()
                    //}
                    //return null;

                    //If row affected ( row updated )                        
                    return command.ExecuteNonQuery() > 0 ? GetLumenByID(id) : null;
                }
            }
        }

        public string AddStatus(string status)
        {
            if (status == "true" || status == "false")
            {
                string sqlquery = "INSERT INTO RoomLight (status) VALUES (@status)";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlquery, connection))
                    {
                        command.Parameters.AddWithValue("@status", bool.Parse(status) ? 1 : 0);
                        command.ExecuteNonQuery();
                        switch (status)
                        {
                            case "true":
                                return "Status er ændret til tændt";
                            case "false":
                                return "Status er ændret til slukket";
                            default:
                                return "Status forbliver uændret";
                        }
                    }
                }
            }
            return null;
        }

        public string AddLimit(string limit)
        {
            string sqlquery = "INSERT INTO RoomLight (limit) VALUES (@limit)";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlquery, connection))
                {
                    command.Parameters.AddWithValue("@limit", int.Parse(limit));
                    return command.ExecuteNonQuery() > 0
                        ? "Grænsen er sat til " + limit
                        : "Grænsen forbliver uændret";
                }
            }
        }

        public string AddTimer(setTimer timer)
        {
            string sqlquery = "INSERT INTO RoomLight (TimerStart, TimerEnd) VALUES (@TimerStart, @TimerEnd)";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlquery, connection))
                {
                    command.Parameters.AddWithValue("@TimerStart", timer.TimerStart);
                    command.Parameters.AddWithValue("@TimerEnd", timer.TimerEnd);
                    return command.ExecuteNonQuery() > 0
                        ? "Timer er sat fra " + timer.TimerStart + " til " + timer.TimerEnd
                        : "Timer forbliver uændret";
                }
            }
        }

        public List<Weather> GetWeather()
        {
            return GetWeatherFromXml().Result;

        }

        public async Task<List<Weather>> GetWeatherFromXml()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(Uri);
                if (response.IsSuccessStatusCode)
                {
                    string xmlString = await response.Content.ReadAsStringAsync();

                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(xmlString);

                    XmlNodeList namesXmlList = xmlDocument.GetElementsByTagName("time");
                    IList<Weather> weatherList = new List<Weather>();
                    for (int i = 0; i < namesXmlList.Count; i++)
                    {
                        XmlNode namesNode = namesXmlList[i];

                        string fromStr = namesNode.Attributes["from"].Value;
                        DateTime from = DateTime.Parse(fromStr);

                        string toStr = namesNode.Attributes["to"].Value;
                        DateTime to = DateTime.Parse(toStr);

                        XmlNode weatherNode = namesNode.SelectSingleNode("symbol");
                        string weather = weatherNode.Attributes["name"].Value;   

                        Weather w = new Weather
                        {
                            From = from,
                            To = to,
                            Vejret = weather
                        };
                        weatherList.Add(w);
                    }
                    return weatherList.ToList();
                }
                return null;
            }
        }

        public List<Temperature> GetTemperature()
        {
            return GetTemperatureFromXml().Result;
        }

        public async Task<List<Temperature>> GetTemperatureFromXml()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(Uri);
                if (response.IsSuccessStatusCode)
                {
                    string xmlString = await response.Content.ReadAsStringAsync();

                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(xmlString);

                    XmlNodeList namesXmlList = xmlDocument.GetElementsByTagName("time");
                    IList<Temperature> temperatureList = new List<Temperature>();
                    for (int i = 0; i < namesXmlList.Count; i++)
                    {
                        XmlNode namesNode = namesXmlList[i];
                        string fromStr = namesNode.Attributes["from"].Value;
                        DateTime from = DateTime.Parse(fromStr);
                        string toStr = namesNode.Attributes["to"].Value;
                        DateTime to = DateTime.Parse(toStr);

                        XmlNode temperatureNode = namesNode.SelectSingleNode("temperature");
                        string temperatureString = temperatureNode.Attributes["value"].Value;
                        int temperature = int.Parse(temperatureString);

                        Temperature t = new Temperature
                        {
                            From = from,
                            To = to,
                            Temperatur = temperature
                        };
                        temperatureList.Add(t);
                    }
                    return temperatureList.ToList();
                }
                return null;
            }
        }
    }
}