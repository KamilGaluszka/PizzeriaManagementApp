using System;

namespace PizzeriaManagementAppMobile.ViewModels
{
    public class PizzeriaPizza
    {
        public Guid Id { get; set; }
        public Guid PizzeriaId { get; set; }
        public virtual Pizzeria Pizzeria { get; set; }

        public Guid PizzaId { get; set; }
        public virtual Pizza Pizza { get; set; }
    }
}
