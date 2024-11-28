using Microsoft.AspNetCore.Mvc;

namespace Printing_Service.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {

            return View();
        }

        public IActionResult Register()
        {

            return View();
        }

    }
}
