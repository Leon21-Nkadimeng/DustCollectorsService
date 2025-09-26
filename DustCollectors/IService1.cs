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
        //Invoice
        [OperationContract]
        List<Invoice> GetUserInvoices(int userId);
        
        [OperationContract]
        bool deleteInvoice(int inID);

        // user
        [OperationContract]
        bool IsReg(SysUser user);
        [OperationContract]
        bool InsertAddress(CustomerAddress address);
        // product
        [OperationContract]
        bool InsertProductColourway(ColourwayDTO newColourway);
        [OperationContract]
        bool InsertProductCategory(CategoryDTO newCategory);
        [OperationContract]
        bool InsertBrand(BrandDTO newBrand);
        [OperationContract]
        bool InsertGender(GenderDTO newGender);
        [OperationContract]
        bool InsertProductSizes(int id, List<ProductSizeDTO> productSizes);
        [OperationContract]
        string InsertProductAndSizes(ProductDTO newProduct, List<ProductSizeDTO> productSizes);
        [OperationContract]
        bool AddItemToCart(int userId, int sizeId, int qty);
       
        
        [OperationContract]
        int createInvoice(int userID, int addressID, decimal subtotal, decimal vat, decimal deliveryfee, decimal grandTot);
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
        List<BrandDTO> getBrands(int isActive);
        [OperationContract]
        List<CategoryDTO> getCategories(bool isAvailable);
        [OperationContract]
        List<ColourwayDTO> getColourways();
        [OperationContract]
        List<GenderDTO> getGenders();
        [OperationContract]
        List<ProductSizeDTO> getProductSizes(int ProductID);
        [OperationContract]
        List<SysUserDTO> getUsers(bool isActive);
        [OperationContract]
        List<DisplayProdCatalog> getActiveProducts();
        [OperationContract]
        List<DisplayProdCatalog> getInactiveProducts();
        [OperationContract]
        List<DisplayProdCatalog> getAllProducts();
        [OperationContract]
        List<ProdForTblManagement> getProductsForManagementTbl(int availability);
        [OperationContract]
        ProductDetail getProductDetails(int prodId);
        [OperationContract]
        ProductDTO getProductInfoForEditing(int prodId);
        [OperationContract]
        BrandDTO getBrand(int id);
        [OperationContract]
        GenderDTO getGenderCategory(int id);
        [OperationContract]
        ColourwayDTO getColourway(int id);
        [OperationContract]
        CategoryDTO getCategory(int id);
        [OperationContract]
        List<RegisteredUsers> getDailyRegisteredUsers();
        [OperationContract]
        List<MonthlyUsers> getMonthlyRegisteredUsers();
        [OperationContract]
        List<AnnualUserRegistrations> getAnnualUserRegistrations();
        [OperationContract]
        List<CartProduct> getCartProducts(int userId);
        [OperationContract]
        List<DisplayProdCatalog> getProductsByCategory(int categoryId);
        [OperationContract]
        List<DisplayProdCatalog> getProductsByGender(int genderId);
        [OperationContract]
        List<DisplayProdCatalog> getProductsByColourway(int colourwayId);
        
       
        /** update methods */
        [OperationContract]
        bool updateBrand(BrandDTO updatedBrand);
        [OperationContract]
        bool updateCategory(CategoryDTO updatedCategory);
        [OperationContract]
        bool updateColourway(ColourwayDTO updatedColourway);
        [OperationContract]
        bool updateGenderCategory(GenderDTO updatedGenderCategory);
        [OperationContract]
        bool updateUserPassword(int userID, string newPassword);
        [OperationContract]
        string updateProductAndSizes(ProductDTO product, List<ProductSizeDTO> newSizes, List<ProductSizeDTO> editedSizes);
        [OperationContract]
       bool updateUserPersonalDetails(UserPersonalDetails details);
        [OperationContract]
        bool updateCartItem(int sizeId, int userId, int qty);
        [OperationContract]
        bool activateGenderCategory(int id);
        [OperationContract]
        bool updateQTYS(List<CartProduct> cartItems);
        /** Delete methods */
        [OperationContract]
        bool deleteAddress(int addressID);
        [OperationContract]
        bool deleteProductSizes(List<int> ids);
        [OperationContract]
        bool deleteBrand(int id);
        [OperationContract]
        bool deleteCategory(int id);
        [OperationContract]
        bool deleteColourway(int id);
        [OperationContract]
        bool deteGenderCategory(int id);
        [OperationContract]
        bool removeItemFromCart(int userID, int sizeID);

        [OperationContract]
        bool deleteCartItems(int userId);
        [OperationContract]
        bool removeShoeSize(int sizeId);





      
    }



}
