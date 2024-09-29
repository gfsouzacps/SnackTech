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

        // CreateMap<Pedido, Domain.DTOs.Driven.PedidoDto>()
        //     .ConstructUsing((src, context) =>
        //         {
        //             return new Domain.DTOs.Driven.PedidoDto{
        //                 Id = src.Id,
        //                 DataCriacao = src.DataCriacao,
        //                 Status = src.Status,
        //                 Cliente = context.Mapper.Map<Cliente, Domain.DTOs.Driven.ClienteDto>(src.Cliente),
        //                 Itens = context.Mapper.Map<IEnumerable<PedidoItem>, List<Domain.DTOs.Driven.PedidoItemDto>>(src.Itens)
        //             };
        //         });

        // CreateMap<Domain.DTOs.Driven.PedidoDto, Pedido>();
    }
}
