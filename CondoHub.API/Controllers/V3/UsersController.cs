using Microsoft.AspNetCore.Mvc;

namespace CondoHub.API.Controllers.V3;

[ApiController]
[Route("api/v3/[controller]")]
public class UsersController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}