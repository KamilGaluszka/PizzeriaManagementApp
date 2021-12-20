using PizzeriaManagementApp.Models;
using System.Collections.Generic;

namespace PizzeriaManagementApp.ViewModels
{
    public class PizzaWithProductsVM
    {
        public Pizza Pizza { get; set; }
        public List<Product> Products { get; set; }
    }
}
