using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace DustCollectors.Classes
{
    [DataContract]
    public class ShoeSizeDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string SizeTag { get; set; }
        [DataMember]
        public string System { get; set; }
    }
}