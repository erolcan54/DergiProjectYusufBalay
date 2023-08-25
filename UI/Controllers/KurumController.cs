using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [Authorize]
    public class KurumController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
