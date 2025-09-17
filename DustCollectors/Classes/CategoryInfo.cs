using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
namespace DustCollectors.Classes
{
    [DataContract]
    public class CategoryInfo
    {
        [DataMember]
        public string name {get; set;}
        [DataMember]
        public int superCategoryID { get; set; }
        [DataMember]
        public bool isAvailable { get; set; }

    }
}