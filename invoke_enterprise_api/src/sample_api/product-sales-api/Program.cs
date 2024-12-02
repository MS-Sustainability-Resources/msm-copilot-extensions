using System.Text.Json;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/products", () =>
{
    var json = File.ReadAllText("products.json");
    var products = JsonSerializer.Deserialize<List<ProductSales>>(json);
    return products;
})
.WithName("GetProducts")
.WithOpenApi();

app.Run();

