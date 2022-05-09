using System.Threading.Tasks;
using CustomerManager.Domain.Models;

namespace CustomerManager.Persistence.Contratos
{
    public interface IClientPersist
    {
        void Add<T>(T entity) where T: class;
        
        Task<Client[]> GetAllClientsAsync();
    }
}