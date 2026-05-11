using WizCoPedidos.WebApi.Entidades;
using WizCoPedidos.WebApi.Models.Enum;

namespace WizCoPedidos.WebApi.Repositories.Interfaces;

public interface IPedidoRepository
{
    Task<Pedido?> ObterPorIdAsync(Guid Id);
    Task<Pedido> CriarPedidoAsync(Pedido pedido);
    Task<List<Pedido>> ObterPorStatusAsync(StatusPedido statusPedido);
    Task<Pedido> AtualizarAsync(Pedido pedido);
}
