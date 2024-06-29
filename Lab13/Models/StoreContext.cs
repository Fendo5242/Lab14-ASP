using Microsoft.EntityFrameworkCore;

namespace Lab13.Models
{
    public class StoreContext: DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Detail> Details { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Server=DESKTOP-O2O9JAG\\SQLEXPRESS2017;Database=market;User Id=Fendo;Password=12345;trustservercertificate=True");

        }
    }
}
