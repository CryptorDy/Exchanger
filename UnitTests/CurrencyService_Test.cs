using Exchanger.Context;
using Exchanger.Models;
using Exchanger.Services;
using Microsoft.EntityFrameworkCore;

namespace UnitTests
{
    public class CurrencyService_Test : IDisposable
    {
        private readonly DbContextOptions<AppDbContext> _options;
        private readonly AppDbContext _dbContext;

        public CurrencyService_Test()
        {
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;

            _dbContext = new AppDbContext(_options);
        }

        [Fact]
        public async Task CreateCurrency_Test()
        {
            var currencyService = new CurrencyService(_dbContext);

            var currency = new Currency
            {
                Name = "TestCurrency",
                Code = "TST"
            };

            await currencyService.Create(currency);

            var savedCurrency = _dbContext.Currencies.FirstOrDefault(c => c.Id == currency.Id);
            Assert.NotNull(savedCurrency);
            Assert.Equal(currency.Name, savedCurrency.Name);
            Assert.Equal(currency.Code, savedCurrency.Code);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}