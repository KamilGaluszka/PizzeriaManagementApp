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
        public DbSet<PizzeriaPizza> PizzeriaPizzas { get; set; }
        public DbSet<PizzeriaEmployee> PizzeriaEmployees { get; set; }
        public DbSet<Pizzeria> Pizzerias { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CartOrders> CartOrders { get; set; }
        public DbSet<OrderAddress> OrderAddresses { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Thickness> Thicknesses { get; set; }
    }
}
