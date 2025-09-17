using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace DustCollectors.Classes
{
    [DataContract]
    public class ShoeSizeInfo
    {
        [DataMember]
        public string sizeTag { get; set; }
        [DataMember]
        public string system { get; set; }
    }
}