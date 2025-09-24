using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
namespace DustCollectors.Classes
{
    [DataContract]
    public class ProdForTblManagement
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
        public string CategoryName { get; set; }
        [DataMember]
        public string GenderAgeCategory { get; set; }
        [DataMember]
        public int AmountInStock { get; set; }
        [DataMember]
        public bool isActive { get; set; }
        [DataMember]
        public string MainImgURL { get; set; }
        [DataMember]
        public DateTime DateAdded { get; set; }
    }
}