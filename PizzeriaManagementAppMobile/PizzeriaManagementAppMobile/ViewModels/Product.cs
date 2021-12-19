using System;
using System.Collections.Generic;

namespace PizzeriaManagementAppMobile.ViewModels
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual IEnumerable<PizzaProducts> PizzaProducts { get; set; }
    }
}
