using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
namespace DustCollectors
{
    [DataContract]
    public class ColourwayDTO
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        
    }
}