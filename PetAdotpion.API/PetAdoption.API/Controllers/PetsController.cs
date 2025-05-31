using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetAdoption.Application.DTO;
using PetAdoption.Application.Interfaces;

namespace PetAdoption.Api.Controllers
{
    public record PetFilesUploadRequest(int PetId, List<string> Images);

    public record Base64ImageFile(string FileName, string Base64Data);
    public record Base64UploadRequest(int PetId, List<Base64ImageFile> Images);

    [Route("api/[controller]")]
    [ApiController]
   // [Authorize] // 👈 Require authentication for all endpoints
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;
        private readonly IWebHostEnvironment _env;

        public PetsController(IWebHostEnvironment env, IPetService petService)
        {
            _env = env;
            _petService = petService;
        }

        /// <summary>
        /// Get all pets
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-list")]
        [AllowAnonymous] // 👈 Allow anyone to view pets
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

        [HttpPost("Upload-pet-files")]
        public async Task<IActionResult> SaveFileAsync([FromBody] Base64UploadRequest request)
        {
            if (request.Images == null || request.Images.Count == 0 || request.PetId < 1)
                return BadRequest("No files uploaded.");

            var savedFilePaths = new List<string>();
            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads/pets");
            // Directory.CreateDirectory(uploadsFolder);

            foreach (var Image in request.Images)
            {
                //var base64Data = Image.Substring(Image.IndexOf(',') + 1);
                //byte[] fileBytes = Convert.FromBase64String(base64Data);




                //var filePath = Path.Combine(uploadsFolder, file.FileName);

                //using var stream = new FileStream(filePath, FileMode.Create);
                //await file.CopyToAsync(stream);

                //// Save relative path like "uploads/pets/filename.pdf"
                //var relativePath = Path.Combine("uploads/pets", file.FileName).Replace("\\", "/");
                //savedFilePaths.Add(relativePath);
            }

            if (savedFilePaths.Any())
            {
                await _petService.SavePetPhotosAsync(request.PetId, savedFilePaths);
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
            if (petDto is null && petDto.Id <= 0)
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
            return NoContent();
        }

        /// <summary>
        /// Delete a Photo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeletePhoto/{id}")]
        public async Task<IActionResult> DeletePetPhoto(int id)
        {
            await _petService.DeletePetPhotoAsync(id);
            return NoContent();
        }
    }
}