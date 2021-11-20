using PizzeriaManagementApp.Models;
using System.Collections.Generic;

namespace PizzeriaManagementApp.ViewModels
{
    public class PizzeriaEmployeesIndexVM
    {
        public string PizzeriaName { get; set; }
        public IEnumerable<PizzeriaEmployee> PizzeriaEmployees { get; set; }
    }
}
