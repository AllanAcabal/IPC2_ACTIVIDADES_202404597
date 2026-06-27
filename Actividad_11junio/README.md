# API de Estructuras de Datos con ASP.NET Core

## Universidad de San Carlos de Guatemala

**Facultad de Ingeniería**  
**Escuela de Ciencias y Sistemas**  
**Introducción a la Programación y Computación 2 (IPC2)**

---

## Descripción

Este repositorio contiene el desarrollo de la actividad de laboratorio **"Estructuras de Datos Avanzadas y APIs con ASP.NET Core"**.

La práctica consiste en desarrollar una **Web API** utilizando **ASP.NET Core Minimal APIs** para simular la administración de una colección de nodos en memoria, representando una estructura de datos similar a un Árbol Binario de Búsqueda (ABB).

Además, se incluye un documento de investigación donde se responden los conceptos teóricos relacionados con Árboles Binarios de Búsqueda, Árboles AVL, arquitectura Cliente-Servidor y el funcionamiento básico de las Web APIs.

---

## Objetivos

- Comprender el funcionamiento de los Árboles Binarios de Búsqueda.
- Conocer el principio de balanceo de los Árboles AVL.
- Implementar una Web API utilizando ASP.NET Core Minimal APIs.
- Aplicar los métodos HTTP GET y POST.
- Comprender el modelo Cliente-Servidor.

---

## Tecnologías utilizadas

- C#
- .NET 8
- ASP.NET Core Minimal APIs
- HTTP
- JSON
- Git
- GitHub

---

## Estructura del proyecto

```text
Actividad_11junio/
│
├── README.md
├── .gitignore
│
├── Documentacion/
│   └── Investigacion_Estructuras_API.md
│
└── ApiEstructurasDemo/
    ├── ApiEstructurasDemo.csproj
    ├── Program.cs
    ├── Models/
    │   └── NodoElemento.cs
    ├── appsettings.json
    ├── appsettings.Development.json
    ├── ApiEstructurasDemo.http
    └── Properties/
```

---

## Endpoints implementados

### GET

```
GET /api/nodos
```

Obtiene la colección de nodos almacenada en memoria.

---

### POST

```
POST /api/nodos
```

Agrega un nuevo nodo a la colección y devuelve un código **201 Created**.

---

## Pruebas realizadas

Se verificó el correcto funcionamiento de la API mediante:

- Consulta GET para recuperar los nodos iniciales.
- Inserción de un nuevo nodo utilizando POST.
- Verificación mediante un nuevo GET para confirmar la inserción.

---

## Autor

**Allan Marcelo Acabal Pérez**

**Carné:** 202404597