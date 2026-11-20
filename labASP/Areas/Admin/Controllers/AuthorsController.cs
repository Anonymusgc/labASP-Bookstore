using labASP.Models;
using Microsoft.AspNetCore.Mvc;

namespace labASP.Areas.Admin.Controllers;

[Area("Admin")]
public class AuthorsController : Controller
{
    private static readonly List<Author> _authors = new()
    {
        new Author { Id = 1, FirstName = "Stanisław", LastName = "Lem", Country = "Polska" },
        new Author { Id = 2, FirstName = "Albert", LastName = "Camus", Country = "Francja" },
        new Author { Id = 3, FirstName = "Bolesław", LastName = "Prus", Country = "Polska" }
    };

    public IActionResult Index() => View(_authors);

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Author author)
    {
        if (!ModelState.IsValid)
            return View(author);

        author.Id = _authors.Count > 0 ? _authors.Max(a => a.Id) + 1 : 1;
        _authors.Add(author);

        return RedirectToAction(nameof(Index));
    }
}