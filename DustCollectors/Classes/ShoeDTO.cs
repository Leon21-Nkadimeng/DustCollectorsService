using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
namespace DustCollectors.Classes
{
    [DataContract]
    public class ShoeDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int ProductID { get; set; }
        [DataMember]
        public int GenderID { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public decimal DiscountPercentage { get; set; }
        [DataMember]
        public int ColourwayID { get; set; }
        [DataMember]
        public string MainImgURL { get; set; }
    }
}