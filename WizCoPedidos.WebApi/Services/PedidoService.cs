using AutoMapper;
using Microsoft.Extensions.Logging;
using WizCoPedidos.WebApi.Entidades;
using WizCoPedidos.WebApi.Exceptions;
using WizCoPedidos.WebApi.Models.Dto;
using WizCoPedidos.WebApi.Models.Enum;
using WizCoPedidos.WebApi.Repositories.Interfaces;
using WizCoPedidos.WebApi.Services.Interfaces;

namespace WizCoPedidos.WebApi.Services;

public class PedidoService(
    IPedidoRepository pedidoRepository,
    IMapper mapper,
    ILogger<PedidoService> logger) : IPedidoService
{

    public async Task<PedidoResponse> CriarPedido(PedidoCriacaoRequest pedidoRequest)
    {
        logger.LogInformation("Criando pedido para cliente {ClienteNome} com {QuantidadeItens} itens", pedidoRequest.ClienteNome, pedidoRequest.ItemPedidos.Count);

        var pedido = mapper.Map<Pedido>(pedidoRequest);
        pedido.DataCriacao = DateTime.UtcNow;
        pedido.Status = StatusPedido.Novo;
        pedido.ValorTotal = pedido.ItemPedidos.Sum(item => item.Quantidade * item.PrecoUnitario);
        pedido.ItemPedidos.ForEach(i => i.PedidoId = pedido.Id);

        var pedidoCriado = await pedidoRepository.CriarPedidoAsync(pedido);
        logger.LogInformation("Pedido {PedidoId} criado com sucesso. Valor total: {ValorTotal}", pedidoCriado.Id, pedidoCriado.ValorTotal);

        return mapper.Map<PedidoResponse>(pedidoCriado);
    }

    public async Task<PedidoResponse> ObterPorId(Guid Id)
    {
        logger.LogInformation("Consultando pedido por id {PedidoId}", Id);

        if (Id == Guid.Empty)
        {
            throw new BadRequestException("Identificador de pedido inválido");
        }

        var pedido = await pedidoRepository.ObterPorIdAsync(Id);

        if (pedido is null)
        {
            throw new NotFoundException("Pedido não encontrado pelo identificador informado");
        }
        
        return mapper.Map<PedidoResponse>(pedido);
    }
    
    public async Task<List<PedidoResponse>> ObterPorStatus(StatusPedido statusPedido)
    {
        logger.LogInformation("Consultando pedidos por status {StatusPedido}", statusPedido);
        var pedidos = await pedidoRepository.ObterPorStatusAsync(statusPedido);
        logger.LogInformation("Consulta por status {StatusPedido} retornou {QuantidadePedidos} pedidos", statusPedido, pedidos.Count);
        return mapper.Map<List<PedidoResponse>>(pedidos);
    }
    
    public async Task<PedidoResponse> Cancelar(Guid Id)
    {
        logger.LogInformation("Solicitação para cancelar pedido {PedidoId}", Id);

        if (Id == Guid.Empty)
            throw new BadRequestException("Identificador de pedido inválido");

        var pedido = await pedidoRepository.ObterPorIdAsync(Id);
        if (pedido is null)
            throw new NotFoundException("Pedido não encontrado pelo identificador informado");

        if (pedido.Status == StatusPedido.Pago)
            throw new BadRequestException("Pedido com pagamento realizado não pode ser cancelado");

        if (pedido.Status == StatusPedido.Cancelado)
            throw new BadRequestException("Pedido já está cancelado");

        pedido.Status = StatusPedido.Cancelado;

        await pedidoRepository.AtualizarAsync(pedido);
        logger.LogInformation("Pedido {PedidoId} cancelado com sucesso", Id);
        
        return mapper.Map<PedidoResponse>(pedido);
    }

    public async Task<PedidoResponse> Pagar(Guid Id)
    {
        logger.LogInformation("Solicitação para pagar pedido {PedidoId}", Id);

        if (Id == Guid.Empty)
            throw new BadRequestException("Identificador de pedido inválido");

        var pedido = await pedidoRepository.ObterPorIdAsync(Id);
        if (pedido is null)
            throw new NotFoundException("Pedido não encontrado pelo identificador informado");

        if (pedido.Status == StatusPedido.Pago)
            throw new BadRequestException("Pedido já está pago");

        if (pedido.Status == StatusPedido.Cancelado)
            throw new BadRequestException("Pedido cancelado não pode ser pago");

        pedido.Status = StatusPedido.Pago;

        await pedidoRepository.AtualizarAsync(pedido);
        logger.LogInformation("Pedido {PedidoId} marcado como pago com sucesso", Id);

        return mapper.Map<PedidoResponse>(pedido);
    }
}
