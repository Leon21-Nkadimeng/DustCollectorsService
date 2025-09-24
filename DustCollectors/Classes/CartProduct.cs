using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
namespace DustCollectors.Classes
{
    [DataContract]
    public class CartProduct
    {
        [DataMember]
        public int SizeID { get; set; }
        [DataMember]
        public int QTY { get; set; }
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string size { get; set; }
       
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public string imageURL { get; set; }
        [DataMember]
        public int amountInStock { get; set; }

    }
}