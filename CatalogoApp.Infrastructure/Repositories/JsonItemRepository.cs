using System.Text.Json;
using CatalogoApp.Domain.Interfaces;
using CatalogoApp.Domain.Models;

namespace CatalogoApp.Infrastructure.Repositories
{
    public class JsonItemRepository : IItemRepository
    {
        private readonly string _filePath;

        public JsonItemRepository(string filePath)
        {
            _filePath = filePath;
        }

        // Método auxiliar que probablemente ya tienes para leer el JSON
        private List<Item> LeerTodos()
        {
            if (!File.Exists(_filePath)) return new List<Item>();
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Item>>(json) ?? new List<Item>();
        }

        // Método auxiliar para guardar la lista completa en el JSON
        private void GuardarTodos(List<Item> items)
        {
            var json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public IEnumerable<Item> ObtenerTodos() => LeerTodos();

        List<Item> IItemRepository.ObtenerTodos()
        {
            throw new NotImplementedException();
        }

        public Item? ObtenerPorId(int id) => LeerTodos().FirstOrDefault(i => i.Id == id);

        public void Agregar(Item item)
        {
            var items = LeerTodos();
            item.Id = items.Any() ? items.Max(i => i.Id) + 1 : 1;
            items.Add(item);
            GuardarTodos(items);
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        // ---------- COPIA ESTE NUEVO MÉTODO ----------
        public void Actualizar(Item itemModificado)
        {
            var items = LeerTodos();
            // Buscamos la posición del juego en el archivo JSON
            var index = items.FindIndex(i => i.Id == itemModificado.Id);
            
            if (index != -1)
            {
                // Reemplazamos el juego viejo por el modificado (con sus nuevos comentarios)
                items[index] = itemModificado;
                // Guardamos la lista actualizada en el archivo items.json
                GuardarTodos(items);
            }
        }
    }
}