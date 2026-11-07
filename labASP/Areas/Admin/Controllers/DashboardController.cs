using Microsoft.AspNetCore.Mvc;

namespace labASP.Areas.Admin.Controllers;

public class DashboardController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}