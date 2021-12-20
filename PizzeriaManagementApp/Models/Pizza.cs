using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzeriaManagementApp.Models
{
    public class Pizza
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Product name")]
        public string Name { get; set; }
        public float Price { get; set; }
        [Required]
        public string Image { get; set; }

        public Guid SizeId { get; set; }
        [ForeignKey("SizeId")]
        public virtual Size Size { get; set; }

        public Guid ThicknessId { get; set; }
        [ForeignKey("ThicknessId")]
        public virtual Thickness Thickness { get; set; }

        public virtual List<PizzaProducts> PizzaProducts { get; set; }
        public virtual List<PizzeriaPizza> PizzeriaPizzas { get; set; }
    }
}
