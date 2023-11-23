using Exchanger.Models;

namespace Exchanger.Services;

public interface ITransactionService
{
    Task SendTransaction(Transaction transaction);
    Task AddToBalance(ActionBalance actionBalance);
    Task RemoveFromBalance(ActionBalance actionBalance);
    Task<User> GetUserBalances(Guid userId);
}

