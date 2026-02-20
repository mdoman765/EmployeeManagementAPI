using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
