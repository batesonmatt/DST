using DST.Models.DataLayer.Repositories;
using DST.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DST.Controllers
{
    public class HomeController : Controller
    {
        #region "Methods"

        public IActionResult Index([FromServices] IHomeUnitOfWork data)
        {
            HomeViewModel model = new()
            {
                DsoItems = data.GetRandomDsoItems(5)
            };

            return View(model); // "Index"
        }

        public IActionResult About()
        {
            return View(); // "About"
        }

        #endregion
    }
}
