using Exchanger.Models;
using Exchanger.Services;
using Microsoft.AspNetCore.Mvc;

namespace Exchanger.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CurrencyController : ControllerBase
{
    private readonly ICurrencyService _currencyService;

    public CurrencyController(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(Currency currency)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _currencyService.Create(currency);

        return Ok();
    }
}
