using CRUDCORE.Datos;
using CRUDCORE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CRUDCORE.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        PrestamoDatos _PrestamoDatos = new PrestamoDatos();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var oLista = _PrestamoDatos.Listar();

            ViewBag.Total = oLista.Count;
            ViewBag.Pendientes = oLista.Count(p => !p.Devuelto);
            ViewBag.Devueltos = oLista.Count(p => p.Devuelto);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}