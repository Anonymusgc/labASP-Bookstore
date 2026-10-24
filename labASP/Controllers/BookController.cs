using Microsoft.AspNetCore.Mvc;

namespace labASP.Controllers;

public class BookController : Controller
{
    // GET /Book/Hello  → zwykły tekst
    public IActionResult Hello() => Content("Witaj w BookStore!", "text/plain; charset=utf-8");

    // GET /Book/Status?code=418 → dowolny kod statusu
    public IActionResult Status(int code = 200) => StatusCode(code);

    // GET /Book/Details/3 → widok
    public IActionResult Details(int id)
    {
        ViewBag.Id = id;
        return View();
    }
}