using System;
using System.Collections.Generic;

namespace PizzeriaManagementAppMobile.Models
{
    public class Pizzeria
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }
        public string IdManager { get; set; }
        public virtual Employee Manager { get; set; }
        public Guid AddressId { get; set; }
        public virtual Address Address { get; set; }
        public virtual List<PizzeriaEmployee> PizzeriaEmployees { get; set; }
        public virtual List<PizzeriaPizza> PizzeriaPizzas { get; set; }

        public string OpenHours 
        {
            get => $"Open hours: {OpenTime:HH: mm tt} - {CloseTime:HH: mm tt}";
        }
    }
}
