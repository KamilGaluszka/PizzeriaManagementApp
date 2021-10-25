using Microsoft.EntityFrameworkCore;
using PizzeriaManagementApp.Models;

namespace PizzeriaManagementApp.Data
{
    public class PizzeriaDbContext : DbContext
    {
        public PizzeriaDbContext(DbContextOptions<PizzeriaDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PizzaProducts> PizzaProducts { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
    }
}
