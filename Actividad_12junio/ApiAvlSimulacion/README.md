# API AVL Simulación

## Universidad de San Carlos de Guatemala

**Facultad de Ingeniería**  
**Escuela de Ciencias y Sistemas**  
**Introducción a la Programación y Computación 2 (IPC2)**

---

## Descripción

Este repositorio contiene el desarrollo de la actividad de laboratorio correspondiente a la **Sesión 10: Rotaciones Dobles en Árboles AVL y Exposición de Estructuras vía Web APIs**.

La práctica consiste en implementar una Web API utilizando **ASP.NET Core Minimal APIs** para simular el comportamiento de un árbol AVL cuando ocurre un caso de desbalance tipo **Izquierda-Derecha (RID)**.

Además, se incluye un documento de investigación donde se responden los conceptos teóricos solicitados en la actividad.

---

## Objetivos

- Comprender el funcionamiento de las rotaciones dobles en árboles AVL.
- Simular un árbol AVL mediante una API en memoria.
- Implementar endpoints HTTP GET y POST.
- Aplicar conceptos básicos de arquitectura Cliente-Servidor.
- Comprender la diferencia entre los métodos HTTP GET y POST.

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
ApiAvlSimulacion/
│
├── README.md
├── .gitignore
│
├── Documentacion/
│   └── Investigacion_AVL.md
│
└── ApiAvlSimulacion/
    ├── Program.cs
    ├── Models/
    │   └── NodoAVL.cs
    ├── ApiAvlSimulacion.csproj
    ├── appsettings.json
    ├── appsettings.Development.json
    └── Properties/
```

---

## Endpoints implementados

### GET

```
GET /api/arbol
```

Obtiene el estado actual del árbol AVL almacenado en memoria.

---

### POST

```
POST /api/arbol/insertar
```

Simula la inserción de un nodo y ejecuta la rotación compuesta RID cuando corresponde.

---

## Autor

**Allan Marcelo Acabal Pérez**

**Carné:** 202404597