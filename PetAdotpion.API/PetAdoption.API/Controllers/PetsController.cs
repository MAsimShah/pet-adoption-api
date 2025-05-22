using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PetAdoption.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // 👈 Require authentication for all endpoints
    public class PetsController : ControllerBase
    {
        //private readonly PetService _petService;

        //public PetsController(PetService petService)
        //{
        //    _petService = petService;
        //}

        /// <summary>
        /// Get all pets
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous] // 👈 Allow anyone to view pets
        public async Task<IActionResult> GetAllPets()
        {
            //var pets = await _petService.GetAllPetsAsync();
            return Ok();
        }

        ///// <summary>
        ///// Get pet by id
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetPetById(int id)
        //{
        //    var pet = await _petService.GetPetByIdAsync(id);
        //    if (pet == null)
        //        return NotFound();

        //    return Ok(pet);
        //}

        ///// <summary>
        ///// Add a New Pet
        ///// </summary>
        ///// <param name="petDto"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<IActionResult> AddPet([FromBody] PetDto petDto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    await _petService.AddPetAsync(petDto);
        //    return CreatedAtAction(nameof(GetPetById), new { id = petDto.Id }, petDto);
        //}

        ///// <summary>
        ///// Update Pet Details
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="petDto"></param>
        ///// <returns></returns>
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdatePet(int id, [FromBody] PetDto petDto)
        //{
        //    if (id != petDto.Id)
        //        return BadRequest("Pet ID mismatch");

        //    await _petService.UpdatePetAsync(petDto);
        //    return NoContent();
        //}

        ///// <summary>
        ///// Delete a Pet
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePet(int id)
        //{
        //    await _petService.DeletePetAsync(id);
        //    return NoContent();
        //}
    }
}