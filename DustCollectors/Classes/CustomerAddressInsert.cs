using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DustCollectors.Classes;

namespace DustCollectors.Classes
{
    [DataContract]
    public class CustomerAddressInsert
    {
       
        [DataMember]
        public string RecipientName { get; set; }
        [DataMember]
        public string RecipientPhone { get; set; }
        [DataMember]
        public string StreetAddress { get; set; }
        [DataMember]
        public string Suburb { get; set; }
        [DataMember]
        public string ComplexOrBuilding { get; set; }
        [DataMember]
        public string CityOrTown { get; set; }
        [DataMember]
        public string Province { get; set; }
        [DataMember]
        public string PostalCode { get; set; }
        [DataMember]
        public int CustomerID { get; set; }
    }
}