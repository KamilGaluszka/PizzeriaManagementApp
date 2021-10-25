using System;
using System.Collections.Generic;

namespace PizzeriaManagementApp.ViewModels
{
    public class PizzaProductsViewModel
    {
        public Guid IdPizza { get; set; }
        public List<CheckBoxItem> ProductsCheckBoxList { get; set; }
    }
}
