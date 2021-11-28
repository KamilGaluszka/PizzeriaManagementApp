using System;
using System.ComponentModel.DataAnnotations;

namespace PizzeriaManagementApp.Models
{
    public class OrderAddress
    {
        public Guid Id { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }
        [Required]
        public string Town { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        [Display(Name = "House number")]
        public string HouseNumber { get; set; }
        [Display(Name = "Apartment number")]
        public string ApartmentNumber { get; set; }
    }
}
