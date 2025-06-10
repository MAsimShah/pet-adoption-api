using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetAdoption.API.Models;
using PetAdoption.Application.DTO;
using PetAdoption.Application.Interfaces;
using PetAdoption.Domain;

namespace PetAdoption.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController(IAuthService _authService) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<ActionResult<User>> RegisterUser(RegisterViewModel model)
        {
            if (await _authService.GetUser(x => x.Email == model.Email) != null)
            {
                return BadRequest($"User is already exist against {model.Email}");
            }

            User user = await _authService.RegisterUserAsync(new RegisterDTO()
            {
                Email = model.Email,
                Name = model.Name,
                Password = model.Password
            });

            return Ok(user);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<TokenResponseDTO>> LoginUser(RegisterViewModel model)
        {
            User user = await _authService.GetUser(x => x.Email == model.Email);
            if (user == null)
            {
                return BadRequest($"User is not exist against {model.Email}");
            }

            if (!string.IsNullOrEmpty(user.PasswordHash) && new PasswordHasher<IdentityUser>().VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Failed)
            {
                return BadRequest("Password is not correct");
            }

            TokenResponseDTO tokens = await _authService.LoginUserAsync(user);

            if (tokens == null)
            {
                return BadRequest("Somthing error occur");
            }

            return Ok(tokens);
        }
    }
}
