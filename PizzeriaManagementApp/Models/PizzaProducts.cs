using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzeriaManagementApp.Models
{
    public class PizzaProducts
    {
        public Guid Id { get; set; }
        public Guid IdPizza { get; set; }
        [ForeignKey("IdPizza")]
        public virtual Pizza Pizza { get; set; }

        public Guid IdProduct { get; set; }
        [ForeignKey("IdProduct")]
        public virtual Product Product { get; set; }
    }
}
