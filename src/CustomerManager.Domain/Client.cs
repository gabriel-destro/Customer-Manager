using System;
using CustomerManager.Swagger;

namespace CustomerManager.Domain.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
        public string RG  { get; set; }
        public string CPF { get; set; }
        public string Address { get; set; }
        public bool ActiveCustomer { get; set; }
        
        [SwaggerIgnoreAttribute]
        public DateTime DateRegistration { get; set; }
    }
}