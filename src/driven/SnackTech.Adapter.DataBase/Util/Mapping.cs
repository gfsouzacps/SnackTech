using AutoMapper;
using SnackTech.Adapter.DataBase.Entities;

namespace SnackTech.Adapter.DataBase.Util;

public static class Mapping
{
    private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(cfg => {
            // This line ensures that internal properties are also mapped over.
            cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
            cfg.AddProfile<MappingProfile>();
        });
        var mapper = config.CreateMapper();
        return mapper;
    });

    public static IMapper Mapper => Lazy.Value;
}

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Produto, Domain.Models.Produto>()
            .ConstructUsing((src, res) =>
                {
                    return new Domain.Models.Produto(
                        src.Id,
                        src.Categoria,
                        src.Nome,
                        src.Descricao,
                        src.Valor
                    );
                });

        CreateMap<Domain.Models.Produto, Produto>();

        CreateMap<Pessoa, Domain.Models.Pessoa>()
            .ConstructUsing((src, res) =>
                {
                    return new Domain.Models.Pessoa(
                        src.Id,
                        src.Nome
                    );
                });

        CreateMap<Domain.Models.Pessoa, Pessoa>();

        CreateMap<Cliente, Domain.Models.Cliente>()
            .ConstructUsing((src, res) =>
                {
                    return new Domain.Models.Cliente(
                        new Domain.Models.Pessoa(src.Id, src.Nome),
                        src.Email,
                        src.Cpf
                    );
                });

        CreateMap<Domain.Models.Cliente, Cliente>();

        CreateMap<PedidoItem, Domain.Models.PedidoItem>()
            .ConstructUsing((src, res) =>
                {
                    return new Domain.Models.PedidoItem(
                        src.Id,
                        src.Pedido.Id,
                        src.Sequencial,
                        src.Produto.Id,
                        src.Quantidade,
                        src.Observacao
                    );
                });

        CreateMap<Domain.Models.PedidoItem, PedidoItem>();

        CreateMap<Pedido, Domain.Models.Pedido>()
            .ConstructUsing((src, context) =>
                {
                    return new Domain.Models.Pedido(
                        src.Id,
                        src.DataCriacao,
                        src.Status,
                        context.Mapper.Map<Cliente, Domain.Models.Cliente>(src.Cliente),
                        context.Mapper.Map<IEnumerable<PedidoItem>, List<Domain.Models.PedidoItem>>(src.Itens)
                    );
                });

        CreateMap<Domain.Models.Pedido, Pedido>();
    }
}
