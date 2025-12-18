using AuthMicroService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using StudentMangementSystem.Model.Auth;

namespace AuthMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;

        public AuthController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("Token")]
        public IActionResult Token([FromBody] LoginUser model)
        {
            // TODO: validate username/password from DB
            // Example only:

            if (model.Username != "admin" || model.Password != "123")
                return Unauthorized("Invalid credentials");

            var token = _tokenService.GenerateToken(
                userId: "1",
                email: "admin@test.com",
                role: "Admin");

            return Ok(new { token });
        }
    }
}
