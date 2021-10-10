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
    }
}
