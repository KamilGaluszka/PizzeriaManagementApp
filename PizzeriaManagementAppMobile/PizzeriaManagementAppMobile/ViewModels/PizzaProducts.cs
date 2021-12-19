using System;

namespace PizzeriaManagementAppMobile.ViewModels
{
    public class PizzaProducts
    {
        public Guid Id { get; set; }
        public Guid IdPizza { get; set; }
        public virtual Pizza Pizza { get; set; }

        public Guid IdProduct { get; set; }
        public virtual Product Product { get; set; }
    }
}
