using System;
using System.ComponentModel.DataAnnotations;

namespace PizzeriaManagementApp.Models
{
    public class Size
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
