using SnackTech.Domain.Guards;

namespace SnackTech.Domain.Models
{
    public class Cliente : Pessoa
    {
        public const string CPF_CLIENTE_PADRAO = "00000000191";

        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public Cliente(string nome, string email, string cpf)
            : base(Guid.NewGuid(), nome)
        {
            EmailGuard.AgainstInvalidEmail(email, nameof(email));
            CpfGuard.AgainstInvalidCpf(cpf, nameof(cpf));

            Email = email;
            Cpf = cpf;
        }
    }
}