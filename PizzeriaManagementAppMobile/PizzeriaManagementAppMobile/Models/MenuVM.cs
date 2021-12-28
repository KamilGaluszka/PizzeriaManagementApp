using System.Collections.Generic;
using System.Linq;

namespace PizzeriaManagementAppMobile.Models
{
    public class MenuVM
    {
        public Pizza Pizza { get; set; }
        public List<Product> Products { get; set; }
        public string SizeAndThickness
        {
            get
            {
                return $"{Pizza.Size.Name} {Pizza.Size.Value} - {Pizza.Thickness.Name}";
            }
        }

        public string PizzaPrice
        {
            get
            {
                return $"Price: {Pizza.Price}";
            }
        }

        public string ProductsNames { 
            get
            {
                List<string> names = Products.Select(x => x.ToString()).ToList();
                return string.Join(", ", names);
            }
        }
    }
}
