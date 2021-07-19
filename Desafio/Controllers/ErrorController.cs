using Microsoft.AspNetCore.Mvc;

namespace Desafio.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ErrorController : Controller
    {
        public IActionResult Error() => Problem();
    }
}
