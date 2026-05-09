using WizCoPedidos.WebApi.Data;
using WizCoPedidos.WebApi.Entidades;
using WizCoPedidos.WebApi.Repositories.Interfaces;

namespace WizCoPedidos.WebApi.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly AppDbContext _context;
    
    public PedidoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Pedido?> ObterPedidoPorIdAsync(int pedidoId)
    {
        return await _context.Pedidos.FindAsync(pedidoId);
    }
    
}