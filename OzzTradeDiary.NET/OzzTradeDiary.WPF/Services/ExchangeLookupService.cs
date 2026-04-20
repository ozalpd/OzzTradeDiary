using TD.Models;
using TD.SQLite;

namespace TD.WPF.Services;

internal class ExchangeLookupService : IExchangeLookupService
{
    private readonly IExchangeRepository _exchangeRepository;

    public ExchangeLookupService(IExchangeRepository exchangeRepository)
    {
        _exchangeRepository = exchangeRepository;
    }

    public async Task<IReadOnlyList<Exchange>> GetActiveExchangesAsync()
    {
        var exchanges = await _exchangeRepository.GetAllAsync(isActive: true);
        return exchanges.ToList();
    }
}
