# Julian Designer Backend

Aplicación Backend web desarrollada en .NET 9 que permite la gestión de imágenes y usuarios. Utiliza Cloudinary para el almacenamiento de imágenes y MySQL como base de datos.

## Características

- Subida de imágenes con Cloudinary
- Gestión de usuarios
- Autenticación de usuarios (Hash de contraseñas)
- Eliminación selectiva o masiva de imágenes
- Recuperación de imágenes para inyección en el frontend

## Estructura del proyecto

- **Controllers**: Contiene los controladores de la API.
- **Repositories**: Contiene las interfaces y clases de repositorio para acceder a la base de datos.
- **Services**: Contiene los servicios para la lógica de negocio, como la integración con Cloudinary.
- **Contexto**: Contiene la configuración del contexto de la base de datos.

## Patrones de diseño respetados: 
- Todos los SOLID
- Todos los GRASP
- Patrón Repositorio
- Service Pattern
- Inyección de dependencias
- Normalización hasta FN 3 en BBDD.

  
## Endpoints disponibles

   - `POST /api/Image/Upload`: Sube una imagen
   - `POST /api/Image/BulkDelete`: Elimina imágenes en masa
   - `GET /api/Image/Getimages`: Recupera todas las imágenes
   - `POST /api/User/login`: Autentica un usuario

## Contribuciones

Las contribuciones son bienvenidas. Por favor, abrí un issue o un pull request para discutir cualquier cambio que quieras realizar.


