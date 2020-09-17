using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MVC_Fashion.Models;
using MVC_Fashion.Models.ViewModel;
using MVC_Fashion.Repositories;
using MVC_Fashion.ViewModel;

namespace MVC_Fashion.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly ProductDBContext appDbContext;
        private readonly IWebHostEnvironment webHost;

        public ProductController(IProductRepository productRepository, ProductDBContext appDbContext,
            IWebHostEnvironment webHost)
        {
            this.productRepository = productRepository;
            this.appDbContext = appDbContext;
            this.webHost = webHost;
        }
        public IActionResult Index()
        {
            var prod = new List<Product>();
            prod = productRepository.GetProduct().ToList();
            return View(prod);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    productRepository.CreateProduct(model);
                    return RedirectToAction("Show", "Product");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = appDbContext.Products.FirstOrDefault(f => f.ProductId == id);
            EditProductViewModel EditProduct = new EditProductViewModel()
            {
                NameProduct = product.NameProduct,
                Price = product.Price,
                Id = product.ProductId,
                AddDescription = product.Description,
                AvatarPaths = product.Avata
            };
            return View(EditProduct);
        }
        [HttpPost]
        public IActionResult Edit(EditProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                productRepository.EditProduct(model);
                string delFile = Path.Combine(webHost.WebRootPath, "images", model.AvatarPaths);
                System.IO.File.Delete(delFile);
                return RedirectToAction("Show", "Product");
            }
            return RedirectToAction("Show", "Product");
        }

        [HttpGet]
        public IActionResult Show()
        {
            return View(appDbContext.Products.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = appDbContext.Products.ToList().Find(p => p.ProductId == id);
            string delFile = Path.Combine(webHost.WebRootPath, "images", product.Avata);
            System.IO.File.Delete(delFile);
            appDbContext.Products.Remove(product);
            await appDbContext.SaveChangesAsync();
            return RedirectToAction("Show", "Product");
        }
    }
}
