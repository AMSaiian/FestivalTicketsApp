using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FestivalTicketsApp.WebUI.Models;

namespace FestivalTicketsApp.WebUI.Controllers;

public class HomeController : Controller
{
    private static readonly string LayoutName = "MainLayout";

    public Task<IActionResult> Index()
    {
        ViewBag.Layout = LayoutName;
        
        return Task.FromResult((IActionResult)View());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}