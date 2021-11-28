using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzeriaManagementApp.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public float TotalPrice { get; set; }
        public string Status { get; set; }
        public string Payment { get; set; }

        public Guid OrderAddressId { get; set; }
        [ForeignKey("OrderAddressId")]
        public virtual OrderAddress Address { get; set; }

        public string CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual ApplicationUser Customer { get; set; }

        public Guid PizzeriaId { get; set; }
        [ForeignKey("PizzeriaId")]
        public virtual Pizzeria Pizzeria { get; set; }

        public virtual IEnumerable<CartOrders> CartOrders { get; set; }
    }
}
