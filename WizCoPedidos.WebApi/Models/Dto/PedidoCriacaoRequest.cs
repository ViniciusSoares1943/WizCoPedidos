namespace WizCoPedidos.WebApi.Models.Dto;

/// <summary>
/// Dados necessários para criação de um pedido.
/// </summary>
public class PedidoCriacaoRequest
{
    /// <summary>
    /// Nome do cliente que está criando o pedido.
    /// </summary>
    public required string ClienteNome { get; set; }

    /// <summary>
    /// Itens que compõem o pedido.
    /// </summary>
    public List<ItemPedidoRequest> ItemPedidos { get; set; } = [];
}
