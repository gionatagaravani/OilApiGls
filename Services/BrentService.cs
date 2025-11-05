using OilTrendApi.Models;

namespace OilTrendApi.Services;

public class BrentService: IBrentService
{
    private readonly List<BrentRecord> pricesRecords;

    public BrentService(List<BrentRecord> data)
    {
        pricesRecords = data;
    }

    public Task<List<BrentRecord>> GetDailyPricesAsync(DateTime startDate, DateTime endDate)
    {
        var data = pricesRecords
            .Where(r =>
            {
                if (!DateTime.TryParse(r.Date, out var d)) return false;
                return d.Date >= startDate.Date && d.Date <= endDate.Date;
            })
            .ToList();

        return Task.FromResult(data);
    }
}
