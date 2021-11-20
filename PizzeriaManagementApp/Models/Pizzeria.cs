using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzeriaManagementApp.Models
{
    public class Pizzeria
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Opened at")]
        public DateTime OpenTime { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Closed at")]
        public DateTime CloseTime { get; set; }
        public string IdManager { get; set; }
        [ForeignKey("IdManager")]
        public virtual Employee Manager { get; set; }
        public Guid AddressId { get; set; }
        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }
        public virtual IEnumerable<PizzeriaEmployee> PizzeriaEmployees { get; set; }
        public virtual IEnumerable<PizzeriaPizza> PizzeriaPizzas { get; set; }
    }
}
