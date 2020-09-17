using Microsoft.AspNetCore.Hosting;
using MVC_Fashion.Models;
using MVC_Fashion.Models.ViewModel;
using MVC_Fashion.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MVC_Fashion.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDBContext context;
        private readonly IWebHostEnvironment webHost;

        public ProductRepository(ProductDBContext context, IWebHostEnvironment webHost)
        {
            this.context = context;
            this.webHost = webHost;
        }
        public int CreateProduct(ProductViewModel product)
        {
            Product products = new Product()
            {
                NameProduct = product.NameProduct,
                Description = product.AddDescription,
                Price = product.Price
            };
            string FileImage = null;
            if (product.AvataPast != null)
            {
                string uploadsFolder = Path.Combine(webHost.WebRootPath, "images");
                FileImage = Guid.NewGuid().ToString() + "_" + product.AvataPast.FileName;
                string filePath = Path.Combine(uploadsFolder, FileImage);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    product.AvataPast.CopyTo(fileStream);
                }
            }
            products.Avata = FileImage;
            products.CategoryId = 2;

            context.Add(products);
            return context.SaveChanges();
        }
     

        public int EditProduct(EditProductViewModel model)
        {
            var product = new Product()
            {
                NameProduct = model.NameProduct,
                Price = model.Price,
                ProductId = model.Id,
                CategoryId=model.CategoryId,
                Avata = model.AvatarPaths
            };
            product.CategoryId = 2;
            string fileName = string.Empty;
            if (model.AvataPast != null)
            {
                string uploadFolder = Path.Combine(webHost.WebRootPath, "images");
                fileName = $"{Guid.NewGuid()}_{model.AvataPast.FileName}";
                var filePath = Path.Combine(uploadFolder, fileName);
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    model.AvataPast.CopyTo(fs);
                }
                Product products = new Product();
                products.Avata = fileName;
                products.NameProduct = product.NameProduct;
                products.ProductId = product.ProductId;
                products.CategoryId = 2;
                products.Price = product.Price;
                if (!string.IsNullOrEmpty(product.Avata) && (products.Avata != "none-avatar.png"))
                {
                    string delFile = Path.Combine(webHost.WebRootPath , "images", product.Avata);
                    /*System.IO.File.Delete(delFile);*/
                }
                context.Products.Update(products);

            }
            else
            {
                context.Products.Update(product);
            }
            

           
            return context.SaveChanges();

        }
        
        public List<Product> GetListProduct()
        {
            return  context.Products.ToList();
        }

        /* public int DeleteProduct(int id)
{
    throw new NotImplementedException();
}*/

        public IEnumerable<Product> GetProduct()
        {
            throw new NotImplementedException();
        }

        public Product GetProductId(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public int UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
