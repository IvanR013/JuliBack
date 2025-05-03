using Microsoft.AspNetCore.Mvc; // Para utilizar los atributos y clases de MVC
using JuliBack.Repositories;
using JuliBack.Models;


namespace JuliBack.Controllers;

//http://localhost:5081/api/User/login
[ApiController]
[Route("api/[controller]")]
public partial class UserController : ControllerBase
{
    private readonly IuserRepository _userRepository;
    public UserController(IuserRepository userRepository) => _userRepository = userRepository;

    [HttpPost("login")]
    public async Task<IActionResult> AuthenticateUser([FromBody] Users loginRequest)
    {
        if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))

            return BadRequest(new { Err = "Parámetros de solicitud no válidos." });


        var IsValidUser = await _userRepository.ValidateUserCredentialsAsync(loginRequest.Username, loginRequest.Password);

        if (!IsValidUser) return Unauthorized(new { Err = "Contraseña Incorrecta." });

        return Ok(new { Status = "Success" });
    }


}


