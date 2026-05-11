namespace WizCoPedidos.WebApi.Models.Dto;

/// <summary>
/// Dados retornados de um item de pedido.
/// </summary>
public class ItemPedidoResponse
{
    /// <summary>
    /// Nome do produto.
    /// </summary>
    public required string ProdutoNome { get; set; }

    /// <summary>
    /// Quantidade do produto.
    /// </summary>
    public int Quantidade { get; set; }

    /// <summary>
    /// Preço unitário do produto.
    /// </summary>
    public decimal PrecoUnitario { get; set; }
}
