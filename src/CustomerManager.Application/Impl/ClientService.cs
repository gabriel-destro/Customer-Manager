using System;
using System.Collections.Generic;
using System.Linq;
using CustomerManager.Application.Contratos;
using CustomerManager.Application.CustomException;
using CustomerManager.Domain.Models;
using CustomerManager.Persistence.Contextos;
using CustomerManager.Persistence.Contratos;


namespace CustomerManager.Application
{
    public class ClientService : IClientService
    {
        private readonly IClientPersist _clientPersist;
        private readonly ClientContext _clientContext;
        public ClientService(IClientPersist clientPersist, ClientContext clientContext)
        {
            _clientPersist = clientPersist;  
            _clientContext = clientContext;
        }

        public Client AddClient(Client objClient)
        {
            try{
                if(objClient.ClientId > 0) throw new BusinessException("Para criar um novo client, não envie ClientId no corpo da requisição");
                
                var clientExists = _clientContext.Clientes.FirstOrDefault(c => c.CPF == objClient.CPF || c.RG == objClient.RG);
                if(clientExists != null) throw new BusinessException("CPF/RG do cliente já cadastrado.");

                objClient.DateRegistration = DateTime.Now;
                    _clientContext.Clientes.Add(objClient);
                    _clientContext.SaveChangesAsync();
                    return objClient;

            }catch (Exception ex){
                throw ex;
            }
        }

        public IEnumerable<Client> GetAllClient()
        {
            try
            {
                var clientes =  _clientContext.Clientes;
                return clientes;
            }
            catch (Exception ex)
            {
                throw ex;  
            }
        }
    }
}