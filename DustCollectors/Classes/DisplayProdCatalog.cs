using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization; 
namespace DustCollectors.Classes
{
    [DataContract]
    public class DisplayProdCatalog
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string BrandName { get; set; }
        
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public string colourway { get; set;  }
        [DataMember]
        public string gender { get; set; }
        [DataMember]
        public int categoryId { get; set; }
        [DataMember]
        public int genderId { get; set; }
        [DataMember]
        public int colourwayId { get; set; }
        [DataMember]
        public int BrandID { get; set; }
        [DataMember]
        public string mainImageURL { get; set; }
    }
}