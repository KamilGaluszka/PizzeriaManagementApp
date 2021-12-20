using System;
using System.Collections.Generic;

namespace PizzeriaManagementAppMobile.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<PizzaProducts> PizzaProducts { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
