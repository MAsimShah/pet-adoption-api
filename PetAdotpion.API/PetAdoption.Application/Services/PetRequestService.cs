using AutoMapper;
using PetAdoption.Application.DTO;
using PetAdoption.Application.Interfaces;
using PetAdoption.Application.Interfaces.InfrastructureInterfaces;
using PetAdoption.Domain;

namespace PetAdoption.Application.Services
{
    public class PetRequestService(IMapper _mapper, IPetRequestRepository _requestRepository, IPetRepository _petRepository) : IPetRequestService
    {
        public async Task<IEnumerable<PetRequestDTO>> GetAllRequestsAsync(string userId = "")
        {
            var requests = string.IsNullOrEmpty(userId) ? await _requestRepository.GetAllRequestsAsync() : await _requestRepository.GetAllRequestsAsync(x => x.UserId == userId);
            var petDtos = _mapper.Map<IEnumerable<PetRequestDTO>>(requests);

            return petDtos;
        }

        public async Task<IEnumerable<PetRequestDTO>> GetAllUserRequestsAsync(string userId)
        {
            var requests = await _requestRepository.GetAllRequestsAsync(x => x.UserId != userId);
            var petDtos = _mapper.Map<IEnumerable<PetRequestDTO>>(requests);

            return petDtos;
        }

        public async Task<PetRequestDTO?> GetRequestByIdAsync(int id)
        {
            PetRequest pet = await _requestRepository.GetAsync(x => x.Id == id);
            PetRequestDTO dto = _mapper.Map<PetRequestDTO>(pet);

            return dto;
        }

        public async Task<PetRequestDTO> AddRequestAsync(PetRequestDTO requestDto)
        {
            if (requestDto == null || requestDto.PetId <= 0)
            {
                throw new ArgumentNullException(nameof(requestDto), "Invalid request, Please try again.");
            }

            var pet = await _petRepository.GetAsync(x => x.Id == requestDto.PetId);

            if (pet == null)
            {
                throw new ArgumentException("Pet not found for the request.");
            }

            PetRequest? request = _mapper.Map<PetRequest>(requestDto);
            request.OwnerId = pet.UserId;

            await _requestRepository.AddRequestAsync(request);

            requestDto.Id = request.Id;

            return requestDto;
        }

        public async Task<PetRequestDTO> UpdateRequestAsync(PetRequestDTO requestDto)
        {
            PetRequest request = await _requestRepository.GetAsync(x => x.Id == requestDto.Id);

            if (request is null)
            {
                throw new ArgumentException("Request not found.");
            }

            var pet = await _petRepository.GetAsync(x => x.Id == requestDto.PetId);
            if (pet is null)
            {
                throw new ArgumentException("Pet not found for the request.");
            }

            request.UpdatedAt = DateTime.UtcNow;
            request.PetId = requestDto.PetId;
            request.OwnerId = pet.UserId;
            request.Message = requestDto.Message;
            request.RequestDate = requestDto.RequestDate;

            await _requestRepository.UpdateRequestAsync(request);
            return requestDto;
        }

        public async Task<PetRequestDTO> UpdateRequestStatusAsync(int requestId, string ownerId, RequestStatus Status)
        {

            var request = await _requestRepository.GetAsync(x => x.Id == requestId);

            if (request == null || request.OwnerId != ownerId)
            {
                throw new ArgumentException("Request not found or you do not have permission to update it.");
            }

            request.UpdatedAt = DateTime.UtcNow;
            request.Status = Status;

            await _requestRepository.UpdateRequestAsync(request);

            return _mapper.Map<PetRequestDTO>(request);
        }

        public async Task DeleteRequestAsync(int id)
        {
            await _requestRepository.DeleteRequestAsync(id);
        }
    }
}
