using Exchanger.Context;
using Exchanger.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Exchanger.Services;

public class ExchangeService: IExchangeService
{
    private readonly AppDbContext _db;
    private readonly ITransactionService _transactionService;
    public ExchangeService(AppDbContext context, ITransactionService transactionService)
    {
        _db = context;
        _transactionService = transactionService;
    }

    public async Task Exchange(Exchange exchange)
    {
            exchange.Id = Guid.NewGuid();

            _db.Exchanges.Add(exchange);
            await _db.SaveChangesAsync();

            var exchangeAmount = exchange.Amount / exchange.Price;

            var exchangeFee = exchangeAmount * exchange.Fee;

            var exchangeAmountWithFee = exchangeAmount - exchangeFee;

            await ExecuteExchange(exchange, exchangeAmountWithFee, exchangeFee);
    }

    private async Task ExecuteExchange(Exchange exchange, decimal exchangeAmountWithFee, decimal exchangeFee)
    {
        var removeBalance = new Transaction
        {
            CurrencyCode = exchange.CurrencyCodeFrom,
            SenderUserid = exchange.UserId,
            RecipientUserId = Transaction.Exchanger,
            Amount = exchange.Amount,
            Type = TransactionType.Exchange,
            ExchangeId = exchange.Id
        };
        await _transactionService.SendTransaction(removeBalance);

        var addBalance = new Transaction
        {
            CurrencyCode = exchange.CurrencyCodeTo,
            SenderUserid = Transaction.Exchanger,
            RecipientUserId = exchange.UserId,
            Amount = exchangeAmountWithFee,
            Type = TransactionType.Exchange,
            ExchangeId = exchange.Id
        };
        await _transactionService.SendTransaction(addBalance);

        var fee = new Transaction
        {
            CurrencyCode = exchange.CurrencyCodeTo,
            SenderUserid = Transaction.Exchanger,
            RecipientUserId = Transaction.FeeRecipient,
            Amount = exchangeFee,
            Type = TransactionType.Fee,
            ExchangeId = exchange.Id
        };
        await _transactionService.SendTransaction(fee);
    }
}
