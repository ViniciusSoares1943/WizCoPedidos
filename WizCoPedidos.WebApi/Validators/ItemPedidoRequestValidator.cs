using FluentValidation;
using WizCoPedidos.WebApi.Models.Dto;

namespace WizCoPedidos.WebApi.Validators;

public class ItemPedidoRequestValidator : AbstractValidator<ItemPedidoRequest>
{
    public ItemPedidoRequestValidator()
    {
        RuleFor(x => x.ProdutoNome)
            .NotEmpty()
            .WithMessage("ProdutoNome é obrigatório");

        RuleFor(x => x.Quantidade)
            .GreaterThan(0)
            .WithMessage("Quantidade deve ser maior que 0");

        RuleFor(x => x.PrecoUnitario)
            .GreaterThan(0)
            .WithMessage("PrecoUnitario deve ser maior que 0");
    }
}
