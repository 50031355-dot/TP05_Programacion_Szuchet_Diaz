using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tp05.Models;

namespace tp05.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View("login");
    }

    public IActionResult Privacy()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Login(string nombreUsuario, string contrasena)
    {
        bool validarLogin=BD.ValidarLogin(nombreUsuario, contrasena);
        if(validarLogin)
        {
            HttpContext.Session.SetString("nombreUsuario", nombreUsuario);
            ViewBag.nombreUsuario=nombreUsuario;
            return View("bienvenida");
        }
        else
        {
            Console.WriteLine(validarLogin);
            ViewBag.Error = "Usuario o contraseña incorrectos";
            return View("login");
        }
    }

    public IActionResult Registro()
    {
        return View("registro");
    }

    [HttpPost]
    public IActionResult ValidarRegistro(Usuarios usuario)
    {
        bool validarRegistro = BD.ValidarRegistro(usuario);
        if (validarRegistro)
        {
            HttpContext.Session.SetString("nombreUsuario", usuario.nombreUsuario);
            ViewBag.nombreUsuario = usuario.nombreUsuario;
            return View("bienvenida");
        }
        else
        {
            Console.WriteLine(validarRegistro);
            ViewBag.Error = "El usuario ya existe";
            return View("registro");
        }
    }
    public IActionResult logout()
    {
        return View();
    }
    public IActionResult ValidarLogout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
