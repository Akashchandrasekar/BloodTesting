using Microsoft.AspNetCore.Mvc;

namespace BloodTesting.Controllers
{
    public class MedicalTestsPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
