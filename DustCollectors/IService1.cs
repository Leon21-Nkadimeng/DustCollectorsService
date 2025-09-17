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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        
        [OperationContract]
        bool IsReg(SysUser user);
        [OperationContract]
        UserSessionDetails GetUserSessionDetails(string email, string password);
        [OperationContract]
        UserPersonalDetails GetUserDetails(int Id);
        [OperationContract]
        string GetUserPassword(int UserId);
        [OperationContract]
        List<CustomerAddress> GetCustomerAddresses(int userID);

        [OperationContract]
        bool InsertAddress(CustomerAddressInsert address);
        [OperationContract]
        CustomerAddress getCustomerAddress(int customerID);
        bool InsertBrand(Brand newBrand);
        [OperationContract]
        bool InsertGender(Gender newGender);
        [OperationContract]
        bool InsertShoeColourway(Colourway newColourway);
        [OperationContract]
        bool InsertShoeCategory(Category newCategory);
        [OperationContract]
        bool InsertShoeSize(ShoeSize newShoeSize);

        [OperationContract]
        bool InsertProduct(Product newProduct);
    }



}
