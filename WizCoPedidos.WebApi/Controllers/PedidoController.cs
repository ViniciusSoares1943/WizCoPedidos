using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WizCoPedidos.WebApi.Models.Dto;
using WizCoPedidos.WebApi.Models.Enum;
using WizCoPedidos.WebApi.Services.Interfaces;

namespace WizCoPedidos.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class PedidoController(IPedidoService pedidoService) : ControllerBase
{
    /// <summary>
    /// Cria um novo pedido com seus itens.
    /// </summary>
    /// <param name="pedidoRequest">Dados de criação do pedido.</param>
    /// <returns>Pedido criado.</returns>
    /// <response code="201">Pedido criado com sucesso.</response>
    /// <response code="400">Dados inválidos para criação do pedido.</response>
    /// <response code="500">Erro interno do servidor.</response>
    [HttpPost]
    [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PedidoResponse>> CriarPedido([FromBody] PedidoCriacaoRequest pedidoRequest)
    {
        var pedidoCriado = await pedidoService.CriarPedido(pedidoRequest);
        return CreatedAtAction(nameof(ObterPorId), new { Id = pedidoCriado.Id }, pedidoCriado);
    }
    
    /// <summary>
    /// Obtém um pedido pelo identificador.
    /// </summary>
    /// <param name="Id">Identificador do pedido.</param>
    /// <returns>Pedido encontrado.</returns>
    /// <response code="200">Pedido encontrado com sucesso.</response>
    /// <response code="400">Identificador inválido.</response>
    /// <response code="404">Pedido não encontrado.</response>
    /// <response code="500">Erro interno do servidor.</response>
    [HttpGet("{Id:guid}")]
    [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PedidoResponse>> ObterPorId(Guid Id)
    {
        var pedido = await pedidoService.ObterPorId(Id);
        return Ok(pedido);
    }
    
    /// <summary>
    /// Lista pedidos por status.
    /// </summary>
    /// <param name="statusPedido">Status do pedido para filtro (Novo, Pago, Cancelado).</param>
    /// <returns>Lista de pedidos no status informado.</returns>
    /// <response code="200">Pedidos retornados com sucesso.</response>
    /// <response code="204">Nenhum pedido encontrado para o status informado.</response>
    /// <response code="500">Erro interno do servidor.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<PedidoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PedidoResponse>> ObterPorStatus([FromQuery] StatusPedido statusPedido)
    {
        var pedidos = await pedidoService.ObterPorStatus(statusPedido);

        if (!pedidos.Any())
            return NoContent();
            
        return Ok(pedidos);
    }
    
    /// <summary>
    /// Cancela um pedido existente.
    /// </summary>
    /// <param name="Id">Identificador do pedido.</param>
    /// <returns>Pedido atualizado com status cancelado.</returns>
    /// <response code="200">Pedido cancelado com sucesso.</response>
    /// <response code="400">Operação inválida para o estado atual do pedido.</response>
    /// <response code="404">Pedido não encontrado.</response>
    /// <response code="500">Erro interno do servidor.</response>
    [HttpPut("{Id:guid}/cancelar")]
    [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PedidoResponse>> Cancelar(Guid Id)
    {
        var pedidos = await pedidoService.Cancelar(Id);
        return Ok(pedidos);
    }

    /// <summary>
    /// Marca um pedido existente como pago.
    /// </summary>
    /// <param name="Id">Identificador do pedido.</param>
    /// <returns>Pedido atualizado com status pago.</returns>
    /// <response code="200">Pedido marcado como pago com sucesso.</response>
    /// <response code="400">Operação inválida para o estado atual do pedido.</response>
    /// <response code="404">Pedido não encontrado.</response>
    /// <response code="500">Erro interno do servidor.</response>
    [HttpPut("{Id:guid}/pagar")]
    [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PedidoResponse>> Pagar(Guid Id)
    {
        var pedido = await pedidoService.Pagar(Id);
        return Ok(pedido);
    }
}
