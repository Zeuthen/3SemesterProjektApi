using System;
using System.Runtime.Serialization;

namespace LightControllerRestService
{
    [DataContract]
    public class Weather
    {
        [DataMember]
        public DateTime From { get; set; }
        [DataMember]
        public DateTime To { get; set; }
        [DataMember]
        public string Vejret { get; set; }             
    }
}