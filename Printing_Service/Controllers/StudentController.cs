using Microsoft.AspNetCore.Mvc;

namespace Printing_Service.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
