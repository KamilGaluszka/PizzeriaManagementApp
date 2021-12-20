using System;

namespace PizzeriaManagementAppMobile.Models
{
    public class ApplicationUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}
