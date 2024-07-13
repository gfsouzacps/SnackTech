using SnackTech.Domain.Guards;

namespace SnackTech.Domain.Models
{
    public class Cliente
    {
        public Pessoa Pessoa {get; private set;}  
        public string Email {get; private set;}
        public string CPF {get; private set;}

        public Cliente(Guid id, string nome, string email, string cpf){
            Pessoa = new Pessoa(id,nome);
            
            EmailGuard.AgainstInvalidEmail(email, nameof(email));
            CpfGuard.AgainstInvalidCpf(cpf, nameof(cpf));
            Email = email;
            CPF = cpf;
        }

        public Cliente(string nome, string email, string cpf)
            :this(Guid.NewGuid(),nome,email,cpf)
        {}
    }
}