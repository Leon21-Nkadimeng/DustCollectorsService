using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DTOClasses;
using UtilityMethods;
using Enums;

namespace DustCollectors
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        
        [OperationContract]
        bool RegisterCustomer(SysUser newCustomer);
        [OperationContract]
        bool AddAdmin(SysUser newAdmin);

        [OperationContract]
        UserDetails GetActiveUsersDetails(string email, string password);
        /*
                // shoe catalog
                [OperationContract]
                List<ShoeInventory> GetActiveShoesCatalog();
        */
        /** retrieval methods */
        
        [OperationContract]
        List<CatalogDisplayShoe> GetActiveShoesCatalog();
        [OperationContract]
        List<Category> GetAvailableShoeCategories();

        /**insert methods */
        // product
        [OperationContract]
         bool InsertProduct(ProductDTO newProduct);
        //shoe
        [OperationContract]
        bool InsertShoe(ShoeInfoDTO newShoe);
        [OperationContract]
        bool InsertColourWay(Colourway newColourway);
        // shoe sizes
        [OperationContract]
        bool InsertShoeSize(ShoeSize newShoeSize);
        // brands
        [OperationContract]
        bool InsertShoeBrand(Brand newBrand);
        [OperationContract]
        List<Brand> GetActiveShoeBrands();
        // shoe categories
        [OperationContract]
        bool InsertShoeCategory(Category newShoeCategory);
       

    }
    
    
}
