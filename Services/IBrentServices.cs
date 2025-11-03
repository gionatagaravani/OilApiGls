using OilTrendApi.Models;

namespace OilTrendApi.Services;

public interface IBrentService
{
    Task<List<BrentRecord>> GetDailyPricesAsync(DateTime startDate, DateTime endDate);
}
