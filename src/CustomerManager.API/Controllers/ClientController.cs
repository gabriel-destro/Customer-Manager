using System;
using CustomerManager.API.CustomException;
using CustomerManager.API.Services;
using CustomerManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreateController : ControllerBase
    {
        private readonly ClientService _service;
        public CreateController(ClientService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllClients()
        {
            try{
                var clientes = _service.GetAllClients();

            if (clientes == null) { 
                return NotFound("Nenhum cliente encontrado."); 
            }

            return Ok(clientes);
            }
            catch {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar clientes");
            }
        }
        
        [HttpPost]
        public IActionResult CreateAsync(Client objClient)
        {
            try{
                objClient = _service.CreateAsync(objClient);
                return Ok(objClient);
            } catch (BusinessException ex){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar cadastrar cliente: {ex.Message}");
            }
            catch (Exception ex){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar cadastrar cliente");
            }
        }
    }
}
