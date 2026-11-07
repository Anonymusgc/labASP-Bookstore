using Microsoft.AspNetCore.Mvc;

namespace labASP.Areas.Admin.Controllers;

public class AuthorsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}