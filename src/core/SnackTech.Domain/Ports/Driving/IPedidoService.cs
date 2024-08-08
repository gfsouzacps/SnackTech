using SnackTech.Domain.Common;
using SnackTech.Domain.DTOs.Pedido;

namespace SnackTech.Domain.Ports.Driving
{
    public interface IPedidoService
    {        
        /// <summary>
        /// Cadastrar pedido a partir da identificação do cliente
        /// </summary>
        /// <param name="cpfCliente"></param>
        /// <returns>Retorna id do pedido</returns>
        Task<Result<Guid>> IniciarPedido(string cpfCliente);
        
        /// <summary>
        /// Gravar pedido com dados de cliente e lista de itens
        /// </summary>
        /// <param name="identificacao"></param>
        /// <returns></returns>
        Task<Result> FinalizarPedidoParaPagamento(string identificacao);
        
        /// <summary>
        /// Caso pedido não esteja pronto para pagamento, permitir modificar itens do pedido
        /// </summary>
        /// <param name="pedidoAtualizado"></param>
        /// <returns></returns>
        Task<Result> AtualizarPedido(AtualizacaoPedido pedidoAtualizado);
        
        /// <summary>
        /// Retornar os pedidos aguardando pagamento
        /// </summary>
        /// <returns></returns>
        Task<Result<IEnumerable<RetornoPedido>>> ListarPedidosParaPagamento();
        
        /// <summary>
        /// Retorna o pedido com id informado
        /// </summary>
        /// <param name="identificacao"></param>
        /// <returns></returns>
        Task<Result<RetornoPedido>> BuscarPorIdenticacao(string identificacao);
        
        /// <summary>
        /// Retorna o ultimo pedido do cliente com cpf informado, não permitir procurar pelo cliente anonimo
        /// </summary>
        /// <param name="cpfCliente"></param>
        /// <returns></returns>
        Task<Result<RetornoPedido>> BuscarUltimoPedidoCliente(string cpfCliente);        
    }
}