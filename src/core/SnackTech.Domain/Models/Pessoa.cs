using SnackTech.Domain.Guards;

namespace SnackTech.Domain.Models
{
    public class Pessoa
    {
        public Guid Id {get; private set;}
        public string Nome {get; private set;}

        public Pessoa(Guid id, string nome){
            CustomGuards.AgainstStringNullOrWhiteSpace(nome, nameof(nome));

            Id = id;
            Nome = nome;
        }
    }
}