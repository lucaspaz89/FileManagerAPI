﻿using FileManagerAPI.Models;
using FileManagerAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoginRepository _loginRepository;

        public UserController(IUserRepository userRepository, ILoginRepository loginRepository)
        {
            _userRepository = userRepository;
            _loginRepository = loginRepository;
        }

        [HttpPost("Registrar")]
        public async Task<ActionResult<UserRegistration>> CreateUser(UserRegistration user)
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
                    return BadRequest("El mail ingresado ya existe");
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserLogin>> Login([FromBody] UserLogin user)
        {
            try
            {
                string response = await _loginRepository.Login(user);

                if (response != "Error")
                {
                    return Ok($"Token creado con exito: {response}");
                }
                else
                {
                    return NotFound("Credenciales inavlidas");
                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
