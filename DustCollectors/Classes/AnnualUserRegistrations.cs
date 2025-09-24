using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
namespace DustCollectors.Classes
{
    [DataContract]
    public class AnnualUserRegistrations
    {
        [DataMember]
        public string year;
        [DataMember]
        public int numUsers;
    }
}