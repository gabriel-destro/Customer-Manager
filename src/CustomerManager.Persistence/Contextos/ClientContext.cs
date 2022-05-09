using Microsoft.EntityFrameworkCore;
using CustomerManager.Domain.Models;

namespace CustomerManager.Persistence.Contextos
{
    public class ClientContext : DbContext
    {
        public ClientContext(DbContextOptions<ClientContext> options) 
            : base(options) {}
        public DbSet<Client> Clientes { get; set; }
    }
}