using SnackTech.Domain.Common;
using SnackTech.Domain.DTOs.Pedido;

namespace SnackTech.Domain.Ports.Driven
{
    public interface IPedidoService
    {
        /*
            Iniciar Pedido
                Cadastrar pedido a partir da identificação do cliente
                Retornar id do pedido
            Finalizar pedido para pagamento
                Gravar pedido com dados de cliente e lista de itens
            Atualizar pedido
                Caso pedido não esteja pronto para pagamento, permitir modificar itens do pedido
            Listar pedidos
                Retornar somente os finalizados
            Procurar pedido por Id
            Procurar pedido por Cliente
                Não permitir procurar pelo cliente anonimo
        */
        Task<Result<Guid>> IniciarPedido(string identificacaoCliente);
        Task<Result> FinalizarPedidoParaPagamento(string identificacao);
        Task<Result> AtualizarPedido(AtualizacaoPedido pedidoAtualizado);
        Task<Result<IEnumerable<RetornoPedido>>> ListarPedidosParaPagamento();
        Task<Result<RetornoPedido>> BuscarPorIdenticacao(string identificacao);
        Task<Result<RetornoPedido>> BuscarUltimoPedidoCliente(string identificacaoCliente);        
    }
}