using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FestivalTicketsApp.WebUI.Models;

namespace FestivalTicketsApp.WebUI.Controllers;

public class HomeController : Controller
{
    private static readonly string _layoutName = "MainLayout";

    public async Task<IActionResult> Index()
    {
        ViewBag.Layout = _layoutName;
        
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}