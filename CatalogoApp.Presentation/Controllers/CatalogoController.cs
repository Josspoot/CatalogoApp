using CatalogoApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using CatalogoApp.Application.Services; 
using Microsoft.AspNetCore.Authorization; 

namespace Catalogo.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly ItemService _itemService;

        public CatalogoController(ItemService itemService)
        {
            _itemService = itemService;
        }

        public IActionResult Index(string? genero)
        {
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
            _itemService.Agregar(item);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize] 
        public IActionResult AgregarComentario(int itemId, string texto, int estrellas)
        {
            var item = _itemService.ObtenerPorId(itemId);
            if (item == null) return NotFound();

            var nuevoComentario = new ComentarioItem
            {
                Usuario = User.Identity?.Name ?? "Anónimo",
                Texto = texto,
                Estrellas = estrellas
            };

            if (item.Comentarios == null)
            {
                item.Comentarios = new List<ComentarioItem>();
            }

            item.Comentarios.Add(nuevoComentario);

            // Guarda los cambios de forma persistente en el JSON
            _itemService.Actualizar(item); 

            return RedirectToAction("Detalle", new { id = itemId });
        }

        [HttpPost]
        [Authorize] 
        public IActionResult ApoyarComentario(int itemId, string comentarioId)
        {
            var item = _itemService.ObtenerPorId(itemId);
            if (item == null) return NotFound();

            var comentario = item.Comentarios?.FirstOrDefault(c => c.Id == comentarioId);
            if (comentario != null)
            {
                comentario.Apoyos++;
        
                // Guarda el nuevo contador de apoyos en el JSON
                _itemService.Actualizar(item); 
            }

            return RedirectToAction("Detalle", new { id = itemId });
        }
    }
}