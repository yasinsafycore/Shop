using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Shop.Web.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}