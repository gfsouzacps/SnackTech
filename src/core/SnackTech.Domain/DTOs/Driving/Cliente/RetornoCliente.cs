using DomainModels = SnackTech.Domain.Models;

namespace SnackTech.Domain.DTOs.Driving.Cliente

{
    public class RetornoCliente
    {
        public Guid Id {get; set;}        
        public string Nome {get; set;} = string.Empty;

        public static RetornoCliente APartirDeCliente(DomainModels.Cliente cliente)
            => new()
            {
                Id = cliente.Id,
                Nome = cliente.Nome
            };
    }
}