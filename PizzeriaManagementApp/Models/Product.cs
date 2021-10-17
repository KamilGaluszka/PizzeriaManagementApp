using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzeriaManagementApp.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Product name")]
        public string Name { get; set; }

        [Display(Name = "Category name")]
        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
