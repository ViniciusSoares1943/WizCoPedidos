using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using WizCoPedidos.WebApi.Entidades;
using WizCoPedidos.WebApi.Exceptions;
using WizCoPedidos.WebApi.Mappings;
using WizCoPedidos.WebApi.Models.Dto;
using WizCoPedidos.WebApi.Models.Enum;
using WizCoPedidos.WebApi.Repositories.Interfaces;
using WizCoPedidos.WebApi.Services;
using Xunit;

namespace WizCoPedidos.WebApi.Tests.Services;

public class PedidoServiceTests
{
    private readonly Mock<IPedidoRepository> _pedidoRepositoryMock = new();
    private readonly Mock<ILogger<PedidoService>> _loggerMock = new();
    private readonly IMapper _mapper;

    public PedidoServiceTests()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<PedidoMappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task CriarPedido_DeveCalcularValorTotalECriarComStatusNovo()
    {
        var sut = CriarSut();
        var request = new PedidoCriacaoRequest
        {
            ClienteNome = "Maria",
            ItemPedidos =
            [
                new ItemPedidoRequest { ProdutoNome = "Produto A", Quantidade = 2, PrecoUnitario = 10m },
                new ItemPedidoRequest { ProdutoNome = "Produto B", Quantidade = 1, PrecoUnitario = 5m }
            ]
        };

        _pedidoRepositoryMock
            .Setup(r => r.CriarPedidoAsync(It.IsAny<Pedido>()))
            .ReturnsAsync((Pedido p) => p);

        var response = await sut.CriarPedido(request);

        Assert.Equal(StatusPedido.Novo, response.Status);
        Assert.Equal(25m, response.ValorTotal);
        Assert.Equal("Maria", response.ClienteNome);
        Assert.Equal(2, response.ItemPedidos.Count);

        _pedidoRepositoryMock.Verify(r => r.CriarPedidoAsync(It.IsAny<Pedido>()), Times.Once);
    }

    [Fact]
    public async Task ObterPorId_ComGuidVazio_DeveLancarBadRequestException()
    {
        var sut = CriarSut();

        var act = async () => await sut.ObterPorId(Guid.Empty);

        var exception = await Assert.ThrowsAsync<BadRequestException>(act);
        Assert.Equal("Identificador de pedido inválido", exception.Message);
    }

    [Fact]
    public async Task ObterPorId_QuandoNaoEncontrado_DeveLancarNotFoundException()
    {
        var sut = CriarSut();
        var id = Guid.NewGuid();

        _pedidoRepositoryMock
            .Setup(r => r.ObterPorIdAsync(id))
            .ReturnsAsync((Pedido?)null);

        var act = async () => await sut.ObterPorId(id);

        var exception = await Assert.ThrowsAsync<NotFoundException>(act);
        Assert.Equal("Pedido não encontrado pelo identificador informado", exception.Message);
    }

    [Fact]
    public async Task Cancelar_QuandoPedidoPago_DeveLancarBadRequestException()
    {
        var sut = CriarSut();
        var id = Guid.NewGuid();

        _pedidoRepositoryMock
            .Setup(r => r.ObterPorIdAsync(id))
            .ReturnsAsync(new Pedido { Id = id, ClienteNome = "João", Status = StatusPedido.Pago });

        var act = async () => await sut.Cancelar(id);

        var exception = await Assert.ThrowsAsync<BadRequestException>(act);
        Assert.Equal("Pedido com pagamento realizado não pode ser cancelado", exception.Message);
    }

    [Fact]
    public async Task Pagar_QuandoPedidoCancelado_DeveLancarBadRequestException()
    {
        var sut = CriarSut();
        var id = Guid.NewGuid();

        _pedidoRepositoryMock
            .Setup(r => r.ObterPorIdAsync(id))
            .ReturnsAsync(new Pedido { Id = id, ClienteNome = "Ana", Status = StatusPedido.Cancelado });

        var act = async () => await sut.Pagar(id);

        var exception = await Assert.ThrowsAsync<BadRequestException>(act);
        Assert.Equal("Pedido cancelado não pode ser pago", exception.Message);
    }

    [Fact]
    public async Task Pagar_QuandoPedidoValido_DeveAtualizarStatusParaPago()
    {
        var sut = CriarSut();
        var id = Guid.NewGuid();
        var pedido = new Pedido
        {
            Id = id,
            ClienteNome = "Carlos",
            Status = StatusPedido.Novo,
            ItemPedidos = []
        };

        _pedidoRepositoryMock
            .Setup(r => r.ObterPorIdAsync(id))
            .ReturnsAsync(pedido);

        _pedidoRepositoryMock
            .Setup(r => r.AtualizarAsync(It.IsAny<Pedido>()))
            .ReturnsAsync((Pedido p) => p);

        var response = await sut.Pagar(id);

        Assert.Equal(StatusPedido.Pago, response.Status);
        _pedidoRepositoryMock.Verify(r => r.AtualizarAsync(It.Is<Pedido>(p => p.Status == StatusPedido.Pago)), Times.Once);
    }

    private PedidoService CriarSut()
    {
        return new PedidoService(_pedidoRepositoryMock.Object, _mapper, _loggerMock.Object);
    }
}
