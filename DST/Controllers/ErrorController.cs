using Microsoft.AspNetCore.Mvc;

namespace DST.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View(); // "Index"
        }
    }
}
