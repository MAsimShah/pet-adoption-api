using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetAdoption.Application.DTO;
using PetAdoption.Application.Interfaces;

namespace PetAdoption.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // 👈 Require authentication for all endpoints
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;
        private readonly IWebHostEnvironment _env;
        private readonly IPetPhotoService _petPhotocervice;

        public PetsController(IWebHostEnvironment env, IPetService petService, IPetPhotoService petPhotocervice)
        {
            _env = env;
            _petService = petService;
            _petPhotocervice = petPhotocervice;
        }

        /// <summary>
        /// Get all pets
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-list")]
        public async Task<IActionResult> GetAllPets()
        {
            var pets = await _petService.GetAllPetsAsync();
            return Ok(pets);
        }

        /// <summary>
        /// Get pet by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetPetById(int id)
        {
            var pet = await _petService.GetPetByIdAsync(id);
            if (pet == null)
                return NotFound();

            return Ok(pet);
        }

        /// <summary>
        /// Get all pets info in dropdown
        /// </summary>
        /// <returns></returns>
        [HttpGet("dropdown")]
        public async Task<IActionResult> GetPetsDropdown()
        {
            var dropdownList = await _petService.GetDropdownAsync();
            return Ok(dropdownList);
        }

        /// <summary>
        /// Add a New Pet
        /// </summary>
        /// <param name="petDto"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<IActionResult> AddPet([FromBody] PetDto petDto)
        {
            if (petDto == null)
                return BadRequest("Pet information is invalid");

            petDto = await _petService.AddPetAsync(petDto);
            return Ok(petDto);
        }

        /// <summary>
        /// Upload pets photos
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost("Upload-pet-files")]
        public async Task<IActionResult> SaveFileAsync([FromBody] Base64UploadRequest request)
        {
            if (request.Images == null || request.Images.Count == 0 || request.PetId < 1)
                return BadRequest("No files uploaded.");

            var savedFilePaths = new List<string>();
            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads/pets");

            foreach (var image in request.Images)
            {
                try
                {
                    // Remove data URL prefix if present
                    var base64Data = image.Base64Data;
                    var base64Index = base64Data.IndexOf("base64,");
                    if (base64Index >= 0)
                    {
                        base64Data = base64Data.Substring(base64Index + 7);
                    }

                    var imageBytes = Convert.FromBase64String(base64Data);

                    // Generate a unique filename
                    var fileExt = Path.GetExtension(image.FileName);
                    var fileName = $"{Guid.NewGuid()}{fileExt}";
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

                    // Store relative path for DB (e.g. "uploads/pets/abc.jpg")
                    var relativePath = Path.Combine("uploads", "pets", fileName).Replace("\\", "/");
                    savedFilePaths.Add(relativePath);
                }
                catch (Exception ex)
                {
                    throw new Exception("Images not valid");
                }
            }

            if (savedFilePaths.Any())
            {
                await _petPhotocervice.SavePetPhotosAsync(request.PetId, savedFilePaths);
            }

            return Ok(savedFilePaths); // return list of saved file paths
        }

        /// <summary>
        /// Update Pet Details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="petDto"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<IActionResult> UpdatePet([FromBody] PetDto petDto)
        {
            if (petDto is null || petDto.Id <= 0)
                return BadRequest("Pet ID is invalid for update record");

            petDto = await _petService.UpdatePetAsync(petDto);
            return Ok(petDto);
        }

        /// <summary>
        /// Delete a Pet
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeletePet(int id)
        {
            await _petService.DeletePetAsync(id);
            return Ok();
        }

        /// <summary>
        /// Delete a Photo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeletePhoto/{id}")]
        public async Task<IActionResult> DeletePetPhoto(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id is invalid");
            }

            var exitingPhoto = await _petPhotocervice.GetAsync(x => x.Id == id);

            if (exitingPhoto != null)
            {
                var filePath = Path.Combine(_env.WebRootPath, exitingPhoto.PhotoUrl);

                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
            }

            await _petPhotocervice.DeletePetPhotoAsync(id);
            return Ok();
        }
    }

    public record PetFilesUploadRequest(int PetId, List<string> Images);

    public record Base64ImageFile(string FileName, string Base64Data);
    public record Base64UploadRequest(int PetId, List<Base64ImageFile> Images);
}