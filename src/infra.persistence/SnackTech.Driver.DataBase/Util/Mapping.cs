using AutoMapper;
using SnackTech.Common.Dto.DataSource;
using SnackTech.Driver.DataBase.Entities;


namespace SnackTech.Driver.DataBase.Util;

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
        CreateMap<Produto, ProdutoDto>();
        CreateMap<ProdutoDto, Produto>();

        CreateMap<Cliente, ClienteDto>();
        CreateMap<ClienteDto, Cliente>();

        CreateMap<PedidoItem, PedidoItemDto>();
        CreateMap<PedidoItemDto, PedidoItem>();

        CreateMap<Pedido, PedidoDto>();
        CreateMap<PedidoDto, Pedido>();

        CreateMap<Produto, ProdutoDto>();
        CreateMap<ProdutoDto, Produto>();

        // CreateMap<PedidoDto, Pedido>()
        //     .ConstructUsing((src, context) =>
        //         {
        //             var itens = src.Itens.Select(i => new PedidoItem(){
        //                 Id = i.Id,
        //                 Quantidade = i.Quantidade,
        //                 Valor = i.Valor,
        //                 Observacao = i.Observacao,
        //                 Produto = context.Mapper.Map<Produto>(i.Produto),
        //                 Pedido = context.Mapper.Map<Pedido>(src)
        //             } );
        //             return new PedidoDto{
        //                 Id = src.Id,
        //                 DataCriacao = src.DataCriacao,
        //                 Status = src.Status,
        //                 Cliente = context.Mapper.Map<Cliente>(src.Cliente),
        //                 Itens = itens
        //             };
        //         });

        // CreateMap<Domain.DTOs.Driven.PedidoDto, Pedido>();
    }
}
