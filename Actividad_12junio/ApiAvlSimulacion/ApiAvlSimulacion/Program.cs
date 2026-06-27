using ApiAvlSimulacion.Models;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


// Estado inicial del árbol en memoria.
var estadoArbol = new List<NodoAVL>
{
    new NodoAVL
    {
        Id = 30,
        Etiqueta = "Nodo Raíz (Abuelo) - FE: -2",
        Altura = 2
    },

    new NodoAVL
    {
        Id = 10,
        Etiqueta = "Hijo Izquierdo - FE: +1",
        Altura = 1
    }
};


// Recupera el estado actual del árbol.
app.MapGet("/api/arbol", () =>
{
    return Results.Ok(estadoArbol);
});


// Simula la inserción de un nuevo nodo.
app.MapPost("/api/arbol/insertar", (NodoAVL nuevoNodo) =>
{

    // Valida que el identificador sea válido.
    if (nuevoNodo.Id <= 0)
    {
        return Results.BadRequest("ID de nodo inválido.");
    }

    if (nuevoNodo.Id == 20)
    {
        estadoArbol.Clear();

        estadoArbol.Add(new NodoAVL
        {
            Id = 20,
            Etiqueta = "Nueva Raíz Balanceada (RID) - FE: 0",
            Altura = 2
        });

        estadoArbol.Add(new NodoAVL
        {
            Id = 10,
            Etiqueta = "Hijo Izquierdo - FE: 0",
            Altura = 1
        });

        estadoArbol.Add(new NodoAVL
        {
            Id = 30,
            Etiqueta = "Hijo Derecho - FE: 0",
            Altura = 1
        });

        return Results.Created(
            "/api/arbol",
            new
            {
                Mensaje = "Rotación RID ejecutada con éxito. Estabilidad total lograda.",
                Estructura = estadoArbol
            });
    }


    // Inserción simple cuando no requiere rotación.
    estadoArbol.Add(nuevoNodo);

    return Results.Created(
        $"/api/arbol/{nuevoNodo.Id}",
        nuevoNodo);
});

app.Run();