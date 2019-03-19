using System;
using System.Runtime.Serialization;

namespace LightControllerRestService
{
    [DataContract]
    public class Temperature
    {
        [DataMember]
        public DateTime From { get; set; }
        [DataMember]
        public DateTime To { get; set; }
        [DataMember]
        public int Temperatur { get; set; }
    }
}