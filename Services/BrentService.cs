using System.Text.Json;
using OilTrendApi.Models;

namespace OilTrendApi.Services;

public class BrentService: IBrentService
{
    private readonly List<BrentRecord> pricesRecords;

    public BrentService(IConfiguration config)
    {
        // read appsettings data url
        var dataUrl = config.GetValue<string>("DataUrl") ?? string.Empty;
        
        string json;

        if (!string.IsNullOrEmpty(dataUrl)) {
            try
            {
                using var client = new System.Net.Http.HttpClient();
                json = client.GetStringAsync(dataUrl).GetAwaiter().GetResult();
            }
            catch
            {
                // get local data if url is invalid
                json = File.ReadAllText("Data/brent-daily.json");
            }
        } else {
            // get local data if url is null or empty
            json = File.ReadAllText("Data/brent-daily.json");
        }

        pricesRecords = JsonSerializer.Deserialize<List<BrentRecord>>(json);

    }

    public Task<List<BrentRecord>> GetDailyPricesAsync(DateTime startDate, DateTime endDate)
    {
        var data = pricesRecords
            .Where(r =>
            {
                if (!DateTime.TryParse(r.DateISO8601, out var d)) return false;
                return d.Date >= startDate.Date && d.Date <= endDate.Date;
            })
            .ToList();

        return Task.FromResult(data);
    }
}