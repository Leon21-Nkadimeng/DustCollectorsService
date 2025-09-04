using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DustCollectors;
using UtilityClasses;
namespace UtilityMethods
{
    public class Helpers 
    {
        
        private static DustCollectorsDBDataClassesDataContext db = new DustCollectorsDBDataClassesDataContext();
        public static bool RegisterUser(SysUser newUser)
        {
            
            // check if the user already exists
            var user = (from u in db.SysUsers
                        where u.EmailAddress.Equals(newUser.EmailAddress) && u.Password.Equals(newUser.Password)
                        select u).FirstOrDefault();


            if (user != null)
                return false;

            newUser.IsActive = true;
            newUser.DateRegistered = DateTime.Now;

            // try to insert the new user
            db.SysUsers.InsertOnSubmit(newUser);
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                e.GetBaseException();
                return false;
            }
        }
        [OperationContract]

        public static bool isValidGender(string gender)
        {
            if (gender.Equals(Genders.MEN) || gender.Equals(Genders.WOMEN) || gender.Equals(Genders.BOYS) || gender.Equals(Genders.GIRLS))
                return true;
            return false;
        }
    }
}
    
