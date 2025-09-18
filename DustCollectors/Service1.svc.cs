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
        /** Create methods */
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
        bool IService1.InsertAddress(CustomerAddress address)
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
            }
            catch (Exception e)
            {
                e.GetBaseException();
                return false;
            }
        }

        bool IService1.InsertBrand(BrandDTO newBrand)
        {
            var brand = (from b in db.Brands
                         where b.Name.Equals(newBrand.name) && b.Description.Equals(newBrand.description) && b.MainLogoURL.Equals(newBrand.mailLogoURL)
                         select b).FirstOrDefault();
            if (brand != null)
                return false;
            db.Brands.InsertOnSubmit(new Brand() { 
                                    Name = newBrand.name,
                                    Description = newBrand.description,
                                    MainLogoURL = newBrand.mailLogoURL,
                                    IsActive = newBrand.isActive,
                                    DateAdded = DateTime.Now
                                });
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

        bool IService1.InsertGender(GenderDTO newGender)
        {
            var gender = (from g in db.Genders
                        where g.Name.Equals(newGender.name) && g.AgeGroup.Equals(newGender.ageGroup)
                        select g).FirstOrDefault();
            if (gender != null)
                return false;
            db.Genders.InsertOnSubmit(new Gender()
            {
                Name = newGender.name,
                AgeGroup = newGender.ageGroup
            });
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

        bool IService1.InsertShoeColourway(ColourwayDTO newColourway)
        {
            var colourway = (from c in db.Colourways
                            where c.Name.Equals(newColourway.name)
                            select c).FirstOrDefault();
            if (colourway != null)
                return false;
            db.Colourways.InsertOnSubmit(new Colourway()
            {
               Name = newColourway.name,
               DateAdded = DateTime.Now
            });
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

        bool IService1.InsertShoeCategory(CategoryDTO newCategory)
        {
            var category = (from c in db.Categories
                         where c.Name.Equals(newCategory.name) && c.IsAvailable.Equals(newCategory.isAvailable)
                         select c).FirstOrDefault();
            if (category != null)
                return false;
            db.Categories.InsertOnSubmit(new Category()
            {
                Name = newCategory.name,
                IsAvailable = newCategory.isAvailable,
                DateAdded = DateTime.Now
            });
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

        bool IService1.InsertShoeSize(ShoeSizeDTO newShoeSize)
        {
            var size = (from s in db.ShoeSizes
                            where s.SizeTag.Equals(newShoeSize.SizeTag) && s.System.Equals(newShoeSize.System)
                            select s).FirstOrDefault();
            if (size != null)
                return false;
            db.ShoeSizes.InsertOnSubmit(new ShoeSize()
            {
                SizeTag = newShoeSize.SizeTag,
                System = newShoeSize.System
            });
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

        bool IService1.InsertProduct(ProductDTO newProduct)
        {
            var prod = (from p in db.Products
                        where p.Name.Equals(newProduct.Name) && p.BrandID.Equals(newProduct.BrandID) && p.Description.Equals(newProduct.Description) && newProduct.CategoryID.Equals(p.CategoryID)
                        select p).FirstOrDefault();
            if (prod != null)
                return false;
            db.Products.InsertOnSubmit(new Product()
            {
                Name = prod.Name,
                BrandID = prod.BrandID,
                Description = prod.Description,
                DateAdded = DateTime.Now,
                CategoryID = prod.CategoryID
            });
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

        bool IService1.InsertShoe(ShoeDTO newShoe)
        {
            var shoe = (from s in db.Shoes
                        where s.ProductID.Equals(newShoe.ProductID) && s.GenderId.Equals(newShoe.GenderID) && s.ColourWayID.Equals(newShoe.ColourwayID)
                        select s).FirstOrDefault();
            if (shoe != null)
                return false;
            db.Shoes.InsertOnSubmit(new Shoe()
            {
                ProductID = newShoe.ProductID,
                DateAdded = DateTime.Now,
                GenderId = newShoe.GenderID,
                Price = newShoe.Price,
                DiscountPercentage = newShoe.DiscountPercentage,
                ColourWayID = newShoe.ColourwayID,
                MainImgURL = newShoe.MainImgURL
            });
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
        bool IService1.InsertShoeVariant(ShoeVariantDTO newShoeVariant)
        {
            var shoe = (from s in db.ShoeVariants
                        where s.SizeID.Equals(newShoeVariant.SizeID) && s.ShoeId.Equals(newShoeVariant.ShoeId)
                        select s).FirstOrDefault();
            if (shoe != null)
                return false;
            db.ShoeVariants.InsertOnSubmit(new ShoeVariant()
            {
                SizeID = newShoeVariant.SizeID,
                ShoeId = newShoeVariant.ShoeId,
                QTYInStock = newShoeVariant.AmountInStock,
                IsAvailable = newShoeVariant.IsAvailable,
           
                DateAdded = DateTime.Now

            });
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


        /** Retrieval methods */
        List<Product> getProds()
        {
            dynamic prods = (from p in db.Products
                             select p).DefaultIfEmpty();

            if (prods == null) return null;
            List<Product> products = new List<Product>();
            foreach(Product p in prods)
            {
                if(p != null)
                {
                    products.Add(new Product()
                    {
                        Name = p.Name,
                        BrandID = p.BrandID,
                        CategoryID = p.CategoryID,
                        Description = p.Description,
                        
                    });
                }
            }

            return products;
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
        List<BrandDTO> IService1.getBrands(bool isActive)
        {
            dynamic brands = (from b in db.Brands
                              where b.IsActive.Equals(isActive)
                              select b).DefaultIfEmpty();
            if (brands == null) return null;
            List<BrandDTO> brandList = new List<BrandDTO>();
            foreach(Brand b in brands)
            {
                if(b != null)
                {
                    brandList.Add(new BrandDTO() {Id=b.Id, name = b.Name, description = b.Description, mailLogoURL = b.MainLogoURL, isActive = b.IsActive });
                }
            }

            return brandList;
        }

        List<CategoryDTO> IService1.getCategories(bool isAvailable)
        {
            dynamic categories = (from c in db.Categories
                             where c.IsAvailable.Equals(1)
                              select c).DefaultIfEmpty();
            if (categories == null) return null;
            List<CategoryDTO> categoryList = new List<CategoryDTO>();
            foreach (Category c in categories)
            {
                if (c != null)
                {
                    categoryList.Add(new CategoryDTO() { id = c.Id, name = c.Name });
                }
            }

            return categoryList;
        }

        List<ColourwayDTO> IService1.getColourways()
        {
            dynamic colourways = (from c in db.Colourways
                                  select c).DefaultIfEmpty();
            if (colourways == null) return null;
            List<ColourwayDTO> colourwaysList = new List<ColourwayDTO>();
            foreach (Colourway c in colourways)
            {
                if (c != null)
                {
                    colourwaysList.Add(new ColourwayDTO() { id = c.Id, name = c.Name});
                }
            }

            return colourwaysList;
        }

        List<GenderDTO> IService1.getGenders()
        {
            dynamic genders = (from g in db.Genders
                               select g).DefaultIfEmpty();
            if (genders == null) 
                return null;
            List<GenderDTO> gendersList = new List<GenderDTO>();
            foreach (Gender g in genders)
            {
                if (g != null)
                {
                    gendersList.Add(new GenderDTO() { Id = g.Id, name = g.Name, ageGroup = g.AgeGroup });
                }
            }

            return gendersList;
        }

        List<ShoeSizeDTO> IService1.getShoeSizes()
        {
            dynamic shoeSizes = (from s in db.ShoeSizes
                                select s).DefaultIfEmpty();
            if (shoeSizes == null) return null;
            List<ShoeSizeDTO> sizesList = new List<ShoeSizeDTO>();
            foreach (ShoeSize s in shoeSizes)
            {
                if (s != null)
                {
                    sizesList.Add(new ShoeSizeDTO() { Id = s.Id, SizeTag = s.SizeTag, System = s.System });
                }
            }

            return sizesList;
        }

        List<SysUserDTO> IService1.getUsers(bool isActive)
        {
            dynamic users = (from u in db.SysUsers
                             where u.IsActive.Equals(isActive)
                             select u).DefaultIfEmpty();

            if (users == null)
                return null;

            List<SysUserDTO> sysUsers = new List<SysUserDTO>();

            foreach(SysUser u in users)
            {
                if(u != null)
                {
                    sysUsers.Add(new SysUserDTO() { FirstName = u.FirstName, 
                                    LastName = u.LastName, 
                                    EmailAddress = u.EmailAddress, 
                                    PhoneNumber = u.PhoneNumber, 
                                    UserType = u.UserType, 
                                    DateRegistered = u.DateRegistered, 
                                    IsActive = u.IsActive});
                }    
            }

            return sysUsers;

        }




        /** Update methods */
        bool IService1.updateUserPassword(int userID, string newPassword)
        {
            var user = (from u in db.SysUsers
                        where u.Id.Equals(userID) && u.IsActive.Equals(1)
                        select u).FirstOrDefault();
            if (user == null)
                return false;

            user.Password = newPassword;
            try
            {
                db.SubmitChanges();
                return true;
            } catch(Exception e){
                e.GetBaseException();
               
                return false;
            }
        }

        /** Delete methods */
        bool IService1.deleteAddress(int addressID)
        {
            var address = (from a in db.CustomerDeliveryAddresses
                           where a.IsActive.Equals(1)
                           select a).FirstOrDefault();
            if(address != null)
            {

                db.CustomerDeliveryAddresses.DeleteOnSubmit(address);
                try
                {
                    db.SubmitChanges();
                    return true;
                } catch(Exception e)
                {
                    return false;
                }
            } else
            {
                return false;
            }
        }

        
    }

}
