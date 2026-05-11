namespace WizCoPedidos.WebApi.Models.Enum;

/// <summary>
/// Status possíveis para um pedido.
/// </summary>
public enum StatusPedido
{
    /// <summary>
    /// Pedido recém-criado.
    /// </summary>
    Novo = 1,
    /// <summary>
    /// Pagamento do pedido foi confirmado.
    /// </summary>
    Pago = 2,
    /// <summary>
    /// Pedido foi cancelado.
    /// </summary>
    Cancelado = 3,
}
