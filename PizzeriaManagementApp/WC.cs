using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace PizzeriaManagementApp
{
    public static class WC
    {
        public const string PizzaImagePath = @"\images\pizza\";

        public const string SessionCart = "ShoppingCardSession";

        public const string AdminRole = "Admin";
        public const string ManagerRole = "Manager";
        public const string EmployeeRole = "Employee";
        public const string CustomerRole = "Customer";

        public const string Ordered = "Ordered";
        public const string InProgress = "In progress";
        public const string Baking = "Baking";
        public const string Delivering = "Delivering";
        public const string Done = "Done";

        public static List<SelectListItem> GetPayments()
        {
            List<SelectListItem> paymentsDropDown = new List<SelectListItem>();
            paymentsDropDown.Add(new SelectListItem()
            {
                Text = "Cash",
                Value = "Cash"
            });
            paymentsDropDown.Add(new SelectListItem()
            {
                Text = "Credit card",
                Value = "Credit card"
            });
            paymentsDropDown.Add(new SelectListItem()
            {
                Text = "Bank transfer",
                Value = "Bank transfer"
            });
            return paymentsDropDown;
        }
    }
}
