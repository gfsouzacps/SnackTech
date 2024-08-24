using SnackTech.Domain.Guards;

namespace SnackTech.Domain.Models
{
    public class Cliente : Pessoa
    {
        public const string CPF_CLIENTE_PADRAO = "00000000191";

        public string Email { get; private set; } = string.Empty;
        public string Cpf { get; private set; } = string.Empty;

        public Cliente(string nome, string email, string cpf)
            : base(Guid.NewGuid(), nome)
        {
            EmailGuard.AgainstInvalidEmail(email, nameof(email));
            CpfGuard.AgainstInvalidCpf(cpf, nameof(cpf));

            Email = email;
            Cpf = cpf;
        }

        public Cliente(Pessoa pessoa, string email, string cpf): base(pessoa.Id, pessoa.Nome){
            EmailGuard.AgainstInvalidEmail(email, nameof(email));
            CpfGuard.AgainstInvalidCpf(cpf, nameof(cpf));

            Email = email;
            Cpf = cpf;
        }

        private Cliente() { }

        public static implicit operator DTOs.Driven.ClienteDto(Cliente cliente)
        {
            return new DTOs.Driven.ClienteDto
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Email = cliente.Email,
                Cpf = cliente.Cpf
            };
        }

        public static implicit operator Cliente(DTOs.Driven.ClienteDto clienteDto)
        {
            return new Cliente {
                Id = clienteDto.Id, Nome = clienteDto.Nome, Email = clienteDto.Email, Cpf = clienteDto.Cpf
            };
        }
    }
}