using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
namespace DustCollectors.Classes
{
    public class UserPersonalDetails
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string EmailAddress { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
    }
}