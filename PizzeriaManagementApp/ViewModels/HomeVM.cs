using PizzeriaManagementApp.Models;
using System.Collections.Generic;

namespace PizzeriaManagementApp.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Pizzeria> Pizzerias { get; set; }
        public IEnumerable<string> Towns { get; set; }
    }
}
