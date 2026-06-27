# Actividad de Laboratorio: Interoperabilidad y Carga Masiva de Datos

**Curso:** Introducción a la Programación y Computación 2
**Universidad:** Universidad de San Carlos de Guatemala
**Estudiante:** Allan Marcelo Acabal Pérez
**Carné:** 202404597
**Fecha:** 26 de Junio de 2026 
**Vacas de Junio**: Primer Semestre 2026
---

# Parte 1. Evaluación Conceptual y Buenas Prácticas

## 1. Formatos de Intercambio

| Formato | Ventajas                                                                                                                   | Desventajas                                                                                                    |
| ------- | -------------------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------- |
| **CSV** | Es un formato simple, ligero y rápido de procesar. Además, es compatible con una gran variedad de aplicaciones y sistemas. | No soporta estructuras jerárquicas, tipos de datos ni metadatos, por lo que requiere validaciones adicionales. |
| **XML** | Permite representar estructuras jerárquicas complejas e incluir metadatos y validaciones.                                  | Es más pesado, ocupa más espacio y su procesamiento es más lento que el de un archivo CSV.                     |

---

## 2. Diferencia entre Serialización y Deserialización

La **serialización** consiste en convertir un objeto de C# en un documento JSON para poder almacenarlo o enviarlo a través de una API.

La **deserialización** consiste en convertir un documento JSON recibido desde una API nuevamente en un objeto de C# utilizando la librería **System.Text.Json**.

---

## 3. Antipatrón de Rendimiento N+1

El problema **N+1** ocurre cuando se realiza una operación sobre la base de datos por cada registro leído de un archivo masivo. Esto provoca un gran número de consultas o inserciones, disminuyendo el rendimiento de la aplicación.

La estrategia recomendada es **Batching**, que consiste en almacenar temporalmente los registros en una lista intermedia y realizar una única inserción mediante `AddRange()`, seguida de una sola llamada a `SaveChangesAsync()`.

---

# Parte 2. Implementación Práctica en C#

## Modelo: Alumno.cs

```csharp
public class Alumno
{
    public string Carnet { get; set; } = string.Empty;

    public string Nombre { get; set; } = string.Empty;

    public string Carrera { get; set; } = string.Empty;
}
```

---

## Desafío 1: ApiService.cs

```csharp
using System.Text.Json;

public class ApiService
{
    public async Task<List<Alumno>?> ObtenerAlumnosAsync()
    {
        using var httpClient = new HttpClient();

        try
        {
            var response = await httpClient.GetAsync("https://api.usac.edu/v1/alumnos");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<List<Alumno>>(json, opciones);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }
}
```

---

## Desafío 2: AlumnosController.cs

```csharp
[ApiController]
[Route("api/[controller]")]
public class AlumnosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AlumnosController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CargarCsv(IFormFile archivo)
    {
        if (archivo == null || archivo.Length == 0)
            return BadRequest("Debe seleccionar un archivo.");

        var alumnos = new List<Alumno>();

        using var stream = archivo.OpenReadStream();
        using var reader = new StreamReader(stream);

        await reader.ReadLineAsync();

        string? linea;

        while ((linea = await reader.ReadLineAsync()) != null)
        {
            var datos = linea.Split(',');

            var alumno = new Alumno
            {
                Carnet = datos[0],
                Nombre = datos[1],
                Carrera = datos[2]
            };

            alumnos.Add(alumno);
        }

        _context.Alumnos.AddRange(alumnos);

        await _context.SaveChangesAsync();

        return Ok("Carga masiva completada correctamente.");
    }
}
```

---

# Parte 3. Referencia Bibliográfica

Facultad de Ingeniería, Universidad de San Carlos de Guatemala. (2026). *Sesión 20: Integración de Datos. Consumo de APIs Externas y Carga Masiva (CSV/XML).* Laboratorio del curso **Introducción a la Programación y Computación 2**. Guatemala.
