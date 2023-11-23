using Exchanger.Models;

namespace Exchanger.Services;

public interface IExchangeService
{
    Task Exchange(Exchange exchange);
}

