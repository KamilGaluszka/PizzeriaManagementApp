using System;
using System.Collections.Generic;

namespace PizzeriaManagementAppMobile.ViewModels
{
    public class Pizza
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Image { get; set; }

        public Guid SizeId { get; set; }
        public virtual Size Size { get; set; }

        public Guid ThicknessId { get; set; }
        public virtual Thickness Thickness { get; set; }

        public virtual IEnumerable<PizzaProducts> PizzaProducts { get; set; }
        public virtual IEnumerable<PizzeriaPizza> PizzeriaPizzas { get; set; }
    }
}
