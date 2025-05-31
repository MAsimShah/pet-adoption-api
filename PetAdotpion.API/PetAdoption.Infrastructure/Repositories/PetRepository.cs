using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetAdoption.Application.Interfaces.InfrastructureInterfaces;
using PetAdoption.Domain;
using PetAdoption.Infrastructure.Interfaces;
using System.Linq.Expressions;

namespace PetAdoption.Infrastructure.Repositories
{
    public class PetRepository(IGenericRepository<Pet> petRepo, IGenericRepository<PetPhoto> petPhotoRepo) : IPetRepository
    {
        private readonly IGenericRepository<Pet> _petRepo = petRepo;
        private readonly IGenericRepository<PetPhoto> _petPhotoRepo = petPhotoRepo;

        public async Task<IEnumerable<Pet>> GetAllPetsAsync(Expression<Func<Pet, bool>> predicate)
        {
            return await _petRepo.ListAsync(predicate, a => a.PetPhotos);
        }

        public async Task<Pet?> GetPetByIdAsync(int id)
        {
            return await _petRepo.GetAsync(x => x.Id == id);
        }

        public async Task AddPetAsync(Pet pet)
        {
            await _petRepo.AddAsync(pet);
            await _petRepo.SaveAsync();
        }

        public async Task UpdatePetAsync(Pet pet)
        {
            await _petRepo.UpdateWithSaveAsync(pet);
        }

        public async Task DeletePetAsync(int id)
        {
            var pet = await _petRepo.GetAsync(x => x.Id == id);
            if (pet != null)
            {
                await _petRepo.DeleteWithSaveAsync(pet);
            }
        }

        public async Task DeletePetPhotoAsync(int id)
        {
            var photo = await _petPhotoRepo.GetAsync(x => x.Id == id);
            if (photo != null)
            {
                await _petPhotoRepo.DeleteWithSaveAsync(photo);
            }
        }

        public async Task SavePetPhotosAsync(List<PetPhoto> entities)
        {
            if (entities == null || !entities.Any())
                throw new Exception("No files uploaded.");

            //var existingAttachments = await _petPhotoRepo.ListAsync(x => x.PetId == entities.First().PetId);
            //if (existingAttachments != null && existingAttachments.Any()) 
            //{
            //    _petPhotoRepo.DeleteRange(existingAttachments);
            //}

            await _petPhotoRepo.AddRangeWithSaveAsync(entities);
        }

        public async Task<Pet> GetAsync(Expression<Func<Pet, bool>> predicate)
        {
            return await _petRepo.GetAsync(predicate, a => a.PetPhotos);
        }
    }
}