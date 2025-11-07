using TinyApi.Models;

namespace TinyApi.Services;

public class ItemRepository : IItemRepository
{
    private readonly List<Item> _items;
    private int _nextId = 1;

    public ItemRepository()
    {
        _items = new List<Item>
        {
            new Item { Id = _nextId++, Name = "Item 1" },
            new Item { Id = _nextId++, Name = "Item 2" },
            new Item { Id = _nextId++, Name = "Item 3" }
        };
    }

    public IEnumerable<Item> GetAll() => _items;

    public Item? GetById(int id) => _items.FirstOrDefault(i => i.Id == id);

    public Item Create(string name)
    {
        var item = new Item
        {
            Id = _nextId++,
            Name = name
        };
        _items.Add(item);
        return item;
    }
}

