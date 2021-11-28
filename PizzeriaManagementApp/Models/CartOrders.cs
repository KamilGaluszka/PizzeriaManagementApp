using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzeriaManagementApp.Models
{
    public class CartOrders
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }

        public Guid PizzaId { get; set; }
        [ForeignKey("PizzaId")]
        public virtual Pizza Pizza { get; set; }

        public Guid OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }
}