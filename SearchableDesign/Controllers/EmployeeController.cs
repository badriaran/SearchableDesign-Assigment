using Microsoft.AspNetCore.Mvc;

namespace SearchableDesign.UI.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UploadEmployee()
        {
            return View();
        }
    }
}
