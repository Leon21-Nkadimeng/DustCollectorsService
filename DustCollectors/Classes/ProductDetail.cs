using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
namespace DustCollectors.Classes
{
    [DataContract]
    public class ProductDetail
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        
        [DataMember]
        public string MainImgURL { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public List<ProductSizeDTO> sizes { get; set; }

    }
}