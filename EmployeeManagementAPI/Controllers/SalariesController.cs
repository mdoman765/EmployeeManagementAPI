using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    public class SalariesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
