namespace TinyApi.Models;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class CreateItemRequest
{
    public string Name { get; set; } = string.Empty;
}

