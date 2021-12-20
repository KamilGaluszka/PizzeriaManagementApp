using System;

namespace PizzeriaManagementAppMobile.Models
{
    public class PizzeriaEmployee
    {
        public Guid Id { get; set; }
        public Guid PizzeriaId { get; set; }
        public virtual Pizzeria Pizzeria { get; set; }

        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
