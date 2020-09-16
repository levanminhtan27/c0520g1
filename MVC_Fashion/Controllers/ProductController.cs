using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC_Fashion.Models;
using MVC_Fashion.Repositories;
using MVC_Fashion.ViewModel;

namespace MVC_Fashion.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
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
        public IActionResult Create(ProductViewModel mode)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    productRepository.CreateProduct(mode);
                    productRepository.Save();
                    return View();
                    //return RedirectToAction("Index","Product");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return View(mode);
        }
    }
}
