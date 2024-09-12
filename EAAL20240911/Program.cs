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

// Crear una lista para alamcenar objetos de tipo Marca 
var marca = new List<Marca>();

//Configurar una ruta GET para ontener todas las marcas
app.MapGet("/marca", () =>
{
    return marca; // Devuelve la lista de marcas
});

//Configurar una ruta GET para otener una marca específica por su ID
app.MapGet("/marca/{id}", (int id) =>
{
    //Buscar una marca en la lista que tenga el ID especificado
    var marcas = marca.FirstOrDefault(c => c.Id == id);
    return marca; //Devuelve el cliente encontrado (o null si no se encuentra)
});

//Configurar una ruta POST para agregar una nueva marca a la lista
app.MapPost("/marca", (Marca marcas) =>
{
    marca.Add(marcas); //Agrega la nueva marca a la lista
    return Results.Ok(); //Devuelve una repuesta HTTP 200 OK
});

//Configurar una ruta PUT para actualizar una marca existente por su ID
app.MapPut("/marca/{id}", (int id, Marca marcas) =>
{
    //Bsucar una marca en la lista que tenga el ID especificado
    var existingMarca = marca.FirstOrDefault(c => c.Id == id);
    if(existingMarca != null)
    {
        //Actualiza los datos de la marca existente con los datos proporcionados
        existingMarca.Nombre = marcas.Nombre;
        return Results.Ok(); // Devuelve una respuesta HTTP 200 Ok
    }
    else
    {
        return Results.NotFound(); // Devulve una respuesta HTTP 404 Not Found si la marca no existe
    }
});

//Ejecutar la aplicacion
app.Run();

//Definicion de la clase Marca que representa la estructura de un cliente
internal class Marca 
{
    public int Id { get; set; }
    public string? Nombre { get; set; }

}