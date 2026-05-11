using Microsoft.EntityFrameworkCore;
using WizCoPedidos.WebApi.Data;
using WizCoPedidos.WebApi.Entidades;
using WizCoPedidos.WebApi.Models.Enum;
using WizCoPedidos.WebApi.Repositories.Interfaces;

namespace WizCoPedidos.WebApi.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly AppDbContext _context;
    
    public PedidoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Pedido> CriarPedidoAsync(Pedido pedido)
    {
        await _context.Pedidos.AddAsync(pedido);
        await _context.SaveChangesAsync();
        return pedido;
    }
    
    public async Task<Pedido?> ObterPorIdAsync(Guid Id)
    {
        return await _context.Pedidos
            .Include(p => p.ItemPedidos)
            .FirstOrDefaultAsync(p => p.Id == Id);
    }
    
    public async Task<List<Pedido>> ObterPorStatusAsync(StatusPedido statusPedido)
    {
        return await _context.Pedidos
            .AsNoTracking()
            .Include(p => p.ItemPedidos)
            .Where(p => p.Status == statusPedido)
            .ToListAsync();
    }
    
    public async Task<Pedido> AtualizarAsync(Pedido pedido)
    {
        _context.Pedidos.Update(pedido);
        await _context.SaveChangesAsync();
        return pedido;
    }
}
