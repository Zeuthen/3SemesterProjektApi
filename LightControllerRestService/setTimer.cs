using System;
using System.Runtime.Serialization;

namespace LightControllerRestService
{
    [DataContract]
    public class setTimer
    {
        [DataMember]
        public DateTime TimerStart { get; set; }

        [DataMember]
        public DateTime TimerEnd { get; set; }
                                                 
                            
    }
}