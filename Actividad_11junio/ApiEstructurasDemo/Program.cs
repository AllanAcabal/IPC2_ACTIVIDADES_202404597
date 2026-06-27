using ApiEstructurasDemo.Models;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


// Colección que simula una estructura de datos en memoria.
var coleccionNodos = new List<NodoElemento>
{
    new NodoElemento
    {
        Id = 10,
        Valor = "Raíz Inicial (ABB)"
    },

    new NodoElemento
    {
        Id = 5,
        Valor = "Hijo Izquierdo"
    }
};



// Obtiene todos los nodos almacenados.
app.MapGet("/api/nodos", () =>
{
    return Results.Ok(coleccionNodos);
});



// Agrega un nuevo nodo a la colección.
app.MapPost("/api/nodos", (NodoElemento nuevoNodo) =>
{
    // Valida los datos recibidos.
    if (nuevoNodo.Id <= 0 || string.IsNullOrWhiteSpace(nuevoNodo.Valor))
    {
        return Results.BadRequest("Datos del nodo inválidos.");
    }

    // Inserta el nodo.
    coleccionNodos.Add(nuevoNodo);

    return Results.Created(
        $"/api/nodos/{nuevoNodo.Id}",
        nuevoNodo);
});

app.Run();