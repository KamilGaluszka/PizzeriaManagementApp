﻿using System;
using System.Collections.Generic;

namespace PizzeriaManagementApp.ViewModels
{
    public class PizzaProductsVM
    {
        public Guid IdPizza { get; set; }
        public List<CheckBoxItem<Guid>> ProductsCheckBoxList { get; set; }
    }
}
