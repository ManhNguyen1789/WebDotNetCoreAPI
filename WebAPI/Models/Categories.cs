using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Products> Products { get; set; } // Navigation: chứa nhiều sản phẩm
    }
}
