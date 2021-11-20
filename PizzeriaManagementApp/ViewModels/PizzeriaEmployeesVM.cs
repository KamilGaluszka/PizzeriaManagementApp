using System;
using System.Collections.Generic;

namespace PizzeriaManagementApp.ViewModels
{
    public class PizzeriaEmployeesVM
    {
        public Guid IdPizzeria { get; set; }
        public List<CheckBoxItem<string>> EmployeesCheckBoxList { get; set; }
    }
}
