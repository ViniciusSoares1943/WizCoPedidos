namespace WizCoPedidos.WebApi.Models.Dto;

/// <summary>
/// Dados de um item no momento da criação do pedido.
/// </summary>
public class ItemPedidoRequest
{
    /// <summary>
    /// Nome do produto.
    /// </summary>
    public required string ProdutoNome { get; set; }

    /// <summary>
    /// Quantidade do produto no pedido.
    /// </summary>
    public int Quantidade { get; set; }

    /// <summary>
    /// Preço unitário do produto.
    /// </summary>
    public decimal PrecoUnitario { get; set; }
}
