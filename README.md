# API REST - Validador de Infracciones Logísticas

## Descripción del Proyecto
Este proyecto es una solución integral diseñada para la validación automática de multas e infracciones de tránsito para flotas de vehículos y conductores. 

El sistema expone una **API RESTful** desarrollada en .NET Core que permite consultar de manera eficiente el estado de libre deuda (multas) almacenado en una base de datos relacional. Está diseñada con una arquitectura limpia, lista para ser consumida por sistemas externos (ERPs, aplicaciones móviles o plataformas logísticas de terceros).

## Arquitectura y Tecnologías
* **Backend / API:** .NET Core (C#). Arquitectura basada en Controladores, principios REST y enrutamiento seguro.
* **Base de Datos:** Microsoft SQL Server. Diseño relacional optimizado para la lectura rápida de estados de infracción.
* **Documentación:** Swagger / OpenAPI implementado de forma nativa para la exploración interactiva y prueba de los endpoints.
* **Seguridad:** Autenticación mediante **API Key** (`x-api-key`) para denegar accesos no autorizados. Protección de credenciales de base de datos mediante `User Secrets`.

## Endpoints Disponibles
La API cuenta con los siguientes endpoints:

* `GET /api/multas/conductor/{dni}`: Retorna los datos del conductor y su estado actual de infracciones.
* `GET /api/multas/vehiculo/{dominio}`: Retorna los datos del vehículo y su estado actual de infracciones.

## 🔒 Autenticación
La API requiere un encabezado de autorización en todas sus peticiones para proteger el acceso a la base de datos.
El cliente debe incluir el siguiente Header:
`x-api-key: <TU_CLAVE_AQUI>` o `x-api-key: mi_clave_secreta_de_ejemplo`
