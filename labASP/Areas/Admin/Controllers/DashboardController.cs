using Microsoft.AspNetCore.Mvc;

namespace labASP.Areas.Admin.Controllers;

[Area("Admin")]
public class DashboardController : Controller
{
    // GET
   
    public IActionResult Index()
    {
        return View();
    }
}