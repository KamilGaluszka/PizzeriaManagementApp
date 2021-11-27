using PizzeriaManagementApp.Models;

namespace PizzeriaManagementApp.ViewModels
{
    public class PizzaCartItemVM
    {
        public Pizzeria Pizzeria { get; set; }
        public Pizza Pizza { get; set; }
        public int Amount { get; set; }
    }
}
