using System;
using System.ComponentModel.DataAnnotations;

namespace PizzeriaManagementApp.Models
{
    public class Employee : ApplicationUser
    {
        [Required]
        public float Salary { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Start date of work")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "End date of work")]
        public DateTime? EndDate { get; set; }
        public string IdManager { get; set; }
    }
}
