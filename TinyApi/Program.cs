using TinyApi.Models;
using TinyApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repository as singleton
builder.Services.AddSingleton<IItemRepository, ItemRepository>();

// Enable CORS to allow any origin
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configure console logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline
// Enable Swagger in both Development and Production
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();

app.UseHttpsRedirection();

// Health endpoint
app.MapGet("/health", () =>
{
    return Results.Ok(new
    {
        status = "ok",
        utc = DateTime.UtcNow.ToString("O")
    });
})
.WithName("GetHealth")
.WithTags("Health")
.Produces<object>(StatusCodes.Status200OK);

// Get all items
app.MapGet("/items", (IItemRepository repository) =>
{
    return Results.Ok(repository.GetAll());
})
.WithName("GetItems")
.WithTags("Items")
.Produces<IEnumerable<Item>>(StatusCodes.Status200OK);

// Get item by id
app.MapGet("/items/{id}", (int id, IItemRepository repository) =>
{
    var item = repository.GetById(id);
    return item is not null
        ? Results.Ok(item)
        : Results.NotFound();
})
.WithName("GetItemById")
.WithTags("Items")
.Produces<Item>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);

// Create new item
app.MapPost("/items", (CreateItemRequest request, IItemRepository repository) =>
{
    var item = repository.Create(request.Name);
    return Results.Created($"/items/{item.Id}", item);
})
.WithName("CreateItem")
.WithTags("Items")
.Accepts<CreateItemRequest>("application/json")
.Produces<Item>(StatusCodes.Status201Created);

app.Run();
