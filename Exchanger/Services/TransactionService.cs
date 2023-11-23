using Exchanger.Context;
using Exchanger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Exchanger.Services
{
    public class TransactionService: ITransactionService
    {
        private readonly AppDbContext _db;
        private readonly IUserService _userService;
        public TransactionService(AppDbContext context, IUserService userService) 
        {
            _db = context;
            _userService = userService;
        }

        public async Task SendTransaction(Transaction transaction)
        {
            var balance = await GetBalance(transaction.SenderUserid, transaction.CurrencyCode);

            if (transaction.SenderUserid != Transaction.Exchanger &&
                transaction.SenderUserid != Transaction.System &&
                balance < transaction.Amount)
                throw new InvalidOperationException("Insufficient funds on balance");

            if (!await _db.Currencies.AnyAsync(c => c.Code == transaction.CurrencyCode))
                throw new Exception("Currency not found");

            try
            {
                transaction.Id = Guid.NewGuid();

                _db.Transactions.Add(transaction);
                await _db.SaveChangesAsync();

            } catch (Exception ex)
            {
                throw new Exception("Transaction could not be sent: " + ex.Message);
            }
            
        }

        public async Task AddToBalance(ActionBalance actionBalance)
        {
            var transaction = new Transaction 
            { 
                CurrencyCode = actionBalance.CurrencyCode,
                SenderUserid = Transaction.System,
                RecipientUserId = actionBalance.UserId,
                Amount = actionBalance.Amount,
                Type = TransactionType.System
            };

            await SendTransaction(transaction);
        }

        public async Task RemoveFromBalance(ActionBalance actionBalance)
        {
            var transaction = new Transaction
            {
                CurrencyCode = actionBalance.CurrencyCode,
                SenderUserid = actionBalance.UserId, 
                RecipientUserId = Transaction.System,
                Amount = actionBalance.Amount,
                Type = TransactionType.System
            };

            await SendTransaction(transaction);
        }

        public async Task<User> GetUserBalances(Guid userId)
        {
            var user = await _userService.GetUser(userId);
            if(user == null)
                throw new Exception("User not found: " + userId.ToString());

            var currencies = _db.Currencies.ToList();

            user.Balances = new List<Balance>();

            foreach (var currency in currencies)
            {
                var amount = await GetBalance(userId, currency.Code);

                var balance = new Balance { CurrencyCode = currency.Code, Amount = amount };

                user.Balances.Add(balance);
            }

            return user;
        }

        private async Task<decimal> GetBalance(Guid userId, string currencyCode)
        {
            decimal sumReceived = await _db.Transactions
                                        .Where(c => c.RecipientUserId == userId && c.CurrencyCode == currencyCode)
                                        .Select(c => c.Amount).SumAsync();

            decimal sumSent = await _db.Transactions
                                    .Where(c => c.SenderUserid == userId && c.CurrencyCode == currencyCode)
                                    .Select(c => c.Amount).SumAsync();

            decimal amount = sumReceived - sumSent;

            if (amount < 0)
                throw new Exception($"The balance is less than zero. UserId: {userId}");

            return amount;
        }
    }
}
