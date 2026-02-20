using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
