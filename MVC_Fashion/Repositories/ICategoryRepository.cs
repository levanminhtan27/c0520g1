using MVC_Fashion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Fashion.Repositories
{
    public interface ICategoryRepository
    {
        int CreateCategory(Category product);
        IEnumerable<Category> GetCategory();
        /*int EditCategory(Category product);
        void SaveCategory();*/
    }
}
