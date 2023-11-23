using Exchanger.Context;
using Exchanger.Models;
using Exchanger.Services;
using Microsoft.EntityFrameworkCore;

namespace UnitTests
{
    using System;
    using System.Threading.Tasks;
    using Moq;
    using Xunit;

    public class ExchangeService_Test
    {
        private readonly DbContextOptions<AppDbContext> _options;
        private readonly AppDbContext _dbContext;
        private readonly Mock<ITransactionService> mockTransactionService = new Mock<ITransactionService>();
        private readonly ExchangeService _exchangeService;

        public ExchangeService_Test()
        {
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;

            _dbContext = new AppDbContext(_options);

            _exchangeService = new ExchangeService(_dbContext, mockTransactionService.Object);
        }

        [Fact]
        public async Task Exchange_Test()
        {

            var exchange = new Exchange
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                CurrencyCodeFrom = "USD",
                CurrencyCodeTo = "EUR",
                Amount = 100,
                Price = 1.2m,
                Fee = 0.5m
            };

            await _exchangeService.Exchange(exchange);

            var savedExchange = _dbContext.Exchanges.FirstOrDefault(c => c.Id == exchange.Id);
            Assert.NotNull(savedExchange);
            Assert.Equal(exchange.CurrencyCodeFrom, savedExchange.CurrencyCodeFrom);
            Assert.Equal(exchange.CurrencyCodeTo, savedExchange.CurrencyCodeTo);
        }
    }

}