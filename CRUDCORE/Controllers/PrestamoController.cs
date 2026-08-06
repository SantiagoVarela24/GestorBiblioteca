using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using CRUDCORE.Datos;
using CRUDCORE.Models;

namespace CRUDCORE.Controllers
{
    [Authorize]
    public class PrestamoController : Controller
    {
        PrestamoDatos _PrestamoDatos = new PrestamoDatos();

        public IActionResult Listar()
        {
            var oLista = _PrestamoDatos.Listar();
            return View(oLista);
        }

        public IActionResult Guardar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Guardar(PrestamoModel oPrestamo)
        {
            if (!ModelState.IsValid)
                return View(oPrestamo);

            var respuesta = _PrestamoDatos.Guardar(oPrestamo);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View(oPrestamo);
        }

        public IActionResult Editar(int IdPrestamo)
        {
            var oPrestamo = _PrestamoDatos.Obtener(IdPrestamo);
            return View(oPrestamo);
        }

        [HttpPost]
        public IActionResult Editar(PrestamoModel oPrestamo)
        {
            if (!ModelState.IsValid)
                return View(oPrestamo);

            var respuesta = _PrestamoDatos.Editar(oPrestamo);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View(oPrestamo);
        }

        public IActionResult Eliminar(int IdPrestamo)
        {
            var oPrestamo = _PrestamoDatos.Obtener(IdPrestamo);
            return View(oPrestamo);
        }

        [HttpPost]
        public IActionResult Eliminar(PrestamoModel oPrestamo)
        {
            var respuesta = _PrestamoDatos.Eliminar(oPrestamo.IdPrestamo);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View(oPrestamo);
        }

        // Marca un préstamo como devuelto sin tener que editarlo entero
        public IActionResult Devolver(int IdPrestamo)
        {
            _PrestamoDatos.MarcarDevuelto(IdPrestamo);
            return RedirectToAction("Listar");
        }
    }
}
