using SnackTech.Common.Dto.Api;

namespace SnackTech.Core.Interfaces
{
    public interface IPagamentoController
    {
        Task<ResultadoOperacao> ProcessarPagamento(PagamentoDto pagamento);        
    }
}