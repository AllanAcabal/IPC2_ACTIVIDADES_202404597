# Actividad de Investigación y Práctica

## Balanceo Compuesto en Árboles AVL y Exposición de Estructuras vía Web APIs

**Curso:** Introducción a la Programación y Computación 2
**Universidad:** Universidad de San Carlos de Guatemala
**Estudiante:** Allan Marcelo Acabal Pérez
**Carné:** 202404597

---

# Parte 1. Investigación Teórica y Análisis de Casos

## 1. El límite de las rotaciones simples y el desbalance en "Zig-Zag"

### El Problema Cruzado

Las rotaciones simples no son suficientes cuando un árbol AVL presenta un desbalance cruzado, también conocido como caso **Zig-Zag**. Un ejemplo ocurre al insertar la secuencia **30, 10 y 20**.

Inicialmente, el nodo 30 es la raíz y el nodo 10 se convierte en su hijo izquierdo. Al insertar el nodo 20, este queda como hijo derecho de 10, provocando un desbalance que una rotación simple no puede corregir completamente, ya que únicamente cambia la inclinación del árbol sin restaurar el equilibrio.

En este caso es necesario aplicar una **Rotación Doble Izquierda-Derecha (RID)**.

La rotación RID se ejecuta cuando se cumplen las siguientes condiciones:

* El nodo padre tiene un **Factor de Equilibrio (FE) = -2**.
* El hijo izquierdo tiene un **Factor de Equilibrio (FE) = +1**.

La solución consiste en realizar primero una rotación simple hacia la izquierda sobre el hijo y posteriormente una rotación simple hacia la derecha sobre el nodo padre, logrando que el árbol recupere su equilibrio.

---

### Principio DRY (Don't Repeat Yourself)

El principio **DRY (Don't Repeat Yourself)** indica que no se debe duplicar código cuando ya existe una solución reutilizable.

En el caso de las rotaciones dobles, resulta más conveniente reutilizar las rotaciones simples ya implementadas que volver a escribir toda la manipulación de punteros desde cero.

Esta práctica ofrece varias ventajas:

* Reduce la duplicación de código.
* Facilita el mantenimiento del programa.
* Disminuye la probabilidad de errores.
* Hace que el código sea más claro y reutilizable.

---

## 2. Fundamentos de Arquitectura Web y Protocolo HTTP

### Modelo Cliente-Servidor

El modelo Cliente-Servidor consiste en una comunicación entre dos componentes principales.

El **cliente** (navegador, aplicación móvil o software) envía una solicitud (**Request**) al servidor para obtener o enviar información.

El **servidor** recibe la solicitud, procesa la información solicitada y devuelve una respuesta (**Response**) al cliente.

Los datos normalmente viajan mediante el protocolo **HTTP**, utilizando formatos como **JSON**, permitiendo la comunicación entre diferentes aplicaciones.

---

### Diferencia entre HTTP GET y POST

El método **GET** se utiliza para recuperar información almacenada en el servidor. Su función principal es consultar recursos sin modificar su contenido.

El método **POST** se utiliza para enviar información al servidor con el propósito de crear o insertar nuevos datos, modificando el estado del recurso.

En esta práctica:

* **GET** recupera la estructura actual del árbol AVL almacenado en memoria.
* **POST** simula la inserción de un nuevo nodo y, cuando corresponde, ejecuta la rotación doble para balancear el árbol.

---

# Parte 2. Implementación Práctica

La implementación se desarrolló utilizando **ASP.NET Core Minimal APIs**.

Se creó un modelo denominado **NodoAVL**, el cual representa cada nodo del árbol mediante un identificador, una etiqueta descriptiva y la altura correspondiente.

Posteriormente se implementaron dos endpoints:

## Endpoint GET

```http
GET /api/arbol
```

Este endpoint devuelve el estado actual del árbol AVL almacenado en memoria.

---

## Endpoint POST

```http
POST /api/arbol/insertar
```

Este endpoint recibe un nuevo nodo en formato JSON.

Cuando el identificador recibido corresponde al valor **20**, la API simula el caso de desbalance **Izquierda-Derecha (RID)** y reorganiza completamente la estructura del árbol, dejando el nodo **20** como nueva raíz balanceada.

En cualquier otro caso, el nodo simplemente se agrega a la colección en memoria.

---

# Resultados de las pruebas

Se realizaron las pruebas solicitadas en el laboratorio.

### Paso A

Se ejecutó una petición **GET** al endpoint:

```text
/api/arbol
```

La respuesta mostró el estado inicial del árbol con los nodos **30** y **10**, representando el desbalance previo.

---

### Paso B

Se realizó una petición **POST** al endpoint:

```text
/api/arbol/insertar
```

Enviando el siguiente cuerpo JSON:

```json
{
    "id": 20,
    "etiqueta": "Nieto Derecho"
}
```

La API respondió correctamente con el código **HTTP 201 Created**, indicando que la simulación de la rotación RID fue ejecutada exitosamente.

---

### Paso C

Finalmente se consultó nuevamente el endpoint:

```text
/api/arbol
```

La respuesta confirmó que el árbol quedó balanceado, teniendo como nueva raíz al nodo **20**, con los nodos **10** y **30** como hijos izquierdo y derecho respectivamente.

---

# Conclusiones

* Las rotaciones dobles permiten corregir desbalances que no pueden resolverse mediante una única rotación simple.
* La reutilización de rotaciones simples favorece un código más limpio y fácil de mantener.
* Los métodos HTTP GET y POST permiten exponer estructuras de datos mediante servicios web de forma sencilla.
* ASP.NET Core Minimal APIs facilita el desarrollo rápido de servicios web con una cantidad reducida de código.

---

# Referencia Bibliográfica

Facultad de Ingeniería, Universidad de San Carlos de Guatemala. (2026). *Sesión 10: Rotaciones Dobles en Árboles AVL y Exposición de Estructuras vía Web APIs.* Laboratorio del curso **Introducción a la Programación y Computación 2**. Guatemala.
