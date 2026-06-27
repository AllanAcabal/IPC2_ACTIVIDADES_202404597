namespace ApiEstructurasDemo.Models;

/// Representa un elemento almacenado en la colección de nodos.
public class NodoElemento
{
    // Identificador del nodo.
    public int Id { get; set; }

    // Valor o descripción del nodo.
    public string Valor { get; set; } = string.Empty;
}