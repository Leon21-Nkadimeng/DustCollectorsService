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
        DustCollectorsDBDataClassesDataContext dbWrite = new DustCollectorsDBDataClassesDataContext();
        /** Create methods */

        //INVOICE MANAGEMENT
        public int createInvoice(int userID, decimal subtotal, decimal vat, decimal deliveryfee, decimal grandTot)
        {
            var invoice = new Invoice
            {
                Id = userID,
                Date = DateTime.Now,
                Subtotal = subtotal,
                VAT = vat,
                DeliveryFee = deliveryfee,
                Total = grandTot,
                Status = "Pending"
            };

            db.Invoices.InsertOnSubmit(invoice);
            db.SubmitChanges();

            return invoice.Id;

        }

        /*
            public List<Invoice> GetUserInvoices(int userID)
            {
                var invoices = (from i in db.Invoices
                                where i.UserID.Equals(userID)
                                select i).ToList();

                return invoices;
            } */

        public List<InvoiceDTO> GetUserInvoices(int userID)
        {
            var invoices = (from i in db.Invoices
                            where i.Id == userID
                            select new InvoiceDTO
                            {
                                InvoiceID = i.Id,
                                InvoiceDate = i.Date,
                                TotalAmount = i.Total,
                                Status = i.Status
                            }).ToList();

            return invoices;
        }


        public bool deleteInvoice(int inID)
        {
            var invoice = db.Invoices.FirstOrDefault(i => i.Id.Equals(inID));

            if (invoice != null)
            {
                db.Invoices.DeleteOnSubmit(invoice);
                db.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        /*
        public Invoice getInvoiceByID(int inID)
        {
            return db.Invoices.FirstOrDefault(i => i.InvoiceID.Equals(inID));
        } */

        public InvoiceDTO getInvoiceByID(int inID)
        {
            return db.Invoices
                     .Where(i => i.Id == inID)
                     .Select(i => new InvoiceDTO
                     {
                         InvoiceID = i.Id,
                         UserID = i.Id,
                         InvoiceDate = i.Date,
                         Subtotal = i.Subtotal,
                         VAT = i.VAT,
                         DeliveryFee = i.DeliveryFee,
                         TotalAmount = i.Total,
                         Status = i.Status
                     })
                     .FirstOrDefault();
        }






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

        bool IService1.InsertProductColourway(ColourwayDTO newColourway)
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

        bool IService1.InsertProductCategory(CategoryDTO newCategory)
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

        bool IService1.InsertProductSizes(int id, List<ProductSizeDTO> productSizes)
        {
            foreach(ProductSizeDTO size in productSizes)
            {
                db.ProductSizes.InsertOnSubmit(new ProductSize() { 
                                                ProductID = id,
                                                SizeSystem = size.System,
                                                SizeTag = size.SizeTag,
                                                AmountInStock = size.AmountInStock,
                                                IsAvailable = size.IsAvailable});
            }
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

        string IService1.InsertProductAndSizes(ProductDTO newProduct, List<ProductSizeDTO> productSizes)
        {
           
            var prod = (from p in db.Products
                        where p.Name.Equals(newProduct.Name) && p.BrandID.Equals(newProduct.BrandID) && p.Description.Equals(newProduct.Description) && newProduct.CategoryID.Equals(p.CategoryID) && p.ColourwayID.Equals(newProduct.ColourwayID) && p.GenderID.Equals(newProduct.GenderID) 
                        select p).FirstOrDefault();
            if (prod != null)
                return "Product already exists";
            var prodToInsert = new Product()
            {
                Name = newProduct.Name,
                BrandID = newProduct.BrandID,
                Description = newProduct.Description,
                DateAdded = DateTime.Now,
                Price = newProduct.Price,
                IsAvailable = newProduct.isActive,
                CategoryID = newProduct.CategoryID,
                ColourwayID = newProduct.ColourwayID,
                GenderID = newProduct.GenderID,
                MainImgURL = newProduct.MainImgURL
            };
            dbWrite.Products.InsertOnSubmit(prodToInsert);
            List<ProductSize> sizes = new List<ProductSize>();
            foreach (var s in productSizes)
            {
                if (s != null)
                {
                    sizes.Add(new ProductSize()
                    {
                        SizeTag = s.SizeTag,
                        SizeSystem = s.System,
                        IsAvailable = s.IsAvailable,
                        AmountInStock = s.AmountInStock,
                        Product = prodToInsert
                    });
                }

            }
            try
            {
                foreach (ProductSize s in sizes)
                    dbWrite.ProductSizes.InsertOnSubmit(s);
                
                
                dbWrite.SubmitChanges();
                return "product inserted successfully";
            }
            catch (Exception e)
            {
                e.GetBaseException();
                //Console.WriteLine(e.GetBaseException().Message);
                return e.GetBaseException().Message;
            }
        }

        bool IService1.AddItemToCart(int userId, int sizeId, int qty)
        {
            
            var amountInStock = (from a in db.ProductSizes
                                 where a.Id.Equals(sizeId)
                                 select a.AmountInStock).FirstOrDefault();
            if (qty > amountInStock)
                return false;

            var cartItem = (from c in db.Carts
                            where c.SizeID.Equals(sizeId) && c.UserID.Equals(userId)
                            select c).FirstOrDefault();

            if (cartItem == null)
            {
                db.Carts.InsertOnSubmit(new Cart() { SizeID = sizeId, QTY = qty, UserID = userId });
            } 
            else
            {
                if (cartItem.QTY + qty <= amountInStock)
                    cartItem.QTY += qty;

            }
            try
            {
                db.SubmitChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

        /*
        int IService1.createInvoice(int userID, int addressID,  decimal subtotal, decimal vat, decimal deliveryfee, decimal grandTot)
        {
            var invoice = new Invoice
            {
                CustomerID = userID,
                Date = DateTime.Now,
                Subtotal = subtotal,
                VAT = vat,
                DeliveryFee = deliveryfee,
                Total = grandTot,
                Status = "Pending",
                ShippingAddressID = addressID
            };
           
            db.Invoices.InsertOnSubmit(invoice);
      
            db.SubmitChanges();

            return invoice.Id;

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

        List<ProdForTblManagement> IService1.getProductsForManagementTbl(int availability)
        {
            
            dynamic prods = null;
            

            if(availability == 0)
            {
                // return inactive ones
                prods = (from p in db.Products
                         where p.IsAvailable.Equals(0)
                         select p).DefaultIfEmpty();
            } 
            else if(availability == 1)
            {
                // only return active ones
                prods = (from p in db.Products
                         where p.IsAvailable.Equals(1)
                         select p).DefaultIfEmpty();
            } 
            else if(availability == 2)
            {
                // return all
                prods = (from p in db.Products
                         select p).DefaultIfEmpty();
            }

            if (prods == null)
                return null;
            List<ProdForTblManagement> products = new List<ProdForTblManagement>();
            foreach(Product p in prods)
            {
                var sizes = (from s in db.ProductSizes
                             where s.ProductID.Equals(p.Id)
                             select s.AmountInStock).Sum();
                

                products.Add(new ProdForTblManagement() { 
                    Id = p.Id,
                    Name = p.Name,
                    BrandName = p.Brand.Name,
                    Price = p.Price,
                    CategoryName = p.Category.Name,
                    GenderAgeCategory = p.Gender.Name + " (" +p.Gender.AgeGroup+")",
                    AmountInStock = sizes,
                    isActive = p.IsAvailable,
                    DateAdded = p.DateAdded,
                    MainImgURL = p.MainImgURL
                });
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
        List<BrandDTO> IService1.getBrands(int isActive)
        {
            dynamic brands = null;
            if (isActive == 0)
            {
                brands = (from b in db.Brands
                                  where b.IsActive.Equals(0)
                                  select b).DefaultIfEmpty();
            } else if(isActive == 1)
            {
                brands = (from b in db.Brands
                                  where b.IsActive.Equals(1)
                                  select b).DefaultIfEmpty();
            } else
            {
                brands = (from b in db.Brands
                                  select b).DefaultIfEmpty();
            }
            if (brands == null) return null;
            List<BrandDTO> brandList = new List<BrandDTO>();
            foreach(Brand b in brands)
            {
                if(b != null)
                {
                    brandList.Add(new BrandDTO() {Id=b.Id, name = b.Name, description = b.Description, mailLogoURL = b.MainLogoURL, isActive = b.IsActive, dateAdded = b.DateAdded });
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

        List<ProductSizeDTO> IService1.getProductSizes(int ProductID)
        {
            dynamic shoeSizes = (from s in db.ProductSizes
                                where s.ProductID == ProductID
                                select s).DefaultIfEmpty();
            if (shoeSizes == null) return null;
            List<ProductSizeDTO> sizesList = new List<ProductSizeDTO>();
            foreach (ProductSize s in shoeSizes)
            {
                if (s != null)
                {
                    sizesList.Add(new ProductSizeDTO() {Id=s.Id, ProductID = s.ProductID, SizeTag = s.SizeTag, System = s.SizeSystem, AmountInStock = s.AmountInStock, IsAvailable = s.IsAvailable });
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
        List<DisplayProdCatalog> IService1.getActiveProducts()
        {
            dynamic prods = (from p in db.Products
                             where p.IsAvailable.Equals(1)
                             select p).DefaultIfEmpty();
            if (prods == null)
                return null;
            List<DisplayProdCatalog> products = new List<DisplayProdCatalog>();
            foreach(Product p in prods)
            {
                if(p != null)
                {
                    products.Add(new DisplayProdCatalog() { 
                                Id = p.Id,
                                Name = p.Name,
                                BrandID = p.BrandID,
                                BrandName = p.Brand.Name,
                                Price = p.Price,
                                colourway = p.Colourway.Name,
                                gender = p.Gender.Name + " (" + p.Gender.AgeGroup+")",
                                categoryId = p.CategoryID,
                                genderId = p.GenderID,
                                colourwayId = p.ColourwayID,
                                mainImageURL = p.MainImgURL
                    });
                }
            }
            return products;
        }
        List<DisplayProdCatalog> IService1.getInactiveProducts()
        {
            dynamic prods = (from p in db.Products
                             where p.IsAvailable.Equals(0)
                             select p).DefaultIfEmpty();
            if (prods == null)
                return null;
            List<DisplayProdCatalog> products = new List<DisplayProdCatalog>();
            foreach (Product p in prods)
            {
                if (p != null)
                {
                    products.Add(new DisplayProdCatalog()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        BrandID = p.BrandID,
                        BrandName = p.Brand.Name,
                        Price = p.Price,
                        colourway = p.Colourway.Name,
                        gender = p.Gender.Name + " (" + p.Gender.AgeGroup + ")",
                        categoryId = p.CategoryID,
                        genderId = p.GenderID,
                        colourwayId = p.ColourwayID,
                        mainImageURL = p.MainImgURL
                    });
                }
            }
            return products;
        }
      
        List<DisplayProdCatalog> IService1.getAllProducts()
        {
            dynamic prods = (from p in db.Products
                      
                             select p).DefaultIfEmpty();
            if (prods == null)
                return null;
            List<DisplayProdCatalog> products = new List<DisplayProdCatalog>();
            foreach (Product p in prods)
            {
                if (p != null)
                {
                    products.Add(new DisplayProdCatalog()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        BrandID = p.BrandID,
                        BrandName = p.Brand.Name,
                        Price = p.Price,
                        colourway = p.Colourway.Name,
                        gender = p.Gender.Name + " (" + p.Gender.AgeGroup + ")",
                        categoryId = p.CategoryID,
                        genderId = p.GenderID,
                        colourwayId = p.ColourwayID,
                        mainImageURL = p.MainImgURL
                    });
                }
            }
            return products;
        }
        ProductDetail IService1.getProductDetails(int prodId)
        {
            var prod = (from p in db.Products
                        where p.Id.Equals(prodId)
                        select p).FirstOrDefault();
            if(prod == null)
                return null;
            List<ProductSizeDTO> sizesToAdd = new List<ProductSizeDTO>();
            dynamic prodSizes = (from s in db.ProductSizes
                             where s.ProductID.Equals(prodId) && s.IsAvailable.Equals(1)
                             select s).DefaultIfEmpty();
            if(prodSizes != null)
            {
                foreach(ProductSize s in prodSizes)
                {
                    if(s != null)
                    {
                        sizesToAdd.Add(new ProductSizeDTO()
                        {
                            Id = s.Id,
                            SizeTag = s.SizeTag,
                            System = s.SizeSystem,
                            AmountInStock = s.AmountInStock,
                            ProductID = prod.Id
                        });
                    }
                }
            }
            return new ProductDetail { 
                Id = prod.Id,
                Name = prod.Name,
                Price = prod.Price,
                MainImgURL = prod.MainImgURL,
                Description = prod.Description,
                sizes = sizesToAdd
            };
        }

        ProductDTO IService1.getProductInfoForEditing(int prodId)
        {
            var prod = (from p in db.Products
                        where p.Id.Equals(prodId)
                        select p).FirstOrDefault();
            if (prod == null)
                return null;

            return new ProductDTO() { 
                Id = prod.Id,
                Name = prod.Name,
                BrandID = prod.BrandID,
                Description = prod.Description,
                Price = prod.Price,
                CategoryID = prod.CategoryID,
                ColourwayID = prod.ColourwayID,
                GenderID = prod.GenderID,
                isActive = prod.IsAvailable,
                MainImgURL = prod.MainImgURL
            };
        }
        BrandDTO IService1.getBrand(int id)
        {
            var brand = (from b in db.Brands
                         where b.Id.Equals(id)
                         select b).FirstOrDefault();
            if(brand == null)
                return null;

            return new BrandDTO() { 
                Id = brand.Id,
            name = brand.Name,
            mailLogoURL = brand.MainLogoURL,
            description = brand.Description,
            isActive = brand.IsActive,
            dateAdded = brand.DateAdded
            };

        }

        GenderDTO IService1.getGenderCategory(int id)
        {
            var genderCategory = (from g in db.Genders
                         where g.Id.Equals(id)
                         select g).FirstOrDefault();
            if (genderCategory == null)
                return null;

            return new GenderDTO()
            {
                Id = genderCategory.Id,
                name = genderCategory.Name,
               ageGroup = genderCategory.AgeGroup
            };

        }

        ColourwayDTO IService1.getColourway(int id)
        {
            var colourway = (from c in db.Colourways
                                  where c.Id.Equals(id)
                                  select c).FirstOrDefault();
            if (colourway == null)
                return null;

            return new ColourwayDTO()
            {
                id = colourway.Id,
                name = colourway.Name,
                dateAdded = colourway.DateAdded
            };

        }
        CategoryDTO IService1.getCategory(int id)
        {
            var category = (from c in db.Categories
                             where c.Id.Equals(id)
                             select c).FirstOrDefault();
            if (category == null)
                return null;

            return new CategoryDTO()
            {
                id = category.Id,
                name = category.Name,
                isAvailable = category.IsAvailable,
                dateAdded =category.DateAdded
            };

        }
        List<RegisteredUsers> IService1.getDailyRegisteredUsers()
        {
            var result = (from u in db.SysUsers
                          group u by u.DateRegistered into regUsersGroup
                          select new { 
                            DateRegistered = regUsersGroup.Key,
                            UsersCount = regUsersGroup.Count()
                          }).DefaultIfEmpty();   
            if (result == null)
                return null;
            List<RegisteredUsers> regusers = new List<RegisteredUsers>();
            int i = 0;
            foreach (var group in result)
            {
                if (i == 90f)
                    break;
                regusers.Add(new RegisteredUsers()
                {
                    date = group.DateRegistered,
                    numUsers = group.UsersCount
                });
                i++;
            }
            return regusers;

        }
        List<MonthlyUsers> IService1.getMonthlyRegisteredUsers()
        {
            
            var result = (from u in db.SysUsers
                          group u by u.DateRegistered.Month into regUsersGroup
                          select new
                          {
                              Month = regUsersGroup.Key.ToString(),
                              
                              UsersCount = regUsersGroup.Count()
                          }).DefaultIfEmpty();
            if (result == null)
                return null;
            List<MonthlyUsers> regusers = new List<MonthlyUsers>();
            int i = 0;
            foreach (var group in result)
            {
                if (i > 12)
                    break;
                regusers.Add(new MonthlyUsers()
                {
                    Month = group.Month.ToString(),
                    numUsers = group.UsersCount
                });
                i++;
            }
            return regusers;
        }
        List<AnnualUserRegistrations> IService1.getAnnualUserRegistrations()
        {
            var result = (from u in db.SysUsers
                          group u by u.DateRegistered.Year into regUsersGroup
                          select new
                          {
                              Year = regUsersGroup.Key.ToString(),

                              UsersCount = regUsersGroup.Count()
                          }).DefaultIfEmpty();
            if (result == null)
                return null;
            List<AnnualUserRegistrations> regusers = new List<AnnualUserRegistrations>();
           
            foreach (var group in result)
            {
               
                regusers.Add(new AnnualUserRegistrations()
                {
                    year = group.Year.ToString(),
                    numUsers = group.UsersCount
                });
            }
            return regusers;
        }
        List<DisplayProdCatalog> IService1.getProductsByCategory(int categoryId)
        {
            dynamic prods = null;
            if (categoryId < 0) { 
                prods = (from p in db.Products
                         where p.IsAvailable.Equals(1)
                         select p).DefaultIfEmpty();
            } 
            else
            {
                prods = (from p in db.Products
                         where p.CategoryID.Equals(categoryId) && p.IsAvailable.Equals(1)
                         select p).DefaultIfEmpty();
            }
             

            if(prods == null)
            {
                return null;
            }
            List<DisplayProdCatalog> products = new List<DisplayProdCatalog>();
            foreach(Product p in prods)
            {
                if(p != null)
                {
                    products.Add(new DisplayProdCatalog()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        BrandName = p.Brand.Name,
                        Price = p.Price,
                        colourway = p.Colourway.Name,
                        gender = p.Gender.Name + " (" + p.Gender.AgeGroup + ")",
                        categoryId = p.CategoryID,
                        genderId = p.GenderID,
                        colourwayId = p.ColourwayID ,
                        BrandID= p.BrandID,
                        mainImageURL = p.MainImgURL
                    });
                   
                }
            }

            return products;
        }

        List<DisplayProdCatalog> IService1.getProductsByGender(int genderId) { 
            dynamic prods = (from p in db.Products
                             where p.GenderID.Equals(genderId) && p.IsAvailable.Equals(1)
                             select p).DefaultIfEmpty();

            if(prods == null)
            {
                return null;
            }
            List<DisplayProdCatalog> products = new List<DisplayProdCatalog>();
            foreach(Product p in prods)
            {
                if(p != null)
                {
                    products.Add(new DisplayProdCatalog()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        BrandName = p.Brand.Name,
                        Price = p.Price,
                        colourway = p.Colourway.Name,
                        gender = p.Gender.Name + " (" + p.Gender.AgeGroup + ")",
                        categoryId = p.CategoryID,
                        genderId = p.GenderID,
                        colourwayId = p.ColourwayID ,
                        BrandID = p.BrandID,
                        mainImageURL = p.MainImgURL
                    });
                   
                }
}

return products;
        }
       
        List<DisplayProdCatalog> IService1.getProductsByColourway(int colourwayId)
        {
            dynamic prods = (from p in db.Products
                             where p.ColourwayID.Equals(colourwayId) && p.IsAvailable.Equals(1)
                             select p).DefaultIfEmpty();

            if (prods == null)
            {
                return null;
            }
            List<DisplayProdCatalog> products = new List<DisplayProdCatalog>();
            foreach (Product p in prods)
            {
                if (p != null)
                {
                    products.Add(new DisplayProdCatalog()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        BrandName = p.Brand.Name,
                        Price = p.Price,
                        colourway = p.Colourway.Name,
                        gender = p.Gender.Name + " (" + p.Gender.AgeGroup + ")",
                        categoryId = p.CategoryID,
                        genderId = p.GenderID,
                        colourwayId = p.ColourwayID,
                        BrandID = p.BrandID,
                        mainImageURL = p.MainImgURL
                    });

                }
            }

            return products;
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
        string IService1.updateProductAndSizes(ProductDTO product, List<ProductSizeDTO> newSizes, List<ProductSizeDTO> editedSizes)
        {
            var prodToEdit = (from p in db.Products
                              where p.Id.Equals(product.Id)
                              select p).FirstOrDefault();
            if (prodToEdit == null)
                return "Product does not exist";
          
            // edit the product
           
                prodToEdit.Name = product.Name;
            
                prodToEdit.MainImgURL = product.MainImgURL;
        
                prodToEdit.IsAvailable = product.isActive;
        
                prodToEdit.Price = product.Price;
         
                prodToEdit.ColourwayID = product.ColourwayID;
         
                prodToEdit.CategoryID = (product.CategoryID);
        
                prodToEdit.IsAvailable = product.isActive;


            // insert new sizes
            foreach (ProductSizeDTO size in newSizes)
            {
                if (size == null)
                    continue;
                var x = (from s in db.ProductSizes
                         where s.SizeSystem.Equals(size.System) && s.SizeTag.Equals(size.SizeTag) && s.ProductID.Equals(product.Id)
                         select s).FirstOrDefault();
                if (x != null)
                    continue;
                db.ProductSizes.InsertOnSubmit(new ProductSize
                {
                    ProductID = prodToEdit.Id,
                    SizeSystem = size.System,
                    SizeTag = size.SizeTag,
                    AmountInStock = size.AmountInStock,
                    IsAvailable = size.IsAvailable
                });
            }

            // edit sizes
            foreach(ProductSizeDTO size in editedSizes)
            {
                if(size != null)
                {
                    var prodSize = (from s in db.ProductSizes
                                    where s.Id.Equals(size.Id)
                                    select s).FirstOrDefault();

                    if(prodSize != null)
                    {
                        if (!prodSize.IsAvailable.Equals(size.IsAvailable))
                            prodSize.IsAvailable = size.IsAvailable;
                        if (!prodSize.AmountInStock.Equals(size.AmountInStock))
                            prodSize.AmountInStock = size.AmountInStock;
                        if (!prodSize.SizeTag.Equals(size.SizeTag))
                            prodSize.SizeTag = size.SizeTag;
                        if (!prodSize.SizeSystem.Equals(size.System))
                            prodSize.SizeSystem = size.System;
                    }
                }
            }
           
            try
            {
                db.SubmitChanges();
                return "Products and sizes edited successfully";
            } catch(Exception e)
            {
                e.GetBaseException();
                return e.GetBaseException().Message;
            }
        }

     
        bool IService1.updateBrand(BrandDTO updatedBrand)
        {
            var brand = (from b in db.Brands
                         where b.Id.Equals(updatedBrand.Id)
                         select b).FirstOrDefault();
            if (brand == null)
                return false;

            //if (!brand.Name.Equals(updatedBrand.name))
                brand.Name = updatedBrand.name;
            //if (!brand.Description.Equals(updatedBrand.description))
                brand.Description = updatedBrand.description;
            //if (!brand.MainLogoURL.Equals(updatedBrand.mailLogoURL))
                brand.MainLogoURL = updatedBrand.mailLogoURL;
            //if (!brand.IsActive.Equals(updatedBrand.isActive))
                brand.IsActive = updatedBrand.isActive;
            if (!brand.IsActive)
            {
                dynamic prods = (from p in db.Products
                                 where p.BrandID.Equals(brand.Id)
                                 select p).DefaultIfEmpty();
                if (prods != null)
                {
                    foreach (Product p in prods)
                    {
                        if (p != null)
                            p.IsAvailable = false;
                    }
                }
            }
            try
            {
                db.SubmitChanges();
                return true;
            } catch(Exception ex)
            {
                return false;
            }

        }
      
        bool IService1.updateCategory(CategoryDTO updatedCategory)
        {
            var category = (from c in db.Categories
                         where c.Id.Equals(updatedCategory.id)
                         select c).FirstOrDefault();
            if (category == null)
                return true;

           // if (!category.Name.Equals(updatedCategory.name))
                category.Name = updatedCategory.name;
           
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
      
        bool IService1.updateColourway(ColourwayDTO updatedColourway)
        {
            var colorway = (from c in db.Colourways
                         where c.Id.Equals(updatedColourway.id)
                         select c).FirstOrDefault();
            if (colorway == null)
                return true;
          //  if (!colorway.Name.Equals(updatedColourway.name))
                colorway.Name = updatedColourway.name;

            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        bool IService1.updateGenderCategory(GenderDTO updatedGenderCategory)
        {
            var genderCategory = (from g in db.Genders
                         where g.Id.Equals(updatedGenderCategory.Id)
                         select g).FirstOrDefault();
            if (genderCategory == null)
                return true;
          //  if (!genderCategory.Name.Equals(updatedGenderCategory.name))
                genderCategory.Name = updatedGenderCategory.name;
           // if (!genderCategory.AgeGroup.Equals(updatedGenderCategory.ageGroup))
                genderCategory.AgeGroup = updatedGenderCategory.ageGroup;
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        List<CartProduct> IService1.getCartProducts(int userId)
        {
            dynamic cartProds = (from c in db.Carts
                                 where c.UserID.Equals(userId)
                                 select c).DefaultIfEmpty();
            if (cartProds == null)
                return null;
            List<CartProduct> cartProducts = new List<CartProduct>();
            foreach(Cart c in cartProds)
            {
                if(c != null)
                {
                    cartProducts.Add(new CartProduct
                    {
                        SizeID = c.SizeID,
                        QTY = c.QTY,
                        UserID = c.UserID,
                        name = c.ProductSize.Product.Name,
                        size = c.ProductSize.SizeTag + " (" + c.ProductSize.SizeSystem + ")",
                        Price = c.ProductSize.Product.Price,
                        imageURL = c.ProductSize.Product.MainImgURL,
                        amountInStock = c.ProductSize.AmountInStock
                    });
                }
            }
            return cartProducts;

        }
        bool IService1.updateUserPersonalDetails(UserPersonalDetails details)
        {
            var user = (from u in db.SysUsers
                        where u.Id.Equals(details.ID)
                        select u).FirstOrDefault();

            if (user == null)
                return false;

            try
            {
                user.FirstName = details.FirstName;
                user.LastName = details.LastName;
                user.PhoneNumber = details.PhoneNumber;
                user.EmailAddress = details.EmailAddress;
                db.SubmitChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        bool IService1.updateCartItem(int sizeId, int userId, int qty)
        {
            var cartItem = (from p in db.Carts
                            where p.SizeID.Equals(sizeId) && p.UserID.Equals(userId)
                            select p).FirstOrDefault();
            if (cartItem == null)
                return false;

            if (qty > cartItem.ProductSize.AmountInStock)
                return false;
            cartItem.QTY = qty;

            try
            {
                db.SubmitChanges();
                return true;
            }catch(Exception e)
            {
                return false;
            }
            
        }
        bool IService1.activateGenderCategory(int id)
        {
            var genderCategory = (from g in db.Genders
                                  where g.Id.Equals(id)
                                  select g).FirstOrDefault();

            if (genderCategory == null)
                return false;
            genderCategory.IsAvailable = true;

            try
            {
                db.SubmitChanges();
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }
        bool IService1.updateQTYS(List<CartProduct> cartItems) 
        {
            foreach(CartProduct c in cartItems)
            {
                if(c != null)
                {
                    var cartprod = (from p in db.ProductSizes
                                    where p.Id.Equals(c.SizeID)
                                    select p).FirstOrDefault();
                    if (cartprod != null)
                        cartprod.AmountInStock -= c.QTY;
                }
            }

            try
            {
                db.SubmitChanges();
                return true;
            }catch(Exception e)
            { return false;
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

      
        bool IService1.deleteProductSizes(List<int> ids)
        {
            List<ProductSize> sizesToDelete = new List<ProductSize>();
            foreach(int i in ids)
            {
                var prodSize = (from s in db.ProductSizes
                                where s.Id.Equals(i)
                                select s).FirstOrDefault();
                if (prodSize != null)
                {
                    prodSize.IsAvailable = false;
                }
            }
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
      
        bool IService1.deleteBrand(int id)
        {
            var brand = (from b in db.Brands
                                  where b.Id.Equals(id)
                                  select b).FirstOrDefault();
            if (brand == null)
                return true;
            brand.IsActive = false;
            dynamic prods = (from p in db.Products
                             where p.BrandID.Equals(brand.Id)
                             select p).DefaultIfEmpty();
            if(prods != null)
            {
                foreach(Product p in prods)
                {
                    if(p != null)
                        p.IsAvailable = false;
                }
            }
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
     
        bool IService1.deleteCategory(int id)
        {
            var category = (from c in db.Categories
                         where c.Id.Equals(id)
                         select c).FirstOrDefault();
            if (category == null)
                return true;
            category.IsAvailable = false;
            dynamic prods = (from p in db.Products
                             where p.BrandID.Equals(category.Id)
                             select p).DefaultIfEmpty();
            if (prods != null)
            {
                foreach (Product p in prods)
                {
                    if(p != null)
                        p.IsAvailable = false;
                }
            }
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
       
        bool IService1.deleteColourway(int id)
        {
            var colourway = (from c in db.Colourways
                            where c.Id.Equals(id)
                            select c).FirstOrDefault();
            if (colourway == null)
                return true;
            colourway.IsAvailable = false;
            dynamic prods = (from p in db.Products
                             where p.BrandID.Equals(colourway.Id)
                             select p).DefaultIfEmpty();
            if (prods != null)
            {
                foreach (Product p in prods)
                {
                    if (p != null)
                        p.IsAvailable = false;
                }
            }
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

        bool IService1.deteGenderCategory(int id)
        {
            var genderCategory = (from c in db.Colourways
                             where c.Id.Equals(id)
                             select c).FirstOrDefault();
            if (genderCategory == null)
                return true;
            genderCategory.IsAvailable = false;
            dynamic prods = (from p in db.Products
                             where p.BrandID.Equals(genderCategory.Id)
                             select p).DefaultIfEmpty();
            if (prods != null)
            {
                foreach (Product p in prods)
                {
                    if (p != null)
                        p.IsAvailable = false;
                }
            }
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

        bool IService1.deleteCartItems(int userId)
        {
            dynamic cartItems = (from c in db.Carts
                                 where c.UserID.Equals(userId)
                                 select c).DefaultIfEmpty();
            if (cartItems == null)
                return false;

            foreach(Cart c in cartItems)
            {
                if(c != null)
                {
                    var cp = (from cpDelete in db.Carts
                              where cpDelete.SizeID.Equals(c.SizeID)
                              select cpDelete).FirstOrDefault();
                    if(cp != null)
                        db.Carts.DeleteOnSubmit(cp);
                }
            }

            try
            {
                db.SubmitChanges();
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }
        bool IService1.removeItemFromCart(int userID, int sizeID)
        {
            var prod = (from p in db.Carts
                        where p.SizeID.Equals(sizeID) && p.UserID.Equals(userID)
                        select p).FirstOrDefault();

            if(prod == null)
                return false;

            db.Carts.DeleteOnSubmit(prod);
            try
            {
                db.SubmitChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

        bool IService1.removeShoeSize(int sizeId)
        {
            var size = (from s in db.ProductSizes
                        where s.Id.Equals(sizeId)
                        select s).FirstOrDefault();
            if (size == null)
                return false;
            db.ProductSizes.DeleteOnSubmit(size);
            try
            {
                db.SubmitChanges();
                return true;
            } 
            catch(Exception ex)
            {
                return false;
            }
        }



    }

}
