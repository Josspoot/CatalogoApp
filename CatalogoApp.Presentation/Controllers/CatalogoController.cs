using CatalogoApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using CatalogoApp.Application.Services; // Necesario para ItemService
using Microsoft.AspNetCore.Authorization; // Necesario para [Authorize]

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

        // ---------- NUEVO MÉTODO AQUÍ ADENTRO DE LA CLASE ----------

        [HttpPost]
        [Authorize] // Protege esta ruta, solo usuarios logueados pueden entrar
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

            // Asegurarse de que la lista esté inicializada (por precaución)
            if (item.Comentarios == null)
            {
                item.Comentarios = new List<ComentarioItem>();
            }

            item.Comentarios.Add(nuevoComentario);
        
            // NOTA: Asegúrate de tener un método en tu ItemService/Repository para guardar los cambios en tu JSON.
            // _itemService.Actualizar(item); 

            return RedirectToAction("Detalle", new { id = itemId });
        }
        
        [HttpPost]
        [Authorize] // Solo usuarios logueados pueden ejecutar esta acción
        public IActionResult ApoyarComentario(int itemId, string comentarioId)
        {
            var item = _itemService.ObtenerPorId(itemId);
            if (item == null) return NotFound();

            // Buscamos el comentario específico dentro del juego usando su Id único
            var comentario = item.Comentarios?.FirstOrDefault(c => c.Id == comentarioId);
            if (comentario != null)
            {
                comentario.Apoyos++;
        
                // NOTA: Recuerda descomentar y usar tu método de persistencia si cuentas con él
                // _itemService.Actualizar(item); 
            }

            return RedirectToAction("Detalle", new { id = itemId });
        }
    }
    
}