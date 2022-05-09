using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CustomerManager.Domain.Models;
using CustomerManager.Persistence.Contratos;
using CustomerManager.Persistence.Contextos;

namespace CustomerManager.Persistence
{
    public class ClientPersist : IClientPersist
    {
        private readonly ClientContext _context;

        public ClientPersist(ClientContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public async Task<Client[]> GetAllClientsAsync()
        {
            IQueryable<Client> query = _context.Clientes;

            query = query.OrderBy(e => e.ClientId);

            return await query.ToArrayAsync();
        }
    }
}