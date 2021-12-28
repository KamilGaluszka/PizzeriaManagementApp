using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaManagementAppMobile.Models
{
    public class CartOrders
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }

        public Guid PizzaId { get; set; }
        public virtual Pizza Pizza { get; set; }

        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
