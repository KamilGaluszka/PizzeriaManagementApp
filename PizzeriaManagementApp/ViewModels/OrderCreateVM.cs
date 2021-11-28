using Microsoft.AspNetCore.Mvc.Rendering;
using PizzeriaManagementApp.Models;
using System.Collections.Generic;

namespace PizzeriaManagementApp.ViewModels
{
    public class OrderCreateVM
    {
        public float TotalPrice { get; set; }
        public IEnumerable<SelectListItem> Payments { get; set; }
        public string Payment { get; set; }
        public OrderAddress Address { get; set; }
    }
}
