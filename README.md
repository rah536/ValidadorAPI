# API REST - Validador de Infracciones Logísticas

# Descripción del Proyecto
Este proyecto es una solución integral diseñada para la validación automática de multas e infracciones de tránsito para flotas de vehículos y conductores. 

El sistema expone una **API RESTful** desarrollada en .NET Core que permite consultar de manera eficiente el estado de libre deuda (multas) almacenado en una base de datos relacional.
Esta arquitectura está preparada para integrarse con un flujo RPA (Robotic Process Automation) que actúa como motor de extracción de datos (Web Scraping) desde portales gubernamentales.

# Arquitectura y Tecnologías
El proyecto se encuentra separdo entre la capa de exposición de datos y la capa de extracción:

Backend / API: .NET Core (C#). Arquitectura basada en Controladores, principios REST y enrutamiento seguro.
Base de Datos: Microsoft SQL Server.
Motor RPA (En desarrollo): Bot basado en Robot Framework + Selenium (Python) encargado de poblar y actualizar la base de datos de forma autónoma.
