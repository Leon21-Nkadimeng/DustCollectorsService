using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using UtilityMethods;
using DTOClasses;
namespace DustCollectors
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        DustCollectorsDBDataClassesDataContext db = new DustCollectorsDBDataClassesDataContext();
        // uses register user function from the Helpers to add a user of type customer
        bool IService1.RegisterCustomer(SysUser newCustomer)
        {
            // determine the user type

            newCustomer.UserType = "customer";
            return Helpers.RegisterUser(newCustomer);
            
            
        }
        // uses register user function from the Helpers to add a user of type admin admin
        bool IService1.AddAdmin(SysUser newAdmin)
        {
            // determine the user type
            newAdmin.UserType = "admin";
            return Helpers.RegisterUser(newAdmin);
        }

        UserDetails IService1.GetActiveUsersDetails(string email, string password)
        {
            var user = (from u in db.SysUsers
                        where u.EmailAddress.Equals(email) && u.Password.Equals(password) && u.IsActive.Equals(1)
                        select u).FirstOrDefault();

            if (user == null)
                return null;

            return new UserDetails(user.FirstName, user.LastName, user.EmailAddress, user.PhoneNumber);
        }
        /*
                /*
                List<Shoe> IService1.GetActiveShoes()
                {
                    dynamic shoes = (from s in db.Shoes
                                     where s.IsAvailable.Equals(1)
                                     select s).DefaultIfEmpty();

                    if (shoes == null)
                        return null;

                    List<Shoe> shoesList = new List<Shoe>();
                    foreach(Shoe s in shoes)
                    {
                        shoesList.Add(s);
                    }

                    return shoesList;
                }*/

        /*

        // shoe catalog
        List<Shoe> IService1.GetActiveShoesInventory()
        {
            dynamic activeShoes = (from s in db.ShoeInventories
                                  where s.IsAvailable.Equals(1)
                                  select s).DefaultIfEmpty();
            if (activeShoes == null)
                return null;
            List<ShoeInventory> shoes = new List<ShoeInventory>();

            foreach (ShoeInventory c in shoes)
            {
                if (c != null)
                    shoes.Add(c);
            }

            return shoes;
        }
        */
        // shoe
        bool IService1.InsertShoe(ShoeInfoDTO newShoe)
        {
            var shoe = (from s in db.Shoes
                        where s.ProductID.Equals(newShoe.productID) 
                        select s).FirstOrDefault();
            /*
            if (shoe != null)
                return false;
            
            var shoeToInsert = new Shoe
            {
                ProductID = newShoe.productID,
                SizeID = newShoe.sizeID,
                Price = newShoe.price,
                DiscountPercentage = newShoe.discountPercentage,
                QTYInStock = newShoe.qtyInStock,
                IsAvailable = newShoe.isAvailable,
                Gender = newShoe.gender,
                ColourWayID = newShoe.colourwayID,
                Weight = newShoe.weight,
                WeightSystem = newShoe.weightMeasurement,
                MainImgURL = newShoe.mainIMGURL,
                DateActivated = newShoe.dateActivated,
                DateAdded = DateTime.Now.Date
            };

            db.Shoes.InsertOnSubmit(shoeToInsert);
            try
            {
                db.SubmitChanges();
                return true;
            } catch(Exception e)
            {
                return false;
            }
       */
            return true;  
        }
        List<CatalogDisplayShoe> IService1.GetActiveShoesCatalog()
        {
            dynamic shoes = (from s in db.Shoes
                             where s.IsAvailable.Equals(1)
                             select s).DefaultIfEmpty();

            if (shoes == null)
                return null;
            List<CatalogDisplayShoe> shoeCatalog = new List<CatalogDisplayShoe>();
            foreach(Shoe shoe in shoes)
            {
                if(shoe != null)
                {
                    CatalogDisplayShoe s = new CatalogDisplayShoe(shoe.Id, shoe.Product.Name, shoe.Product.Name, shoe.Price, shoe.DiscountPercentage, shoe.MainImgURL, shoe.Gender, shoe.Product.Category.Name);
                    shoeCatalog.Add(s);
                }
            }

            return shoes;
        }
        bool IService1.InsertProduct(ProductDTO newProduct)
        {
            var product = (from p in db.Products
                        where p.Name.Equals(newProduct.getName()) && p.Description.Equals(newProduct.getDescription())
                        select p).FirstOrDefault();

            if (product != null)
                return false;
            // setup the shoe brand
            var brand = (from b in db.Brands
                         where b.Id.Equals(newProduct.getBrandID())
                         select b).FirstOrDefault();
            if (brand == null)
                return false;


            var prod = new Product
            {
                Name = newProduct.getName(),
                BrandID = newProduct.getBrandID(),
                Description = newProduct.getDescription(),
                MainImgURL = newProduct.getMainIMGURL(),
                CategoryID = newProduct.getCategoryID()
            };

            

           
           

            // newShoe.DateAdded = DateTime.Now;
            db.Products.InsertOnSubmit(prod);
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.GetBaseException();
                return false;
            }

        }
        // colourways
        bool IService1.InsertColourWay(Colourway newColourway)
        {
            var colourway = (from c in db.Colourways
                            where c.Name.Equals(newColourway.Name)
                            select c).FirstOrDefault();
            if (colourway != null)
                return false;
            newColourway.DateAdded = DateTime.Now;
            db.Colourways.InsertOnSubmit(newColourway);
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        // shoe sizes
        bool IService1.InsertShoeSize(ShoeSize newShoeSize)
        {
            var shoeSize = (from s in db.ShoeSizes
                         where s.SizeTag.Equals(newShoeSize.SizeTag) && s.System.Equals(newShoeSize.System)
                         select s).FirstOrDefault();
            if (shoeSize != null) 
                return false;

            db.ShoeSizes.InsertOnSubmit(newShoeSize);
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            
        }
        // brands
        bool IService1.InsertShoeBrand(Brand newBrand)
        {
            var brand = (from b in db.Brands
                         where b.Name.Equals(newBrand.Name) && b.Description.Equals(newBrand.Description)
                         select b).FirstOrDefault();
            if (brand != null) return false;

            db.Brands.InsertOnSubmit(newBrand);
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        List<Brand> IService1.GetActiveShoeBrands()
        {
            dynamic brands = (from b in db.Brands
                              where b.IsActive.Equals(1)
                              select b).DefaultIfEmpty();
            if (brands == null)
                return null;
            List<Brand> shoeBrands = new List<Brand>();

            foreach (Brand b in brands)
            {
                if (b != null)
                    shoeBrands.Add(b);
            }

            return shoeBrands;
        }

        // shoe categories
        bool IService1.InsertShoeCategory(Category newShoeCategory)
        {
            var category = (from c in db.Categories
                            where c.Name.Equals(newShoeCategory.Name)
                            select c).FirstOrDefault();
            if (category != null) return false;
          //  newShoeCategory.IsAvailable = true;
            db.Categories.InsertOnSubmit(newShoeCategory);
            
            
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        List<Category> IService1.GetAvailableShoeCategories()
        {
            dynamic categories = (from c in db.Categories
                                  where c.IsAvailable.Equals(1)
                                  select c).DefaultIfEmpty();
            if (categories == null)
                return null;
            List<Category> shoeCategories = new List<Category>();

            foreach (Category c in categories)
            {
                if (c != null)
                    shoeCategories.Add(c);
            }

            return shoeCategories;
        }

    }

   





}
