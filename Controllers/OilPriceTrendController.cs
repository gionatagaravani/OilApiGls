using Microsoft.AspNetCore.Mvc;
using OilTrendApi.Models;
using OilTrendApi.Services;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class OilPriceTrendController : ControllerBase
{
    private readonly IBrentService _brentService;
    public OilPriceTrendController(IBrentService brentService)
    {
        _brentService = brentService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] OilPriceRequest request)
    {
        if (request == null)
        {
            return BadRequest();
        }

        if (request?.Jsonrpc != "2.0")
        {
            return BadRequest("Only JSON-RPC 2.0 supported!");

        }

        if (request?.Method != "GetOilPriceTrend")
        {
            return Ok(new JsonRpcResponse { Jsonrpc = "2.0", Id = request.Id, Error = "Method not found" });

        }

        var startDate = DateTime.TryParse(request.Params.StartDateISO8601, out var start) ? start : (DateTime?)null;
        var endDate = DateTime.TryParse(request.Params.EndDateISO8601, out var end) ? end : (DateTime?)null;

        if (!startDate.HasValue || !endDate.HasValue)
        {
            return Ok(new JsonRpcResponse { Jsonrpc = "2.0", Id = request.Id, Error = "Invalid date format. Use ISO 8601 format (ex: '2023-01-01')" });
        }
        var prices = await _brentService.GetDailyPricesAsync(startDate.Value, endDate.Value);
        
        return Ok(new JsonRpcResponse
        {
            Jsonrpc = "2.0",
            Id = request.Id,
            Result = new { prices = prices }
        });
    }
}