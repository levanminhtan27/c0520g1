using Microsoft.AspNetCore.Hosting;
using MVC_Fashion.Models;
using MVC_Fashion.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

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

            context.Products.Add(products);
            return context.SaveChanges();
        }
        public int DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }
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
