using Exchanger.Models;
using Exchanger.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exchanger.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BalanceController : ControllerBase
{
    private readonly ITransactionService _transactionService;
    public BalanceController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost("SendTransaction")]
    public async Task<IActionResult> SendTransaction(Transaction transaction)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            await _transactionService.SendTransaction(transaction);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
        return Ok();
    }

    [HttpPost("AddToBalance")]
    public async Task<IActionResult> AddToBalance(ActionBalance actionBalance)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _transactionService.AddToBalance(actionBalance);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok();
    }

    [HttpPost("RemoveFromBalance")]
    public async Task<IActionResult> RemoveFromBalance(ActionBalance actionBalance)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _transactionService.RemoveFromBalance(actionBalance);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok();
    }

    [HttpGet("UserBalances/{userId}")]
    public async Task<IActionResult> UserBalances(string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("Value parameter is required");
        }

        User user;

        try
        {
            user = await _transactionService.GetUserBalances(new Guid(userId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok(user);
    }
}
