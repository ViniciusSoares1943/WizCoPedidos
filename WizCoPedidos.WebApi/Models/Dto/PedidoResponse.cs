using WizCoPedidos.WebApi.Models.Enum;

namespace WizCoPedidos.WebApi.Models.Dto;

/// <summary>
/// Dados retornados para um pedido.
/// </summary>
public class PedidoResponse
{
    /// <summary>
    /// Identificador único do pedido.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Nome do cliente.
    /// </summary>
    public required string ClienteNome { get; set; }

    /// <summary>
    /// Data e hora de criação do pedido.
    /// </summary>
    public DateTime DataCriacao { get; set; }

    /// <summary>
    /// Status atual do pedido.
    /// </summary>
    public StatusPedido Status { get; set; }

    /// <summary>
    /// Valor total do pedido calculado a partir dos itens.
    /// </summary>
    public decimal ValorTotal { get; set; }

    /// <summary>
    /// Itens que compõem o pedido.
    /// </summary>
    public List<ItemPedidoResponse> ItemPedidos { get; set; } = [];
}
