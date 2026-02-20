using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
