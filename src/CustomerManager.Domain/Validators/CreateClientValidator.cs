using System.Text.RegularExpressions;
using CustomerManager.Domain.Models;
using FluentValidation;

namespace CustomerManager.Domain.Validators
{
    public class CreateClientValidator : AbstractValidator<Client>
    {
        public CreateClientValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .Must(validString).WithMessage("Nome não aceita números e caracteres especiais.")
                // O menor nome possivel tem 3 caracteres. Ex: Ana
                .MinimumLength(3).WithMessage("Mínimo de caracteres é 3.")
                .MaximumLength(50).WithMessage("Máximo de caracteres é 50.");

            RuleFor(x => x.DateBirth)
                .NotEmpty().WithMessage("Data de nascimento é obrigatório.");

            RuleFor(x => x.RG)
                .Length(7).WithMessage("RG deve ter 7 digitos")
                .Must(validNumber).WithMessage("RG inválido.");  

            RuleFor(x => x.CPF)
                .Length(11).WithMessage("CPF deve ter 11 digitos")
                .Must(validNumber).WithMessage("CPF inválido."); 

            RuleFor(x => x.Address)
                .MaximumLength(100).WithMessage("Máximo de caracteres é 100.");

            RuleFor(x => x.ActiveCustomer)
                .NotEmpty().WithMessage("Status do cliente é obrigatório.");
                //.Must(validActiveCustomer).WithMessage("Utilize 1 para ativo e 0 para inativo.");
        }

        private static bool validString(string StringValid)
        {
            return Regex.IsMatch(StringValid, @"^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]+$");
        }

        private static bool validNumber(string NumberValid)
        {
            return Regex.IsMatch(NumberValid, @"^\d+$");
        }
    }
}
