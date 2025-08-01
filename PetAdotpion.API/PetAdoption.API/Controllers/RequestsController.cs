using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetAdoption.API.Interfaces;
using PetAdoption.Application.DTO;
using PetAdoption.Application.Interfaces;

namespace PetAdoption.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // 👈 Require authentication for all endpoints
    public class RequestsController(IPetRequestService _requestRepo, IUserContextService _userContext) : ControllerBase
    {
        /// <summary>
        /// Get all pets requests
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-list")]
        public async Task<IActionResult> GetAllPets()
        {
            var userId = _userContext.UserId;
            var requests = await _requestRepo.GetAllRequestsAsync(userId);
            return Ok(requests);
        }

        /// <summary>
        /// Get pet by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetPetById(int id)
        {
            var request = await _requestRepo.GetRequestByIdAsync(id);
            if (request == null)
                return NotFound();

            return Ok(request);
        }

        /// <summary>
        /// Add a New Pet's request
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<IActionResult> AddRequest([FromBody] PetRequestDTO requestDto)
        {
            if (requestDto == null)
                return BadRequest("Pet Request is invalid");

            requestDto = await _requestRepo.AddRequestAsync(requestDto);
            return Ok(requestDto);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<IActionResult> UpdatePet([FromBody] PetRequestDTO requestDto)
        {
            if (requestDto is null || requestDto.Id <= 0)
                return BadRequest("Pet's Request ID is invalid for update record");

            requestDto = await _requestRepo.UpdateRequestAsync(requestDto);
            return Ok(requestDto);
        }

        /// <summary>
        /// Delete request of pet
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeletePet(int id)
        {
            await _requestRepo.DeleteRequestAsync(id);
            return Ok();
        }
    }
}