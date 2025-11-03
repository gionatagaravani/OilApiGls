using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class OilPriceTrendController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable> GetAll() => Ok([]);
}