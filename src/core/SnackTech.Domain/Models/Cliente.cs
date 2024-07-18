using SnackTech.Domain.Guards;

namespace SnackTech.Domain.Models
{
    public class Cliente
    {
        public Guid Id { get; set; }
        public Pessoa Pessoa { get; private set; } = null!;
        public string Email {get; private set;}
        public string Cpf {get; private set;}
        public IList<Pedido>? Pedidos { get; private set; }

        private Cliente(Guid id, string nome, string email, string cpf)
            :this(new Pessoa(id, nome), email, cpf)
        {
            Id = id;
        }

        public Cliente(string nome, string email, string cpf)
            :this(Guid.NewGuid(),nome,email,cpf)
        {}

        public Cliente(Pessoa pessoa, string email, string cpf)
        {
            CustomGuards.AgainstObjectNull(pessoa, nameof(pessoa));
            EmailGuard.AgainstInvalidEmail(email, nameof(email));
            CpfGuard.AgainstInvalidCpf(cpf, nameof(cpf));

            Pessoa = pessoa;
            Email = email;
            Cpf = cpf;
        }
    }
}