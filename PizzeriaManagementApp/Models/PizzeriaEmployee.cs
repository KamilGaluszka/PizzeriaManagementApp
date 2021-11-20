using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzeriaManagementApp.Models
{
    public class PizzeriaEmployee
    {
        public Guid Id { get; set; }
        public Guid PizzeriaId { get; set; }
        [ForeignKey("PizzeriaId")]
        public virtual Pizzeria Pizzeria { get; set; }

        public string EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}
