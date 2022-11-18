using FileManagerAPI.Helpers;
using FileManagerAPI.Models;
using FileManagerAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserLogin UserOb = new();
        private readonly IUserRepository _userRepository;
        private readonly ILoginRepository _loginRepository;
        private readonly JWTConfiguration _jwtService;

        public UserController(IUserRepository userRepository, ILoginRepository loginRepository, JWTConfiguration jwtService)
        {
            _userRepository = userRepository;
            _loginRepository = loginRepository;
            _jwtService = jwtService;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<UserRegistration>> CreateUser([FromBody] UserRegistration user)
        {
            try
            {
                var response = await _userRepository.CreateUser(user);

                if (response)
                {
                    return Ok("Se ha creado el usuario exitosamente");
                }
                else
                {
                    return BadRequest(new
                    {
                        message = "El email ya existe"
                    }); 
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserLogin>> Login([FromBody] UserLogin user)
        {
            try
            {
                string response = await _loginRepository.Login(user);

                if (response != "Error")
                {
                    var jwt = response.ToString();

                    Response.Cookies.Append("jwt", jwt, new CookieOptions
                    {
                        HttpOnly = true,
                    });

                    return Ok(new { message = "Success" });
                }
                else
                {
                    return BadRequest("Credenciales inavlidas");
                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok(new
            {
                message = "Success"
            });
        }

        [HttpGet("user")]
        public async Task<IActionResult> User()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];

                var token = _jwtService.Verify(jwt);

                int userId = int.Parse(token.Issuer);

                var user = await _loginRepository.GetById(userId);

                return Ok(user);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

    }
}
