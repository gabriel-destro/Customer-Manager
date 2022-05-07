using System;
using System.Collections.Generic;
using System.Linq;
using CustomerManager.API.CustomException;
using CustomerManager.Controllers;
using CustomerManager.Data;
using CustomerManager.Models;
using Microsoft.Extensions.Logging;

namespace CustomerManager.API.Services
{
    public class ClientService
    {
        private readonly DataContext _context;
        private readonly ILogger<CreateController> _logger;
        public ClientService(DataContext context, ILogger<CreateController> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        //public async Task<IActionResult> GetAllClients()
        public IEnumerable<Client> GetAllClients()
        {
            try
            {
                var clientes =  _context.Clientes;
                return clientes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao recuperar clientes");
                throw ex;  
            }
        }

        public Client CreateAsync(Client objClient)
        {
            try{
                if(objClient.ClientId > 0) throw new BusinessException("Para criar um novo client, não envie ClientId no corpo da requisição");

                var clientExists = _context.Clientes.FirstOrDefault(c => c.CPF == objClient.CPF || c.RG == objClient.RG);
                
                if(clientExists != null) throw new BusinessException("CPF/RG do cliente já cadastrado.");
                
                if(objClient.Name == "Carlos") throw new BusinessException("Não aceitamos clientes com o nome Carlos.");
                    objClient.DateRegistration = DateTime.Now;
                    _context.Clientes.Add(objClient);
                    _context.SaveChangesAsync();
                    Console.WriteLine(objClient.ClientId);
                    return objClient;
    
            } catch (Exception ex){
                _logger.LogError(ex, "Erro ao cadastrar clientes");
                throw ex;
            }
        }
    }
}