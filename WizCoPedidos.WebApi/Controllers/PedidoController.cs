using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WizCoPedidos.WebApi.Services.Interfaces;

namespace WizCoPedidos.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class PedidoController(IPedidoService _pedidoService) : ControllerBase
{
    
}