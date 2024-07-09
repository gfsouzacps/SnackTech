using SnackTech.Domain.Guards;

namespace SnackTech.Domain.Models
{
    public class Cliente
    {
        public Guid Id {get; private set;}        
        public string Email {get; private set;}
        public string Nome {get; private set;}
        public string CPF {get; private set;}

        public Cliente(Guid id, string nome, string email, string cpf){
            CustomGuards.AgainstStringNullOrWhiteSpace(nome, nameof(nome));
            EmailGuard.AgainstInvalidEmail(email, nameof(email));
            CpfGuard.AgainstInvalidCpf(cpf, nameof(cpf));

            Id = id;
            Email = email;
            Nome = nome;
            CPF = cpf;
        }

        public Cliente(string nome, string email, string cpf)
            :this(Guid.NewGuid(),nome,email,cpf)
        {}
    }
}