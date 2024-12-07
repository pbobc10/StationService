using AutoMapper;
using StationService.Business_Layer.Interfaces;
using StationService.DTOs;
using StationService.Interfaces;
using StationService.Models;

namespace StationService.Business_Layer.Services
{
    public class AdministratorFacade : IAdministratorFacade
    {
        private readonly IMapper _mapper;
        private readonly IAdministratorRepository _repository;

        public AdministratorFacade( IMapper mapper, IAdministratorRepository repository) {
            _mapper = mapper;
            _repository = repository;
        }

        async Task IAdministratorFacade.AddAsync(AdministratorInputDto inputDto)
        {
            var administator = _mapper.Map<Administrator>(inputDto);
            await _repository.AddAsync(administator);
        }

        async Task IAdministratorFacade.DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        async Task<IEnumerable<AdministratorOutputDto>> IAdministratorFacade.GetAllAsync()
        {
           var administrators = await _repository.GetAllAsync();
            return  _mapper.Map<IEnumerable<AdministratorOutputDto>>(administrators);
        }

        async Task<AdministratorOutputDetailDto> IAdministratorFacade.GetByIdAsync(int id)
        {
            var adminitrator = await _repository.GetAsync(id);
            return _mapper.Map<AdministratorOutputDetailDto>(adminitrator);
        }

        async Task IAdministratorFacade.UpdateAsync(int id, AdministratorInputDto inputDto)
        {

            var administrator =  await _repository.GetAsync(id);
            if (administrator == null)
            {

                throw new KeyNotFoundException("Administrator not found.");
            }
            _mapper.Map(inputDto, administrator); 
            await _repository.UpdateAsync(administrator);
        }
    }
}
