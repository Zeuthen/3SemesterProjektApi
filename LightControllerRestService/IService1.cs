#region References

using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

#endregion

namespace LightControllerRestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        #region Lumen

        // CREATE
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "lumen")]
        Lumen AddLumen(Lumen lumen);

        // READ                        
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "lumen")]
        IList<Lumen> GetLumen();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "lumen/{id}")]
        IList<Lumen> GetLumenByID(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "lumen/date/{dateFrom}/to/{dateTo}")]
        IList<Lumen> GetLumenByDate(string dateFrom, string dateTo);

        // UPDATE
        [OperationContract]
        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "lumen/{id}")]
        Lumen UpdateLumen(Lumen lumen, string id);

        // DELETE
        [OperationContract]
        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "lumen/")]
        IList<Lumen> DeleteLumen();

        [OperationContract]
        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "lumen/{id}")]
        IList<Lumen> DeleteLumenByID(string id);

        #endregion

        //Tænd og sluk
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "roomlight/{status}")]
        string AddStatus(string status);

        //Sæt grænsen
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "roomlight/limit/{limit}")]
        string AddLimit(string limit);

        //Sæt Timer
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "roomlight/timer")]
        string AddTimer(setTimer timer);

        //Vejret                                
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "weather")]
        List<Weather> GetWeather();

        //Temperaturen
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "temperature")]
        List<Temperature> GetTemperature();
    }
}