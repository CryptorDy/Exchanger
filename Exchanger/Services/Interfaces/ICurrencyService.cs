using Exchanger.Models;

namespace Exchanger.Services;

public interface ICurrencyService
{
    Task Create(Currency currency);
}
