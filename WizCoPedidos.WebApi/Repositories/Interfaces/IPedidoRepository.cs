using WizCoPedidos.WebApi.Entidades;

namespace WizCoPedidos.WebApi.Repositories.Interfaces;

public interface IPedidoRepository
{
    Task<Pedido?> ObterPedidoPorIdAsync(int pedidoId);
}