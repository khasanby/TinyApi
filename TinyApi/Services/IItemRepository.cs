using TinyApi.Models;

namespace TinyApi.Services;

public interface IItemRepository
{
    IEnumerable<Item> GetAll();
    Item? GetById(int id);
    Item Create(string name);
}

