using WizCoPedidos.WebApi.Models.Enum;

namespace WizCoPedidos.WebApi.Entidades;

public class Pedido
{
    public Guid Id { get; set; } = new Guid();
    public string ClienteNome { get; set; }
    public DateTime DataCriacao { get; set; }
    public StatusPedido Status { get; set; }
    public decimal ValorTotal { get; set; }
}