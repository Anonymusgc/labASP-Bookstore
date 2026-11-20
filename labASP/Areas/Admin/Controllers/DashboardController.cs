using labASP.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace labASP.Areas.Admin.Controllers;

[Area("Admin")]
public class DashboardController : Controller
{
    private readonly RequestStats _stats;
    public DashboardController(RequestStats stats) => _stats = stats;
    // GET
   
    public IActionResult Index()
    {
        ViewBag.TotalRequests = _stats.Total;
        ViewBag.TopPaths = _stats.GetTopPaths(5);
        return View();
    }
}