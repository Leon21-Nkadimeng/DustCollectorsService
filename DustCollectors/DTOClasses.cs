using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Enums;

namespace DTOClasses
{
    [DataContract]
    public class ProductDTO
    {
        [DataMember]
        private string name;
        [DataMember]
        private int brandID;
        [DataMember]
        private string description;
        [DataMember]
        private string mainImgURL;
        [DataMember]
        private int categoryID;
        public ProductDTO(string name, int brandID, string description, string mainImgURL, int categoryID)
        {
            this.name = name;
            this.brandID = brandID;
            this.description = description;
            this.mainImgURL = mainImgURL;
            this.categoryID = categoryID;
        }
        public string getName()
        {
            return name;
        }
        public int getBrandID()
        {
            return brandID;
        }
        public string getDescription()
        {
            return description;
        }
        public string getMainIMGURL()
        {
            return mainImgURL;
        }
        public int getCategoryID()
        {
            return categoryID;
        }
    }

    // user details to be sent to the front end when the user logs in
    [DataContract]
    public class UserDetails
    {
        [DataMember]
        private string name;
        [DataMember]
        private string surname;
        [DataMember]
        private string email;
        [DataMember]
        private string phoneNumber;


        public UserDetails(string name, string surname, string email, string phoneNumber)
        {
            this.name = name;
            this.surname = surname;
            this.email = email;
            this.phoneNumber = phoneNumber;
        }

        // accessors
        [OperationContract]
        public string getFirstName()
        {
            return name;
        }
        [OperationContract]
        public string getSurname()
        {
            return surname;
        }
        [OperationContract]
        public string getEmail()
        {
            return email;

        }
        [OperationContract]
        public string getPhoneNumber()
        {
            return phoneNumber;
        }
    }
    
    [DataContract]
    public class ShoeInfoDTO
    {
        [DataMember]
        public int productID { get; }
        [DataMember]
        public int sizeID { get; }
        [DataMember]
        public decimal price { get; }
        [DataMember]
        public decimal discountPercentage { get; }
        [DataMember]
        public int qtyInStock { get; }
        [DataMember]
        public bool isAvailable { get; }
        [DataMember]
        public string gender { get; }
        [DataMember]
        public int colourwayID { get; }
        [DataMember]
        public decimal weight { get; }
        [DataMember]
        public string weightMeasurement { get; }
        [DataMember]
        public string mainIMGURL { get; }
        /*TODO: set date added in service method*/
        [DataMember]
        public DateTime dateActivated { get; }
        
        public ShoeInfoDTO(int prodID, int sizeID, decimal price, decimal discount, int qty, bool isAvailable, string gender, int colourwayID, decimal weight, string weightSystem, string mainImgURL)
        {
            productID = prodID;
            this.sizeID = sizeID;
            this.price = price;
            discountPercentage = discount;
            qtyInStock = qty;
            this.isAvailable = isAvailable;
            this.gender = gender;
            this.colourwayID = colourwayID;
            this.weight = weight;
            weightMeasurement = weightSystem;
           mainIMGURL = mainImgURL;
            if (this.isAvailable)
               dateActivated = DateTime.Now.Date;

        }

    }

    
    [DataContract]
    public class CatalogDisplayShoe
    {
        [DataMember]
        public int shoeID { get; }
        [DataMember]
        public string shoeName { get;  }
       
        [DataMember]
        public string brandName { get; }
        [DataMember]
        public decimal price { get; }
        [DataMember]
        public decimal discountPrecentage { get; }
        [DataMember]
        public string MainImgURL { get; }
        [DataMember]
        public string gender { get; }
        [DataMember]
        public string category { get; }
        public CatalogDisplayShoe(int shoeID, string name, string brandName, decimal price, decimal discountPercentage, string imageURL, string gender, string category)
        {
            this.shoeID = shoeID;
            shoeName = name;
            this.brandName = brandName;
            this.price = price;
            this.discountPrecentage = discountPercentage;
            MainImgURL = imageURL;
            this.gender = gender;
            this.category = category;
        }
    }

}