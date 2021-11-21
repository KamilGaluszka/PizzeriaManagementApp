using PizzeriaManagementApp.Models;
using System.Collections.Generic;

namespace PizzeriaManagementApp.ViewModels
{
    public class PizzeriaMenuVM
    {
        public Pizzeria Pizzeria { get; set; }
        public IEnumerable<Pizza> Pizzas { get; set; }
    }
}
