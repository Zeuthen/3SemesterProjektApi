using System;
using System.Runtime.Serialization;

namespace LightControllerRestService
{
    [DataContract]
    public class RoomLight
    {
        [DataMember]
        public DateTime TimerFrom { get; set; }

        [DataMember]
        public DateTime TimerTo { get; set; }

        [DataMember]
        public int UpperLimit { get; set; }

        [DataMember]
        public int LowerLimit { get; set; }

        [DataMember]
        public bool Status { get; set; }

    }
}