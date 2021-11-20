using PizzeriaManagementApp.Models;
using System.Collections.Generic;

namespace PizzeriaManagementApp.ViewModels
{
    public class PizzeriaPizzaIndexVM
    {
        public string PizzeriaName { get; set; }
        public IEnumerable<PizzeriaPizza> PizzeriaPizzas { get; set; }
    }
}
