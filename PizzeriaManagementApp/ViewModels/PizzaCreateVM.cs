using Microsoft.AspNetCore.Mvc.Rendering;
using PizzeriaManagementApp.Models;
using System.Collections.Generic;

namespace PizzeriaManagementApp.ViewModels
{
    public class PizzaCreateVM
    {
        public Pizza Pizza { get; set; }
        public IEnumerable<SelectListItem> Sizes { get; set; }
        public IEnumerable<SelectListItem> Thicknesses { get; set; }
    }
}
