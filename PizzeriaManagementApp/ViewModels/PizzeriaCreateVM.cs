using Microsoft.AspNetCore.Mvc.Rendering;
using PizzeriaManagementApp.Models;
using System.Collections.Generic;

namespace PizzeriaManagementApp.ViewModels
{
    public class PizzeriaCreateVM
    {
        public Pizzeria Pizzeria { get; set; }
        public IEnumerable<SelectListItem> Managers { get; set; }
    }
}
