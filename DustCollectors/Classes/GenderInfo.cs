using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
namespace DustCollectors.Classes
{
    [DataContract]
    public class GenderInfo
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string ageGroup { get; set; }
    }
}