using WizCoPedidos.WebApi.Models.Dto;
using WizCoPedidos.WebApi.Models.Enum;

namespace WizCoPedidos.WebApi.Services.Interfaces;

public interface IPedidoService
{
    Task<PedidoResponse> CriarPedido(PedidoCriacaoRequest pedidoRequest);
    Task<PedidoResponse> ObterPorId(Guid Id);
    Task<List<PedidoResponse>> ObterPorStatus(StatusPedido statusPedido);
    Task<PedidoResponse> Cancelar(Guid Id);
    Task<PedidoResponse> Pagar(Guid Id);
}
