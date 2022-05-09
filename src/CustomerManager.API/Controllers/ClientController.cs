using System;
using CustomerManager.Application.CustomException;
using CustomerManager.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CustomerManager.Application;
using Microsoft.Extensions.Logging;
using CustomerManager.Application.Contratos;

namespace CustomerManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreateController : ControllerBase
    {
        private readonly ILogger<CreateController> _logger;
        private readonly IClientService _ClientService;
        public CreateController(IClientService ClientService, ILogger<CreateController> logger)
        {
            _ClientService = ClientService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllClients()
        {
            try{
                var clientes = _ClientService.GetAllClient();

            if (clientes == null) { 
                return NotFound("Nenhum cliente encontrado."); 
            }

            return Ok(clientes);
            }
            catch (Exception ex){
                _logger.LogError(ex, "Erro ao recuperar clientes");
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar clientes");
            }
        }
        
        [HttpPost]
        public IActionResult CreateAsync(Client objClient)
        {
            try{
                objClient = _ClientService.AddClient(objClient);
                return Ok(objClient);
            } catch (BusinessException ex){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar cadastrar cliente: {ex.Message}");
            }
            catch (Exception ex){
                _logger.LogError(ex, "Erro ao cadastrar clientes");
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar cadastrar cliente");
            }
        }
    }
}
