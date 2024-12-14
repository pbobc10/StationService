using AutoMapper;
using StationService.Business_Layer.Interfaces;
using StationService.DTOs;
using StationService.Interfaces;
using StationService.Models;

namespace StationService.Business_Layer.Services
{
    public class SupervisorFacade : ISupervisorFacade
    {
        private readonly ISupervisorRepository _supervisorRepository;
        private readonly IMapper _mapper;
        public SupervisorFacade(ISupervisorRepository supervisorRepository, IMapper mapper)
        {
            _mapper = mapper;
            _supervisorRepository = supervisorRepository;
        }

        public async Task AddAsync(SupervisorInputDto inputDto)
        {
            var supervisor = _mapper.Map<Supervisor>(inputDto);
            await _supervisorRepository.AddAsync(supervisor);
        }

        public async Task DeleteAsync(int id)
        {
            await _supervisorRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<SupervisorOutputDto>> GetAllAsync()
        {
            var supervisors = await _supervisorRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SupervisorOutputDto>>(supervisors);
        }

        public async Task<SupervisorOutputDto> GetByIdAsync(int id)
        {
            var supervisor = await _supervisorRepository.GetAsync(id);
            return _mapper.Map<SupervisorOutputDto>(supervisor);
        }

        public async Task UpdateAsync(int id, SupervisorInputDto inputDto)
        {
            var supervisor = await _supervisorRepository.GetAsync(id);
            if (supervisor == null)
            {
                throw new KeyNotFoundException("Administrator not found.");
            }
            _mapper.Map(supervisor, inputDto);
            await _supervisorRepository.UpdateAsync(supervisor);
        }
    }
}
