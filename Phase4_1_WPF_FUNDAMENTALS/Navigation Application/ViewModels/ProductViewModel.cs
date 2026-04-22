using Navigation_Application.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Navigation_Application.ViewModels
{
    public class ProductViewModel
    {
        public string Title => "Welcome to Product Page";

        public List<Product> Products { get; set; }

        public ProductViewModel() 
        {
            Products = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop", Price = 50000 },
                new Product { Id = 2, Name = "Mouse", Price = 500 },
                new Product { Id = 3, Name = "Keyboard", Price = 1500 }
            };
        }
    }
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }

}
