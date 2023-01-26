using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DST.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;

namespace DST.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(); // "Index"
        }

        public IActionResult About()
        {
            return View(); // "About"
        }
    }
}
