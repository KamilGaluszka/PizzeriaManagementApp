using System;

namespace PizzeriaManagementAppMobile.Models
{
    public class Employee : ApplicationUser
    {
        public float Salary { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string IdManager { get; set; }
    }
}
