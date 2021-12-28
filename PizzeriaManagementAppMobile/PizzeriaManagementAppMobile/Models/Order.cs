using System;
using System.Collections.Generic;

namespace PizzeriaManagementAppMobile.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public float TotalPrice { get; set; }
        public string Status { get; set; }
        public string Payment { get; set; }

        public Guid OrderAddressId { get; set; }
        public virtual OrderAddress Address { get; set; }

        public string CustomerId { get; set; }
        public virtual ApplicationUser Customer { get; set; }

        public Guid PizzeriaId { get; set; }
        public virtual Pizzeria Pizzeria { get; set; }

        public virtual List<CartOrders> CartOrders { get; set; }
        public string CurrentStatus
        {
            get
            {
                return $"Status is: {Status}";
            }
        }

        public string PaymentMethod
        {
            get
            {
                return $"Payment: {Payment}";
            }
        }

        public string CreatedOnDate
        {
            get
            {
                return $"Created on: {CreatedOn.ToString("HH:mm:ss dd-MM-yyyy")}";
            }
        }
    }
}
