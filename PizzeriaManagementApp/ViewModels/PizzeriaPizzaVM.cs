using System;
using System.Collections.Generic;

namespace PizzeriaManagementApp.ViewModels
{
    public class PizzeriaPizzaVM
    {
        public Guid IdPizzeria { get; set; }
        public List<CheckBoxItem<Guid>> PizzasCheckBoxList { get; set; }
    }
}
