using Microsoft.AspNetCore.Mvc;

namespace CondoHub.API.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public class CondominiumManagerController : Controller
{
    [HttpGet]
    public IActionResult GetManagers()
    {
        return Ok(new[] { "Manager1", "Manager2" });
    }
}
