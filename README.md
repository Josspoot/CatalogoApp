# CatalogoApp

## Datos
* **Nombre:** Josue Enmanuel Poot Mateo
* **Materia:** Arquitectura de Software
* **Cuatrimestre:** 3
* **Institución:** Tecnológico de Software


## Descripción del Proyecto
CatalogoApp es una aplicación web desarrollada con .NET y C#. El proyecto implementa un sistema de catálogo estructurado bajo los principios de Arquitectura Limpia (Clean Architecture), separando las responsabilidades en distintas capas para facilitar su mantenimiento y escalabilidad.

## Estructura de la Solución
La arquitectura del proyecto está compuesta por las siguientes capas principales:

* **CatalogoApp.Domain**: Define las entidades centrales del dominio (modelo `Item`) y las abstracciones principales (como `IItemRepository`).
* **CatalogoApp.Application**: Contiene la lógica de negocio de la aplicación y los servicios (como `ItemService`) que actúan como intermediarios entre la presentación y el dominio.
* **CatalogoApp.Infrastructure**: Maneja el acceso a los datos, la persistencia y las implementaciones concretas (incluyendo `JsonItemRepository` para la gestión de datos mediante archivos JSON).
* **CatalogoApp.Presentation**: Es la capa de interfaz de usuario desarrollada con ASP.NET Core MVC, encargada de gestionar los controladores (`CatalogoController`, `HomeController`) y renderizar las vistas para el usuario final.

## Tecnologías Utilizadas
* C#
* ASP.NET Core MVC
* .NET

## Cláusula de Uso de Inteligencia Artificial
Se hace constar que el presente trabajo fue realizado con el apoyo de herramientas de Inteligencia Artificial. Dicha tecnología fue empleada específicamente como asistencia para la realización de los estilos visuales, así como para el desarrollo e implementación de algunas funcionalidades del proyecto.
