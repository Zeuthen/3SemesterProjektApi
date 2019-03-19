using System;
using System.Runtime.Serialization;

namespace LightControllerRestService
{
    [DataContract]
    public class Lumen
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int Amount { get; set; }

        [DataMember]
        public DateTime Date { get; set; }
    }
}