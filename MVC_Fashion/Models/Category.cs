﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Fashion.Models
{
    //thể loại sản phẩm
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public List<Product> products { get; set; }
    }
}
