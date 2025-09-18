using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
namespace DustCollectors.Classes
{
    [DataContract]
    public class ProductDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int BrandID { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int CategoryID { get; set; }
    }
}