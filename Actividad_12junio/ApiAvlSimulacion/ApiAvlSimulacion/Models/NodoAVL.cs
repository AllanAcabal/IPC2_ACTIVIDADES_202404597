namespace ApiAvlSimulacion.Models;

/// Representa un nodo del árbol AVL utilizado en la simulación.
public class NodoAVL
{
    // Identificador o llave del nodo.
    public int Id { get; set; }

    // Nombre o descripción del nodo.
    public string Etiqueta { get; set; } = string.Empty;

    // Altura del nodo dentro del árbol.
    public int Altura { get; set; } = 1;
}