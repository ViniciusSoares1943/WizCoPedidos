namespace WizCoPedidos.WebApi.Entidades;

public class ItemPedido
{
    public Guid Id { get; set; } = new Guid();
    public Guid PedidoId { get; set; }
    public string ProdutoNome { get; set; }
    public decimal Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
}