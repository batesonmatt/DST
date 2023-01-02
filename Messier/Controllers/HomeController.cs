using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messier.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;

namespace Messier.Controllers
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
