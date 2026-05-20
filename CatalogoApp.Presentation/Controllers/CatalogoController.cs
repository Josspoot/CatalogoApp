using CatalogoApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using CatalogoApp.Application.Services; // Necesario para ItemService

namespace Catalogo.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly ItemService _itemService;

        // Inyectamos el servicio a través del constructor
        public CatalogoController(ItemService itemService)
        {
            _itemService = itemService;
        }

        public IActionResult Index(string? genero)
        {
            // Usamos el servicio para obtener los datos
            var resultado = string.IsNullOrEmpty(genero)
                ? _itemService.ObtenerTodos()
                : _itemService.ObtenerPorGenero(genero);

            ViewBag.Generos = _itemService.ObtenerGeneros();
            ViewBag.GeneroActual = genero;
            
            return View(resultado);
        }

        public IActionResult Detalle(int id)
        {
            var item = _itemService.ObtenerPorId(id);
            return item == null ? NotFound() : View(item);
        }

        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(Item item)
        {
            // Llamamos al método Agregar del servicio, el cual invocará al repositorio para escribir en el JSON
            _itemService.Agregar(item);
            
            return RedirectToAction("Index");
        }
    }
}