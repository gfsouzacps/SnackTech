using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackTech.Application.Interfaces
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
    }
}