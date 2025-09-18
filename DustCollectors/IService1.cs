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
        /** create methods */
        [OperationContract]
        bool IsReg(SysUser user);
        [OperationContract]
        bool InsertAddress(CustomerAddress address);
        [OperationContract]
        bool InsertShoeColourway(ColourwayDTO newColourway);
        [OperationContract]
        bool InsertShoeCategory(CategoryDTO newCategory);
        [OperationContract]
        bool InsertShoeSize(ShoeSizeDTO newShoeSize);
        [OperationContract]
        bool InsertProduct(ProductDTO newProduct);
        [OperationContract]
        bool InsertBrand(BrandDTO newBrand);
        [OperationContract]
        bool InsertGender(GenderDTO newGender);
        [OperationContract]
        bool InsertShoe(ShoeDTO newShoe);
        [OperationContract]
        bool InsertShoeVariant(ShoeVariantDTO newShoeVariant);

        /** retrieval methods */
        [OperationContract]
        UserSessionDetails GetUserSessionDetails(string email, string password);
        [OperationContract]
        UserPersonalDetails GetUserDetails(int Id);
        [OperationContract]
        string GetUserPassword(int UserId);
        [OperationContract]
        List<CustomerAddress> GetCustomerAddresses(int userID);
        [OperationContract]
        CustomerAddress getCustomerAddress(int customerID);
        [OperationContract]
        List<BrandDTO> getBrands(bool isActive);
        [OperationContract]
        List<CategoryDTO> getCategories(bool isAvailable);
        [OperationContract]
        List<ColourwayDTO> getColourways();
        [OperationContract]
        List<GenderDTO> getGenders();
        [OperationContract]
        List<ShoeSizeDTO> getShoeSizes();
        [OperationContract]
        List<SysUserDTO> getUsers(bool isActive);


        /** update methods */
        [OperationContract]
        bool updateUserPassword(int userID, string newPassword);

        /** Delete methods */
        [OperationContract]
        bool deleteAddress(int addressID);
       
    }



}
