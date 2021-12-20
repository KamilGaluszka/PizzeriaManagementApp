using PizzeriaManagementApp.Models;
using System.Collections.Generic;

namespace PizzeriaManagementApp.ViewModels
{
    public class MenuVM
    {
        public Pizzeria Pizzeria { get; set; }
        public List<Pizza> Pizzas { get; set; } 
    }
}
