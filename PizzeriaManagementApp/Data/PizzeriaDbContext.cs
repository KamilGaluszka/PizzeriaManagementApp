using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PizzeriaManagementApp.Models;

namespace PizzeriaManagementApp.Data
{
    public class PizzeriaDbContext : IdentityDbContext
    {
        public PizzeriaDbContext(DbContextOptions<PizzeriaDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PizzaProducts> PizzaProducts { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
