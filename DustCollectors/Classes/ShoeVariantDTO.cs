using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
namespace DustCollectors.Classes
{
    [DataContract]
    public class ShoeVariantDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int SizeID { get; set; }
        [DataMember]
        public int ShoeId { get; set; }
        [DataMember]
        public int AmountInStock { get; set; }
        [DataMember]
        public bool IsAvailable { get; set; }
        [DataMember]
        public DateTime DateActivated { get; set; }
    }
}