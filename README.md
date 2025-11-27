 Aplicación de Notas con MongoDB y C# (Windows Forms)

Objetivo del Proyecto
Implementación de un gestor de notas básico (CRUD: Crear, Leer, Actualizar, Eliminar) para demostrar la conexión y manipulación de una base de datos NoSQL (MongoDB Atlas) desde una aplicación de escritorio desarrollada en C# (.NET Framework) y Windows Forms.

 Requerimientos e Instalación

1. Requisitos
* Visual Studio.
* .NET Framework 4.8.
* Cluster activo en MongoDB Atlas.

2. Instalación del Driver
El proyecto utiliza el driver oficial de MongoDB. Se debe instalar en el proyecto mediante la consola de NuGet:

bash
Install-Package MongoDB.Driver

Prueba de Funcionalidad (CRUD)
1. Interfaz Principal y Lectura Exitosa (READ)
La aplicación carga y muestra todas las notas guardadas en la colección "Notas" de MongoDB Atlas.
<img width="782" height="470" alt="image" src="https://github.com/user-attachments/assets/d4ab9528-92fd-4ca6-b58f-348e1294ab8f" />


Creación Exitosa (CREATE)
Al presionar "GUARDAR", el documento se inserta correctamente en MongoDB.
<img width="794" height="469" alt="image" src="https://github.com/user-attachments/assets/1825bbd5-5508-4a06-89cf-33c09b1ae146" />
<img width="864" height="191" alt="image" src="https://github.com/user-attachments/assets/6906286f-c474-4329-b64d-c9c8421830a8" />


. Edición (UPDATE)
Al seleccionar una nota, editar sus campos y presionar "GUARDAR", el documento se actualiza correctamente en la base de datos.
<img width="789" height="467" alt="image" src="https://github.com/user-attachments/assets/c2e41075-ee05-430b-b7f6-f9afb7c39653" />


4. Eliminación Exitosa (DELETE)
Se elimina el documento seleccionado de la colección de MongoDB.
<img width="788" height="467" alt="image" src="https://github.com/user-attachments/assets/8796043d-770d-4398-86e7-8b77912694ac" />

