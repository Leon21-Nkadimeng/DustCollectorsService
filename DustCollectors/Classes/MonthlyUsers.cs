using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
namespace DustCollectors.Classes
{
    [DataContract]
    public class MonthlyUsers
    {
        [DataMember]
        public string Month;
        [DataMember]
        public int numUsers;
    }
}