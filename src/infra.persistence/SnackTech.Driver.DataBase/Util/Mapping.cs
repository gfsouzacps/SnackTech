using AutoMapper;
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
        CreateMap<Produto, Domain.DTOs.Driven.ProdutoDto>();
        CreateMap<Domain.DTOs.Driven.ProdutoDto, Produto>();

        CreateMap<Cliente, Domain.DTOs.Driven.ClienteDto>();
        CreateMap<Domain.DTOs.Driven.ClienteDto, Cliente>();

        CreateMap<PedidoItem, Domain.DTOs.Driven.PedidoItemDto>();
        CreateMap<Domain.DTOs.Driven.PedidoItemDto, PedidoItem>();

        CreateMap<Pedido, Domain.DTOs.Driven.PedidoDto>();
        CreateMap<Domain.DTOs.Driven.PedidoDto, Pedido>();
        

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
