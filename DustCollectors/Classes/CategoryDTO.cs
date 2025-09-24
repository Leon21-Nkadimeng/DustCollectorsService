using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
namespace DustCollectors.Classes
{
    [DataContract]
    public class CategoryDTO
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name {get; set;}
      
        [DataMember]
        public bool isAvailable { get; set; }
        [DataMember]
        public DateTime dateAdded { get; set; }
        [DataMember]
        public bool isActive { get; set; }

    }
}