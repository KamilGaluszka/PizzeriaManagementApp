using System;

namespace PizzeriaManagementAppMobile.ViewModels
{
    public class Address
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Town { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string ApartmentNumber { get; set; }
    }
}
