# Actividad Corta de Laboratorio

## De ADO.NET Tradicional a la Automatización con EF Core

**Curso:** Introducción a la Programación y Computación 2
**Universidad:** Universidad de San Carlos de Guatemala
**Estudiante:** Allan Marcelo Acabal Pérez
**Carné:** 202404597

---

# Parte 1. Diagnóstico Técnico y Brecha de Impedancia

## 1. La Brecha de Impedancia

La brecha de impedancia representa la diferencia entre la forma en que los datos se modelan en un lenguaje orientado a objetos como C# y la forma en que se almacenan dentro de una base de datos relacional.

Entity Framework Core actúa como un **ORM (Object Relational Mapper)**, permitiendo establecer automáticamente la relación entre ambos dominios.

### Equivalencias

| Dominio de Objetos (C#) | Dominio Relacional (SQL) |
| ----------------------- | ------------------------ |
| Clase Clásica (POCO)    | Tabla                    |
| Propiedad / Atributo    | Columna                  |
| Instancia de Objeto     | Registro (Fila)          |

---

## 2. Mitigación de Vulnerabilidades

En ADO.NET tradicional, concatenar variables dentro de una consulta SQL puede provocar ataques de **Inyección SQL**.

Entity Framework Core evita este problema utilizando automáticamente **consultas parametrizadas**, por lo que los valores enviados por el usuario no se concatenan directamente dentro del comando SQL.

En ADO.NET la forma recomendada de evitar esta vulnerabilidad consistía en utilizar parámetros mediante instrucciones como:

```csharp
cmd.Parameters.AddWithValue("@filtro", "Ing.%");
```

---

## 3. Optimización de Infraestructura

El método **AsNoTracking()** desactiva el seguimiento de cambios que realiza Entity Framework Core sobre los objetos recuperados.

Cuando una consulta únicamente mostrará información y no modificará registros, utilizar este método reduce el consumo de memoria RAM y el trabajo del servidor.

En un entorno universitario donde múltiples estudiantes comparten el mismo hardware, esta práctica contribuye a utilizar los recursos de manera más eficiente.

---

# Parte 2. Desafío de Refactorización

## Fragmento 1. UnidadAcademicaContext

```csharp
using Microsoft.EntityFrameworkCore;

public class UnidadAcademicaContext : DbContext
{
    public UnidadAcademicaContext(DbContextOptions<UnidadAcademicaContext> options)
        : base(options)
    {
    }

    // Representa la tabla de catedráticos.
    public DbSet<Catedratico> Catedraticos { get; set; }
}
```

---

## Fragmento 2. Consulta LINQ con EF Core

```csharp
using Microsoft.EntityFrameworkCore;

public class CatedraticoService
{
    private readonly UnidadAcademicaContext _context;

    public CatedraticoService(UnidadAcademicaContext context)
    {
        _context = context;
    }

    public async Task<List<Catedratico>> ObtenerCatedraticosAsync()
    {
        // Consulta de solo lectura.
        return await _context.Catedraticos
            .AsNoTracking()
            .Where(c => c.Nombre.StartsWith("Ing."))
            .ToListAsync();
    }
}
```

---

# Comparación entre ADO.NET y EF Core

| ADO.NET                                   | Entity Framework Core                    |
| ----------------------------------------- | ---------------------------------------- |
| Requiere escribir SQL manualmente.        | Utiliza consultas LINQ.                  |
| El mapeo de datos se realiza manualmente. | El ORM realiza el mapeo automáticamente. |
| Se administra manualmente la conexión.    | El contexto administra la conexión.      |
| Mayor cantidad de código repetitivo.      | Código más limpio y mantenible.          |

---

# Conclusiones

* Entity Framework Core simplifica el acceso a bases de datos mediante el uso de un ORM.
* El mapeo automático reduce la cantidad de código necesario para trabajar con entidades.
* El uso de consultas parametrizadas disminuye el riesgo de Inyección SQL.
* Utilizar `.AsNoTracking()` mejora el rendimiento cuando las consultas son únicamente de lectura.

---

# Referencias Bibliográficas

* Facultad de Ingeniería, Universidad de San Carlos de Guatemala. (2026). *Sesión 17: Conectividad con SQL Server. Acceso Estructurado a Datos mediante C# y ADO.NET.* Laboratorio de Introducción a la Programación y Computación 2. Guatemala.

* Facultad de Ingeniería, Universidad de San Carlos de Guatemala. (2026). *Sesión 18: Mapeo de Objetos Relacionales. Persistencia Automatizada con Entity Framework Core.* Laboratorio de Introducción a la Programación y Computación 2. Guatemala.
