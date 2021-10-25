using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzeriaManagementApp.Models
{
    public class Pizza
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Product name")]
        public string Name { get; set; }
        public float Price { get; set; }
        public virtual IEnumerable<PizzaProducts> PizzaProducts { get; set; }
    }
}
