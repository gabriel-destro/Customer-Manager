using Microsoft.EntityFrameworkCore;
using CustomerManager.Models;

namespace CustomerManager.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
        public DbSet<Client> Clientes { get; set; }
    }
}