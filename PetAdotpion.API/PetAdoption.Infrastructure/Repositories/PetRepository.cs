using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetAdoption.Application.Interfaces.InfrastructureInterfaces;
using PetAdoption.Domain;
using PetAdoption.Infrastructure.Interfaces;
using System.Linq.Expressions;

namespace PetAdoption.Infrastructure.Repositories
{
    public class PetRepository(IGenericRepository<Pet> petRepo) : IPetRepository
    {
        private readonly IGenericRepository<Pet> _petRepo = petRepo;

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

        public async Task<Pet> GetAsync(Expression<Func<Pet, bool>> predicate)
        {
            return await _petRepo.GetAsync(predicate, a => a.PetPhotos);
        }
    }
}