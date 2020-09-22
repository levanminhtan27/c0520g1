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
        private readonly IWebHostEnvironment webHost;
        private ProductDBContext _context;
        public ProductController(IProductRepository productRepository,
            IWebHostEnvironment webHost, ProductDBContext context)
        {
            this.productRepository = productRepository;
            this.webHost = webHost;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        /* public IActionResult ShowOne(int? id)
         {
             try
             {
                 int.Parse(id.Value.ToString());
                 var prd = productRepository.GetProduct(id.Value);
                 if (prd == null)
                 {
                     return View();
                 }

                 var product = productRepository.GetProduct(id ?? 1);
                 return View(product);
             }
             catch (Exception e)
             {

                 throw e;
             }

         }*/

        [Route("Product/ShowOne/{productId}")]
        public ViewResult ShowOne(int productId)
        {
            var detailView = productRepository.GetProduct(productId);
            return View(detailView);
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
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var edit = productRepository.GetProduct(id);
            return View(edit);
        }
        [HttpPost]
        public IActionResult Edit(EditProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                productRepository.EditProduct(model);
                //string delFile = Path.Combine(webHost.WebRootPath, "images", model.AvatarPaths);
                //System.IO.File.Delete(delFile);
                return RedirectToAction("Show", "Product");
            }
            return RedirectToAction("Show", "Product");
        }

        [HttpGet]
        public IActionResult Show()
        {
            var prod = new List<Product>();
            prod = productRepository.GetListProduct();
            return View(prod);
           
        }
        [Route("Product/ShowUser/{id}")]
        public IActionResult ShowUser(int id)
        {
            var prodLits = new List<Product>();
            prodLits = _context.Products.ToList().FindAll(el=>el.CategoryId==id);
            return View(prodLits);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {

            productRepository.DeleteProduct(id);
            return RedirectToAction("Show", "Product");
        }
    }
}
