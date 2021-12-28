using PizzeriaManagementApp.ViewModels;
using System;
using System.Collections.Generic;

namespace PizzeriaManagementApp.Models
{
    public class OrderAPI
    {
        public string Email { get; set; }
        public float TotalPrice { get; set; }
        public List<PizzaAmountVM> PizzaAmounts { get; set; }
        public Address Address { get; set; }
        public Guid PizzeriaId { get; set; }
        public string Payment { get; set; }
    }
}
