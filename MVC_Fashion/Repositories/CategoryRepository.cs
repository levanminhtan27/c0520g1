using MVC_Fashion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Fashion.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ProductDBContext context;

        public CategoryRepository(ProductDBContext context)
        {
            this.context = context;
        }
        public int CreateCategory(Category category)
        {
            context.Add(category);
            return context.SaveChanges();
            
        }

        public IEnumerable<Category> GetCategory()
        {
            return context.Categories.ToList();
        }

        /*  public int EditCategory(Category product)
          {
              if (ModelState.IsValid)
              {
                  var acc = appDbContext.Account.Find(id);
                  acc.Email = model.Email;
                  acc.Name = model.Name;
                  acc.Phone = model.Phone;


                  appDbContext.Account.Update(acc);
                  await appDbContext.SaveChangesAsync();

                  return RedirectToAction("ShowDataTables", "Home");
              }
              return RedirectToAction("ShowDataTables", "Home");
          }
  */
        public void SaveCategory()
        {
            throw new NotImplementedException();
        }
    }
}
