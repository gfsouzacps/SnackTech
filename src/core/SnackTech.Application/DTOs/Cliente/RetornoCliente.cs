using DomainModels = SnackTech.Domain.Models;

namespace SnackTech.Application.DTOs.Cliente
{
    public class RetornoCliente
    {
        public Guid Id {get; set;}        
        public string Nome {get; set;} = string.Empty;

        public static RetornoCliente APartirDeCliente(DomainModels.Cliente cliente)
            => new()
            {
                Id = cliente.RecuperarUid(),
                Nome = cliente.RecuperarNome()
            };
    }
}