using AutoMapper;
using WizCoPedidos.WebApi.Entidades;
using WizCoPedidos.WebApi.Models.Dto;

namespace WizCoPedidos.WebApi.Mappings;

public class PedidoMappingProfile : Profile
{
    public PedidoMappingProfile()
    {
        CreateMap<ItemPedidoRequest, ItemPedido>();
        CreateMap<PedidoCriacaoRequest, Pedido>();

        CreateMap<ItemPedido, ItemPedidoResponse>();
        CreateMap<Pedido, PedidoResponse>();
    }
}
