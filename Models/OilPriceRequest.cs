public class OilPriceRequest
{
    public int Id { get; set; }
    public string Jsonrpc { get; set; } = default!;
    public string Method { get; set; } = default!;
    public OilPriceParams Params { get; set; } = default!;
}

public class OilPriceParams
{
    public string StartDateISO8601 { get; set; } = default!;
    public string EndDateISO8601   { get; set; } = default!;
}