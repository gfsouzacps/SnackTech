using System;
using SnackTech.Common.Dto;

namespace SnackTech.Common.Interface;

public interface IProdutoDataSource
{
    Task<ProdutoDto> ProcurarPorNome(string nome);
    Task<ProdutoDto> ProcurarPorGuid(Guid identificacao);
    Task<IEnumerable<ProdutoDto>> ListarPorCategoria(int categoria);
    Task Inserir(ProdutoDto produtoDto);
    Task Atualizar(ProdutoDto produtoDto);
    Task RemoverPorGuid(Guid identificacao);
}
