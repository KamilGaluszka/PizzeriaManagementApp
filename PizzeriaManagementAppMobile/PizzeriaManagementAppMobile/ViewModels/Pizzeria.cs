using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaManagementAppMobile.ViewModels
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
        public virtual IEnumerable<PizzeriaEmployee> PizzeriaEmployees { get; set; }
        public virtual IEnumerable<PizzeriaPizza> PizzeriaPizzas { get; set; }
    }
}
