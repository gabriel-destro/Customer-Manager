using System.Collections.Generic;
using CustomerManager.Domain.Models;

namespace CustomerManager.Application.Contratos
{
    public interface IClientService
    {
        Client AddClient(Client model);
        IEnumerable<Client> GetAllClient();
    }
}