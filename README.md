# JuliBack

JuliBack es una aplicación web desarrollada en .NET 9 y C# 13.0 que permite la gestión de imágenes y usuarios. Utiliza Cloudinary para el almacenamiento de imágenes y MySQL como base de datos.

## Características

- Subida de imágenes con Cloudinary
- Gestión de usuarios
- Autenticación de usuarios
- Eliminación masiva de imágenes
- Recuperación de imágenes para inyección en el frontend


## Configuración

1. **Clonar el repositorio**

2. **Configurar la base de datos**

  Crea una base de datos en MySQL y actualiza la cadena de conexión en `appsettings.json`

3. **Configurar Cloudinary**

   Actualiza las credenciales de Cloudinary en `appsettings.json`:

   
## Ejecución

1. **Compilar y ejecutar la aplicación**

   Abre el proyecto en Visual Studio y presiona `F5` para compilar y ejecutar la aplicación.

2. **Endpoints disponibles**

   - `POST /api/Image/Upload`: Sube una imagen
   - `POST /api/Image/BulkDelete`: Elimina imágenes en masa
   - `GET /api/Image/Getimages`: Recupera todas las imágenes
   - `POST /api/User/login`: Autentica un usuario

## Estructura del proyecto

- **Controllers**: Contiene los controladores de la API.
- **Repositories**: Contiene las interfaces y clases de repositorio para acceder a la base de datos.
- **Services**: Contiene los servicios para la lógica de negocio, como la integración con Cloudinary.
- **Contexto**: Contiene la configuración del contexto de la base de datos.

## Contribuciones

Las contribuciones son bienvenidas. Por favor, abrí un issue o un pull request para discutir cualquier cambio que quieras realizar.

## Licencia

Este proyecto está licenciado bajo la Licencia MIT. Consulta el archivo `LICENSE` para más detalles.

