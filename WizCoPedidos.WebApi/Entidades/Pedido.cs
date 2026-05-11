using WizCoPedidos.WebApi.Models.Enum;

namespace WizCoPedidos.WebApi.Entidades;

public class Pedido
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string ClienteNome { get; set; }
    public DateTime DataCriacao { get; set; }
    public StatusPedido Status { get; set; }
    public decimal ValorTotal { get; set; }
    public List<ItemPedido> ItemPedidos { get; set; } = [];
}