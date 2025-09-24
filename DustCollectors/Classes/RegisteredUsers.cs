using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
namespace DustCollectors.Classes
{
    [DataContract]
    public class RegisteredUsers
    {
        [DataMember]
        public int numUsers;
        [DataMember]
        public DateTime date;
    }
}