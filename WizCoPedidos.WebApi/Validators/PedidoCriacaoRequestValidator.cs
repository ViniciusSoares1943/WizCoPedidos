using FluentValidation;
using WizCoPedidos.WebApi.Models.Dto;

namespace WizCoPedidos.WebApi.Validators;

public class PedidoCriacaoRequestValidator : AbstractValidator<PedidoCriacaoRequest>
{
    public PedidoCriacaoRequestValidator()
    {
        RuleFor(x => x.ClienteNome)
            .NotEmpty();

        RuleFor(x => x.ItemPedidos)
            .NotNull()
            .Must(itens => itens is not null && itens.Count > 0)
            .WithMessage("Não permitido pedido sem itens");

        RuleForEach(x => x.ItemPedidos)
            .SetValidator(new ItemPedidoRequestValidator());
    }
}
