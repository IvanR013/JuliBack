using Microsoft.AspNetCore.Mvc; // Para utilizar los atributos y clases de MVC
using JuliBack.Repositories;
using JuliBack.Services;      // Para acceder a tu contexto de datos (reemplaza YourNamespace con tu espacio de nombres real)


namespace JuliBack.Controllers;

//http://localhost:5081/api/Image/Getimages
[ApiController]
[Route("api/[controller]")]
public partial class ImageController : ControllerBase
{
    private readonly IimageRepository _imageRepository;
    private readonly CloudinaryService _cloudinaryService;
    public ImageController(IimageRepository _imageRepository, CloudinaryService _cloudinaryService)
    {
        this._imageRepository = _imageRepository;
        this._cloudinaryService = _cloudinaryService;
    }


    //Sube los datos del form de carga de imágenes
    [HttpPost("Upload")]
    public async Task<IActionResult> Upload([FromForm] IFormFile imageFile, [FromForm] string tittle, [FromForm] string description, [FromForm] string section, [FromForm] string RedirectUrl)
    {
        if (imageFile == null || imageFile.Length == 0) return BadRequest(new { err = "No se encontraron archivos." });


        if (string.IsNullOrEmpty(tittle) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(section) || string.IsNullOrEmpty(RedirectUrl))

            return BadRequest(new { err = "Todos los campos del formulario son obligatorios." });

        try
        {
            await _cloudinaryService.SaveManager(imageFile, tittle, description, section, RedirectUrl);

            return Ok(new { Status = "Success" });
        }

        catch (Exception ex)
        {

            return StatusCode(500, new { ErrMessage = "Error de conexión.", ErrMensaje = ex.Message });
        }
    }
}

public partial class ImageController : ControllerBase
{
    //Endpoint de eliminación
    [HttpPost("BulkDelete")]
    public async Task<IActionResult> BulkDelete([FromBody] List<int> ids)
    {

        if (ids == null || ids.Count == 0) return NotFound(new { Error = "No se encontraron las imágenes." });

        await _imageRepository.DeleteAsync(ids);

        return Ok(new { Status = "Success" });
    }

    //Endpoint de retorno al front para la inyeccion de imágenes.
    [HttpGet("Getimages")]
    public async Task<IActionResult> GetimagesAsync()
    {
        var imgs = await _imageRepository.GetAllImagesAsync();

        if (imgs == null || imgs.Count == 0) return NotFound(new { err = "Datos no encontrados" });

        return Ok(new { status = "Success", imgs });
    }
}

