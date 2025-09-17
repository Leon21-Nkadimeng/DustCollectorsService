using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DustCollectors.Classes;

namespace DustCollectors
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        DustCollectorsDBDataClassesDataContext db = new DustCollectorsDBDataClassesDataContext();

        // checks if a user exists, if they exist, retruns false otherwise adds the user to the database and returns true
        bool IService1.IsReg(SysUser newUser)
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
        // retrieves the details required to start a user session
        UserSessionDetails IService1.GetUserSessionDetails(string email, string password)
        {
            var user = (from u in db.SysUsers
                        where u.EmailAddress.Equals(email) && u.Password.Equals(password)
                        select u).FirstOrDefault();
            if (user == null)
                return null;

            return new UserSessionDetails
            {
                Id = user.Id,
                Name = user.FirstName,
                userType = user.UserType
            };
        }
        UserPersonalDetails IService1.GetUserDetails(int Id)
        {
            var user = (from u in db.SysUsers
                        where u.IsActive.Equals(1) && u.Id.Equals(Id)
                        select u).FirstOrDefault();
            if (user == null)
                return null;
            return new UserPersonalDetails()
            {
                ID = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress,
                PhoneNumber = user.PhoneNumber
            };
        }
        string IService1.GetUserPassword(int UserId)
        {
            var user = (from u in db.SysUsers
                        where u.IsActive.Equals(1) && u.Id.Equals(UserId)
                        select u).FirstOrDefault();
            if (user == null)
                return null;
            return user.Password;
        }
        List<CustomerAddress> IService1.GetCustomerAddresses(int userID)
        {
            dynamic addresses = (from a in db.CustomerDeliveryAddresses
                                 where a.IsActive.Equals(1) && a.CustomerID.Equals(userID)
                                 select a).DefaultIfEmpty();
            if (addresses == null)
                return null;

            List<CustomerAddress> custAddresses = new List<CustomerAddress>();
            foreach(CustomerDeliveryAddress a in addresses)
            {
                if (a != null)
                {
                    custAddresses.Add(new CustomerAddress()
                    {
                        Id=a.Id,
                        RecipientName = a.RecipientName,
                        RecipientPhone = a.RecipientPhone,
                        StreetAddress = a.Street_Address,
                        ComplexOrBuilding = a.ComplexOrBuilding,
                        Suburb = a.Surburb,
                        CityOrTown = a.CityOrTown,
                        Province = a.Province,
                        PostalCode = a.PostalCode
                    });
                }
            }

            return custAddresses;
        }
        bool IService1.InsertAddress(CustomerAddressInsert address)
        {
            var customerAddress = new CustomerDeliveryAddress()
            {
                RecipientName = address.RecipientName,
                RecipientPhone = address.RecipientPhone,
                Street_Address = address.StreetAddress,
                ComplexOrBuilding = address.ComplexOrBuilding,
                Surburb = address.Suburb,
                CityOrTown = address.CityOrTown,
                Province = address.Province,
                PostalCode = address.PostalCode,
                CustomerID = address.CustomerID,
                IsActive = true,
            };
            db.CustomerDeliveryAddresses.InsertOnSubmit(customerAddress);
            try
            {
                db.SubmitChanges();
                return true;
            } catch(Exception e)
            {
                e.GetBaseException();
                return false;
            }
        }

        bool IService1.InsertBrand(Brand newBrand)
        {
            throw new NotImplementedException();
        }

        bool IService1.InsertGender(Gender newGender)
        {
            throw new NotImplementedException();
        }

        bool IService1.InsertShoeColourway(Colourway newColourway)
        {
            throw new NotImplementedException();
        }

        bool IService1.InsertShoeCategory(Category newCategory)
        {
            throw new NotImplementedException();
        }

        bool IService1.InsertShoeSize(ShoeSize newShoeSize)
        {
            throw new NotImplementedException();
        }

        bool IService1.InsertProduct(Product newProduct)
        {
            throw new NotImplementedException();
        }

        CustomerAddress IService1.getCustomerAddress(int customerID)
        {
            var address = (from a in db.CustomerDeliveryAddresses
                           where a.CustomerID.Equals(customerID)
                           select a).FirstOrDefault();

            if (address == null)
                return null;

            return new CustomerAddress() {
                Id = address.Id,
                RecipientName = address.RecipientName,
                RecipientPhone = address.RecipientPhone,
                StreetAddress = address.Street_Address,
                ComplexOrBuilding = address.ComplexOrBuilding,
                Suburb =address.Surburb,
                CityOrTown = address.CityOrTown,
                Province = address.Province,
                PostalCode = address.PostalCode
            };
        }
    }

}
