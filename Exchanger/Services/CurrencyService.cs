using Exchanger.Context;
using Exchanger.Models;

namespace Exchanger.Services
{
    public class CurrencyService: ICurrencyService
    {
        private AppDbContext _db;
        public CurrencyService(AppDbContext context) 
        {
            _db = context;
        }

        public async Task Create(Currency currency)
        {
            currency.Id = Guid.NewGuid();

            _db.Currencies.Add(currency);
            await _db.SaveChangesAsync();
        }
    }
}
