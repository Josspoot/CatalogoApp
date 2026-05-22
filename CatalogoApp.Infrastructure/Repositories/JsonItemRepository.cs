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

        private List<Item> LeerTodos()
        {
            if (!File.Exists(_filePath)) return new List<Item>();
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Item>>(json) ?? new List<Item>();
        }

        private void GuardarTodos(List<Item> items)
        {
            var json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        // CORRECCIÓN: Cambiado de IEnumerable a List para coincidir con la interfaz
        public List<Item> ObtenerTodos() => LeerTodos();

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

        public void Actualizar(Item itemModificado)
        {
            var items = LeerTodos();
            var index = items.FindIndex(i => i.Id == itemModificado.Id);
            
            if (index != -1)
            {
                items[index] = itemModificado;
                GuardarTodos(items);
            }
        }
    }
}