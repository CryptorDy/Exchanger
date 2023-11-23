using Exchanger.Models;
using Exchanger.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exchanger.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExchangeController : ControllerBase
{
    private readonly IExchangeService _exchangeService;
    public ExchangeController(IExchangeService exchangeService)
    {
        _exchangeService = exchangeService;
    }

    [HttpPost("Exchange")]
    public async Task<IActionResult> Exchange(Exchange exchange)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _exchangeService.Exchange(exchange);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        

        return Ok();
    }
}
