using OilTrendApi.Models;
using OilTrendApi.Services;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dataUrl = builder.Configuration.GetValue<string>("DataUrl");
string json;
if (!string.IsNullOrEmpty(dataUrl)) {
    try
    {
        using var client = new System.Net.Http.HttpClient();
        json = client.GetStringAsync(dataUrl).GetAwaiter().GetResult();
        Console.WriteLine("Downloaded successfully!");
    }
    catch
    {
        // get local data if url is invalid
        json = File.ReadAllText("Data/brent-daily.json");
        Console.WriteLine("The url is invalid!");
    }
} else {
    // get local data if url is null or empty
    json = File.ReadAllText("Data/brent-daily.json");
    Console.WriteLine("The url is null or empty!");
}

var data = JsonSerializer.Deserialize<List<BrentRecord>>(json)!;

// Register custom services
builder.Services.AddSingleton<IBrentService>(new BrentService(data));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
