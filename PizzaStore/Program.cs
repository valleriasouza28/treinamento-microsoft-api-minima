using Microsoft.OpenApi.Models;
using PizzaStore.DB;

var builder = WebApplication.CreateBuilder(args);

// Registro dos serviços necessários
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PizzaStore API", Description = "Making the Pizzas you love", Version = "v1" });
});

var app = builder.Build();

// Adicionar Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
});

// Mapeamento das Rotas

// GET: Obter todas as pizzas
app.MapGet("/pizzas", () => PizzaDB.GetPizzas());

// GET: Obter uma pizza por ID
app.MapGet("/pizzas/{id}", (int id) => PizzaDB.GetPizza(id));

// POST: Criar uma nova pizza
app.MapPost("/pizzas", (Pizza pizza) => PizzaDB.CreatePizza(pizza));

// PUT: Atualizar uma pizza existente
app.MapPut("/pizzas", (Pizza pizza) => PizzaDB.UpdatePizza(pizza));

// DELETE: Remover uma pizza por ID
app.MapDelete("/pizzas/{id}", (int id) =>
{
    PizzaDB.RemovePizza(id);
    return Results.NoContent();
});

// Rota padrão (apenas para teste)
app.MapGet("/", () => "Hello World!");

app.Run();
