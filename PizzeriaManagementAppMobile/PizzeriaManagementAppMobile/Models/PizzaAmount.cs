namespace PizzeriaManagementAppMobile.Models
{
    public class PizzaAmount
    {
        public Pizza Pizza { get; set; }
        public int Amount { get; set; }

        public string PizzaWithAmount
        {
            get
            {
                return $"{Amount}x {Pizza.Name}";
            }
        }

        public string PizzaPrice
        {
            get
            {
                return $"Price: {Pizza.Price * Amount}";
            }
        }

        public string SizeAndThickness
        {
            get
            {
                return $"{Pizza.Size.Name} {Pizza.Size.Value} - {Pizza.Thickness.Name}";
            }
        }
    }
}
