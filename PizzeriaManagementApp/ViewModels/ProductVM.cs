using Microsoft.AspNetCore.Mvc.Rendering;
using PizzeriaManagementApp.Models;
using System.Collections.Generic;

namespace PizzeriaManagementApp.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
