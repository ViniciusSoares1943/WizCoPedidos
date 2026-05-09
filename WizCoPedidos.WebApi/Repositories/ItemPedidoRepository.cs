using WizCoPedidos.WebApi.Data;
using WizCoPedidos.WebApi.Repositories.Interfaces;

namespace WizCoPedidos.WebApi.Repositories;

public class ItemPedidoRepository : IItemPedidoRepository
{
    private readonly AppDbContext _context;
    
    public ItemPedidoRepository(AppDbContext context)
    {
        _context = context;
    }
}