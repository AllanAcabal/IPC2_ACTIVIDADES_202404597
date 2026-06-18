# Reporte de Laboratorio: Arquitectura Multi-Nivel (N-Tier) y Patrón MVC en .NET

**Curso:** Introducción a la Programación y Computación 2 (P)
**Universidad de San Carlos de Guatemala**

---

## Parte 1: Fundamentación Teórica y Análisis Crítico

### 1. El Tránsito hacia los Sistemas Distribuidos y Multi-Capa

**La Limitación del Monolito Local**

Cuando la interfaz, la lógica de negocio y el almacenamiento de datos residen de forma exclusiva en una sola máquina física, el sistema queda atado a la capacidad de ese único equipo: para atender más usuarios solo es posible escalar verticalmente (agregar más RAM o CPU a esa máquina), nunca distribuir la carga entre varios servidores. Además, al no existir separación entre el proceso que atiende peticiones y el que custodia los datos, cualquier acceso concurrente debe resolverse con bloqueos dentro del mismo proceso, lo que genera cuellos de botella y condiciones de carrera a medida que crece el número de usuarios simultáneos. Por último, el monolito local representa un único punto de falla: si la máquina se cae, se pierde simultáneamente la interfaz, la lógica y los datos, y cualquier mantenimiento obliga a detener el sistema completo.

**Distinción Crítica (Layers vs. Tiers)**

Las **Capas Lógicas (Layers)** son una forma de organizar el código fuente según su responsabilidad (presentación, lógica de negocio, acceso a datos), independientemente de en qué máquina se ejecute ese código. Es un concepto de diseño de software que puede existir incluso dentro de un único proceso o un único servidor. Los **Niveles Físicos (Tiers)**, en cambio, son la distribución de esas capas en máquinas, procesos o entornos de ejecución físicamente distintos; es un concepto de despliegue de infraestructura. En consecuencia, es perfectamente posible tener una aplicación con tres capas lógicas bien separadas pero desplegada en un solo nivel físico (un monolito ordenado), o tener esas mismas tres capas distribuidas en tres servidores distintos (una arquitectura de 3 niveles real). Layers responde a "cómo está organizado el código"; Tiers responde a "dónde se ejecuta físicamente ese código".

**Responsabilidades en la Arquitectura de 3 Niveles**

- **Nivel 1 — Capa de Presentación (Presentation Tier):** su misión exclusiva es interactuar con el usuario final, capturando sus acciones y mostrando los resultados ya procesados. No debe contener reglas de negocio ni acceso directo a los datos. Tecnologías comunes: navegadores web con HTML/CSS/JavaScript, aplicaciones móviles, o motores de vistas como Razor en ASP.NET Core.
- **Nivel 2 — Capa de Aplicación o Negocio (Application Tier):** concentra las reglas del negocio, las validaciones y la orquestación de las operaciones; es el intermediario entre lo que el usuario pide y lo que la base de datos puede ofrecer. Tecnologías comunes: ASP.NET Core, Java EE/Spring, Node.js como servidores de aplicación.
- **Nivel 3 — Capa de Datos (Data Tier):** se encarga exclusivamente del almacenamiento persistente, la integridad referencial y las transacciones. Tecnologías comunes: motores de bases de datos relacionales como SQL Server, PostgreSQL o MySQL.

**Seguridad Perimetral**

Exponer públicamente el puerto de una base de datos a internet es un error crítico porque convierte al activo más valioso del sistema (los datos) en un objetivo directo de ataques de fuerza bruta, *credential stuffing* o explotación de vulnerabilidades del motor de base de datos, sin que exista ninguna capa intermedia que valide, filtre o limite esas peticiones. Además, se elimina por completo el control de reglas de negocio: cualquier cliente con las credenciales correctas podría leer o modificar datos sin pasar por las validaciones de la capa de aplicación. La buena práctica recomendada es ubicar la base de datos en una subred privada, sin IP pública, accesible únicamente desde los servidores de la capa de aplicación a través de la red interna (o una VPN), con reglas de *firewall* que restrinjan el acceso solo a las direcciones IP conocidas de esos servidores.

### 2. Desacoplamiento Lógico con el Patrón MVC

**La Crisis del Código Espagueti**

Mezclar sentencias SQL, lógica matemática y etiquetas visuales dentro de un mismo archivo físico genera un acoplamiento extremo entre tres responsabilidades que deberían ser independientes: modificar el diseño visual obliga a tocar el mismo archivo donde vive la lógica de negocio y las consultas a la base de datos, con el riesgo constante de romper algo que no se pretendía cambiar. Para las pruebas unitarias, esto es devastador: no es posible probar la lógica de negocio de forma aislada, porque para ejecutar esa prueba también haría falta una base de datos real conectada y un motor capaz de interpretar HTML, lo que vuelve las pruebas lentas, frágiles y dependientes de infraestructura externa. El mantenimiento se vuelve costoso, la legibilidad se pierde y el código se vuelve, literalmente, una maraña ("espagueti") difícil de seguir.

**Separación de Preocupaciones (SoC)**

El patrón formulado por Trygve Reenskaug divide la aplicación en tres componentes con aislamiento estricto entre ellos:

- **Modelo:** representa los datos y las reglas de negocio del dominio (entidades, validaciones, cálculos). No debe conocer cómo esos datos serán mostrados —ni HTML, ni JSON, ni ningún detalle de presentación— porque eso lo amarraría a una tecnología de interfaz específica, impidiendo reutilizarlo en distintas vistas (web, móvil, API) y dificultando probarlo de forma aislada.
- **Vista:** se define como una entidad pasiva e "inteligente" porque su única inteligencia permitida es la de formato y recorrido de datos para mostrarlos (iterar una lista, dar formato a una fecha o un número), pero tiene estrictamente prohibido contener lógica de negocio, cálculos complejos o acceso directo a la base de datos. Es pasiva porque no decide nada por sí misma: solo despliega lo que el controlador le entrega.
- **Controlador:** actúa como intermediario táctico entre la petición HTTP entrante y el resto del sistema. Recibe la solicitud, realiza validaciones livianas, invoca al modelo para ejecutar la operación correspondiente y decide qué vista debe renderizarse junto con qué datos. Es el "director de orquesta" porque coordina, pero no ejecuta él mismo la lógica de negocio pesada ni el renderizado.

**Métricas de Ingeniería de Software**

El patrón MVC favorece una **alta cohesión** porque cada componente agrupa únicamente responsabilidades afines entre sí (el Modelo solo se ocupa de datos y reglas, la Vista solo de presentación, el Controlador solo de orquestación), y un **bajo acoplamiento** porque la comunicación entre esos componentes ocurre a través de interfaces bien definidas (el Controlador le pasa datos a la Vista mediante el objeto de modelo, sin que la Vista necesite conocer de dónde vinieron esos datos). Esta combinación permite que equipos de desarrollo trabajen en paralelo sobre distintos componentes sin interferir entre sí, que los cambios en una capa (por ejemplo, cambiar el motor de base de datos) no obliguen a modificar las otras, y que cada componente pueda probarse de forma independiente.

---

## Parte 2: Modelado del Ciclo de Vida y Enrutamiento Semántico

### 1. Mapeo Analítico de URLs

Con base en la plantilla jerárquica por defecto `{controller=Home}/{action=Index}/{id?}`:

| URL Entrante del Cliente | Clase Controladora Buscada | Método (Acción) Ejecutado | Parámetro `id` Inyectado |
|---|---|---|---|
| `https://ingenieria.usac.edu.gt/ControlAcademico/Login` | `ControlAcademicoController` | `Login` | (Ninguno) |
| `https://ingenieria.usac.edu.gt/Estudiante/Historial/20260123` | `EstudianteController` | `Historial` | `20260123` |
| `https://ingenieria.usac.edu.gt/Asignacion/Detalle/10` | `AsignacionController` | `Detalle` | `10` |
| `https://ingenieria.usac.edu.gt/Home` | `HomeController` | `Index` | (Ninguno / Opcional) |

El framework siempre toma el primer segmento de la ruta como nombre del controlador (agregando el sufijo `Controller` al buscar la clase), el segundo segmento como el método de acción a invocar, y el tercer segmento —si existe— se intenta enlazar al parámetro `id` de ese método; como `id?` es opcional en la plantilla, su ausencia (como en `/Home` o `/ControlAcademico/Login`) no provoca un error de enrutamiento.

### 2. Diagramación del Flujo Interactivo

1. **Interacción del usuario:** el usuario hace clic en un botón o enlace en el navegador, el cual construye y envía una petición HTTP (GET o POST) hacia la URL correspondiente del servidor.
2. **Enrutamiento (Routing):** el middleware de enrutamiento de ASP.NET Core (`UseRouting`) intercepta la petición, la compara contra la plantilla `{controller}/{action}/{id?}` y determina qué clase Controladora y qué método de Acción deben ejecutarse, extrayendo también el parámetro `id` si está presente.
3. **Ejecución del Controlador:** el Controlador correspondiente es instanciado y ejecuta el método de acción solicitado; dentro de ese método invoca al Modelo (o a un servicio) para obtener, calcular o modificar los datos necesarios, sin involucrarse él mismo en los detalles de cómo se mostrarán.
4. **Renderizado de la Vista:** el Controlador selecciona la Vista apropiada y le entrega el objeto de Modelo mediante `return View(modelo)`; el motor de plantillas Razor combina ese modelo con el archivo `.cshtml` para producir el documento HTML final.
5. **Respuesta al cliente:** el HTML generado dinámicamente viaja de regreso a través del *pipeline* HTTP hasta el navegador del usuario, quien lo recibe y lo renderiza visualmente, completando el ciclo de la petición.

---

## Parte 3: Implementación Práctica

Se construyó el proyecto `ControlAcademicoMvc` sobre ASP.NET Core 8 (controladores MVC nativos), siguiendo la separación de responsabilidades exigida:

- `Models/Estudiante.cs`: entidad POCO pura, sin ninguna referencia a presentación ni a persistencia.
- `Controllers/EstudianteController.cs`: controlador delgado con dos acciones (`Listar` para GET y `Registrar` para POST), ninguna de las cuales supera las 20 líneas de código; la única lógica que contienen es de orquestación y validación perimetral básica.
- `Controllers/HomeController.cs`: controlador mínimo requerido por la ruta convencional por defecto (`Home/Index`).
- `Views/Estudiante/Listar.cshtml`: vista fuertemente tipada (`@model IEnumerable<Estudiante>`) que únicamente recorre la colección y la presenta en una tabla HTML, sin lógica de negocio.
- `Program.cs`: configura el contenedor de servicios MVC (`AddControllersWithViews`) y registra la ruta convencional `{controller=Home}/{action=Index}/{id?}`.

El almacenamiento se simula con una lista estática en memoria dentro del Controlador, representando de forma simplificada el rol del Nivel 3 (Capa de Datos) sin necesidad de un motor de base de datos real para esta práctica.

---

## Parte 4: Auditoría y Control de Calidad

**Prueba de Cohesión (GET `/Estudiante/Listar`):** la acción `Listar()` se limita a devolver `View(_baseDatosMemoria)`; no calcula variables internas, no concatena sentencias SQL en texto plano ni realiza ninguna operación distinta a despachar los datos hacia la Vista. Esto confirma que el Controlador cumple su rol de intermediario y no absorbe responsabilidades del Modelo o de la Vista.

**Evaluación de Antipatrones (Fat Controllers):** al revisar `EstudianteController.cs`, ambos métodos (`Listar` y `Registrar`) se mantienen muy por debajo del límite de 20 líneas establecido, y ninguno mezcla lógica de presentación. No fue necesaria una refactorización adicional, ya que el diseño se planteó desde el inicio como *Skinny Controller*.

---

## Parte 5: Referencias Bibliográficas

> Facultad de Ingeniería, USAC. (2026). *Sesión 11: Modelado Base y Arquitecturas de Despliegue. Evolución de Sistemas Distribuidos, Fundamentos del Modelo Cliente-Servidor y Diseño Físico Multi-Capas (N-Tier).* Laboratorio del curso Introducción a la Programación y Computación 2. Guatemala.

> Facultad de Ingeniería, USAC. (2026). *Sesión 12: Arquitectura y Componentes del Patrón MVC. Desacoplamiento Lógico de Software, Ciclo de Vida de las Peticiones y Enrutamiento en Aplicaciones Interactivas Modernas.* Laboratorio del curso Introducción a la Programación y Computación 2. Guatemala.
