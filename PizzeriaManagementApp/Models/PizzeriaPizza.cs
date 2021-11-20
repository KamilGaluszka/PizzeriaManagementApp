using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzeriaManagementApp.Models
{
    public class PizzeriaPizza
    {
        public Guid Id { get; set; }
        public Guid PizzeriaId { get; set; }
        [ForeignKey("PizzeriaId")]
        public virtual Pizzeria Pizzeria { get; set; }

        public Guid PizzaId { get; set; }
        [ForeignKey("PizzaId")]
        public virtual Pizza Pizza { get; set; }
    }
}
