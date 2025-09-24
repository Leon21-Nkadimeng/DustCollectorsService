using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
namespace DustCollectors.Classes
{
    [Serializable]
    [DataContract]
    public class ProductSizeDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int ProductID { get; set; }
        [DataMember]
        public int AmountInStock { get; set; }
        [DataMember]
        public bool IsAvailable { get; set; }
        [DataMember]
        public string SizeTag { get; set; }
        [DataMember]
        public string System { get; set; }
    }
}