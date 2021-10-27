using PizzeriaManagementApp.Models;
using System.Collections.Generic;

namespace PizzeriaManagementApp.ViewModels
{
    public class PizzaProductsIndexVM
    {
        public string PizzaName { get; set; }
        public IEnumerable<PizzaProducts> PizzaProducts { get; set; }
    }
}
