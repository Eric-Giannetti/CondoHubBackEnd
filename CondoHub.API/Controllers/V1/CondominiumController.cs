using Microsoft.AspNetCore.Mvc;

namespace CondoHub.API.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public class CondominiumController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
