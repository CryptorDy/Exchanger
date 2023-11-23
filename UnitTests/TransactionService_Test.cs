using Exchanger.Context;
using Exchanger.Models;
using Exchanger.Services;
using Microsoft.EntityFrameworkCore;

namespace UnitTests;

// Добавьте необходимые директивы using

public class TransactionService_Test : IDisposable
{
    private readonly DbContextOptions<AppDbContext> _options;
    private readonly AppDbContext _dbContext;
    public TransactionService_Test()
    {
        _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDbForTesting")
            .Options;

        _dbContext = new AppDbContext(_options);
    }

    [Fact]
    public async Task SendTransaction_Test()
    {
        var userService = new UserService(_dbContext);
        var transactionService = new TransactionService(_dbContext, userService);

        var trasaction = new Transaction
        {
            CurrencyCode = "USD",
            SenderUserid = Guid.NewGuid(),
            RecipientUserId = Guid.NewGuid(),
            Type = TransactionType.TransferToUser,
            Amount = 10
        };

        await transactionService.SendTransaction(trasaction);

        var dbTrasaction = _dbContext.Transactions.FirstOrDefault(c => c.SenderUserid == trasaction.SenderUserid);

        Assert.NotNull(dbTrasaction);
        Assert.Equal(trasaction.RecipientUserId, dbTrasaction.RecipientUserId);
    }

    [Fact]
    public async Task SendTransaction_WithInvalidId_Test()
    {
        var userService = new UserService(_dbContext);
        var transactionService = new TransactionService(_dbContext, userService);

        var trasaction = new Transaction
        {
            RecipientUserId = Guid.NewGuid(),
            Type = TransactionType.TransferToUser,
            Amount = 10
        };

        await Assert.ThrowsAsync<Exception>(async () =>
        {
            await transactionService.SendTransaction(trasaction); 
        });
        
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
