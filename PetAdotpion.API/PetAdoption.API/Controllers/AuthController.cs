using Microsoft.AspNetCore.Authorization;
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
    public class AuthController(IWebHostEnvironment _env, IAuthService _authService) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<ActionResult<TokenResponseDTO>> RegisterUser([FromBody] RegisterViewModel model)
        {
            try
            {
                if (model is null || model.Email is null || string.IsNullOrEmpty(model.Password))
                    return BadRequest("Email and Password are required.");

                if (await _authService.GetUser(x => x.Email == model.Email) != null)
                    return BadRequest($"User is already exist against {model.Email}");

                string fileName = null;
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads/users");

                if (model.ProfilePhoto != null)
                {
                    try
                    {
                        // Remove data URL prefix if present
                        var base64Data = model.ProfilePhoto.Base64Data;
                        var base64Index = base64Data.IndexOf("base64,");
                        if (base64Index >= 0)
                        {
                            base64Data = base64Data.Substring(base64Index + 7);
                        }

                        var imageBytes = Convert.FromBase64String(base64Data);

                        // Generate a unique filename
                        var fileExt = Path.GetExtension(model.ProfilePhoto.FileName);
                        fileName = $"{Guid.NewGuid()}{fileExt}";
                        var filePath = Path.Combine(uploadsFolder, fileName);

                        await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Profile Images not able to process");
                    }
                }

                User user = await _authService.RegisterUserAsync(new RegisterDTO()
                {
                    Email = model.Email,
                    Name = model.Name,
                    Password = model.Password,
                    ProfilePhoto = fileName
                });

                if (user is null)
                {
                    return BadRequest("User not successful Register. Please try again!");
                }

                TokenResponseDTO tokens = await _authService.LoginUserAsync(user);

                if (tokens == null)
                {
                    return BadRequest("Somthing error occur");
                }

                return Ok(tokens);
            }
            catch (Exception ex)
            {
                return BadRequest("User not successful Register. Please try again!");
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<TokenResponseDTO>> LoginUser([FromBody] LoginViewModel model)
        {
            try
            {
                if (model is null || model.Email is null || string.IsNullOrEmpty(model.Password))
                    return BadRequest("Email and Password are required.");

                User user = await _authService.GetUser(x => x.Email == model.Email);
                if (user == null)
                {
                    return BadRequest($"User is not exist against {model.Email}");
                }

                bool passwordIsValid = await _authService.CheckUsePassword(user, model.Password);

                if (string.IsNullOrEmpty(user.PasswordHash) && passwordIsValid)
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
            catch (Exception ex)
            {
                return BadRequest("User not successful login. Please try again! Kidnly check your email and name.");
            }
        }

        [HttpPost("RefreshToken")]
        public async Task<ActionResult<TokenResponseDTO>> Refresh(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest(new { Message = "Refresh token is required." });

            var user = await _authService.GetUser(u => u.RefreshToken == refreshToken);

            if (user == null)
                return Unauthorized(new { Message = "Invalid refresh token." });

            if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return Unauthorized(new { Message = "Refresh token has expired." });

            return await _authService.RegenrateToken(user);
        }
    }
}
