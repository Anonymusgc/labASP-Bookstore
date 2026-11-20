using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using labASP.Models;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace labASP.Controllers;



public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IActionDescriptorCollectionProvider _actionProvider;

    public HomeController(
        ILogger<HomeController> logger,
        IActionDescriptorCollectionProvider actionProvider)
    {
        _logger = logger;
        _actionProvider = actionProvider;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    
    public IActionResult Docs(string? path) => Content($"Dokumentacja: {path}");

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    public IActionResult Boom() => throw new InvalidOperationException("Celowy wyjątek");
    
    public IActionResult Routes([FromServices] IActionDescriptorCollectionProvider provider)
    {
        var routes = provider.ActionDescriptors.Items.OfType<ControllerActionDescriptor>()
            .Select(r =>
            {
                var area = r.RouteValues.TryGetValue("area", out var a) ? a : null;
                var conventional = string.IsNullOrEmpty(area)
                    ? $"{r.ControllerName}/{r.ActionName}"
                    : $"{area}/{r.ControllerName}/{r.ActionName}";

                var methods = string.Join(", ",
                    r.ActionConstraints?.OfType<HttpMethodActionConstraint>()
                        .SelectMany(c => c.HttpMethods ?? ["*"]) ?? Array.Empty<string>()
                );

                return new RouteInfo()
                {
                    Pattern = "/" + (r.AttributeRouteInfo?.Template?.TrimStart('/') ?? conventional),
                    Action = r.ActionName,
                    Controller = r.ControllerName,
                    Methods = string.IsNullOrEmpty(methods) ? "*" : methods,
                    Area = area
                };
            }).OrderBy(r => r.Pattern)
            .ThenBy(r => r.Methods)
            .ToList();
        return View(routes);
    }
    
    public IActionResult HttpStatus(int code)
    {
        Response.StatusCode = code;
        ViewBag.StatusCode = code;
        ViewBag.Message = code switch
        {
            403 => "Brak dostępu do zasobu (403 Forbidden).",
            404 => "Nie znaleziono żądanej strony (404 Not Found).",
            418 => "Jestem czajniczkiem (418 I'm a teapot).",
            500 => "Wewnętrzny błąd serwera (500 Internal Server Error).",
            _ => $"Wystąpił błąd HTTP o kodzie {code}."
        };

        return View();
    }
    
}