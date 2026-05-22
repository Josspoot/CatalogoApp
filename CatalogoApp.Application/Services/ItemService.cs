using CatalogoApp.Domain.Interfaces;
using CatalogoApp.Domain.Models;

namespace CatalogoApp.Application.Services
{
    public class ItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public IEnumerable<Item> ObtenerTodos() => _itemRepository.ObtenerTodos();
        public Item? ObtenerPorId(int id) => _itemRepository.ObtenerPorId(id);
        public IEnumerable<string> ObtenerGeneros() => _itemRepository.ObtenerTodos().Select(i => i.Genero).Distinct();
        public IEnumerable<Item> ObtenerPorGenero(string genero) => _itemRepository.ObtenerTodos().Where(i => i.Genero == genero);
        public void Agregar(Item item) => _itemRepository.Agregar(item);

        // ---------- AÑADE ESTE MÉTODO ----------
        public void Actualizar(Item item)
        {
            _itemRepository.Actualizar(item);
        }
    }
}