using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Fashion.ViewModel
{
    public class ProductViewModel
    {
        public string NameProduct { get; set; }
        public string AddDescription { get; set; }
        public int Price { get; set; }
        public IFormFile AvataPast { get; set; }
    }
}
