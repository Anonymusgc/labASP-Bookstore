using labASP.Filters;
using labASP.Middleware;
using labASP.Models;
using labASP.Models.Ef;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration["Database:ConnectionString"];

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<RequestStats>();
builder.Services.AddScoped<LogActionFilter>();

builder.Services.AddControllersWithViews(o =>
{
    o.Filters.Add<GlobalExceptionFilter>();
});

builder.Services.AddDbContext<BookDbContext>(o =>
    o.UseSqlite(connection));
builder.Services.AddScoped<IBookService, EfBookService>();

var app = builder.Build();

app.UseMiddleware<RequestTimingMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseStatusCodePagesWithReExecute("/Home/HttpStatus", "?code={0}");

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute("books-by-year", "books/year/{year:int:min(1450)}",
    new { controller = "Book", action = "ByYear" });                    // ograniczenie int + min
app.MapControllerRoute("books-by-isbn", "books/isbn/{isbn:regex(^\\d{{13}}$)}",
    new { controller = "Book", action = "ByIsbn" });                    // regex: dokładnie 13 cyfr
app.MapControllerRoute("docs", "docs/{*path}",
    new { controller = "Home", action = "Docs" });                      // catch-all
app.MapControllerRoute("admin-area", "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "archive",
    pattern: "archive/{year:int}/{month:int:range(1,12)}",
    defaults: new { controller = "Book", action = "Archive" });
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");   // segmenty opcjonalne




app.Run();