using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("List", "Task");
        }
    }
}
