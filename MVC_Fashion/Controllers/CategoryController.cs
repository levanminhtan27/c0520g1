using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC_Fashion.Models;
using MVC_Fashion.Repositories;

namespace MVC_Fashion.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository iCategory;

        public CategoryController(ICategoryRepository ICategory)
        {
           this.iCategory = ICategory;
        }


        public IActionResult Index()
        {
            var prod = new List<Category>();
            prod = iCategory.GetCategory().ToList();
            return View(prod);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    iCategory.CreateCategory(category);
                    return RedirectToAction("Show", "Product");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return View(category);
        }
    }
}
