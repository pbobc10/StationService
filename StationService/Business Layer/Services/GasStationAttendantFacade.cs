using AutoMapper;
using StationService.Business_Layer.Interfaces;
using StationService.DTOs;
using StationService.Interfaces;
using StationService.Models;

namespace StationService.Business_Layer.Services
{
    public class GasStationAttendantFacade : IGasStationAttendantFacade
    {
        private readonly IGasStationAttendantRepository _gasStationAttendantRepository;
        private readonly IMapper _mapper;
        public GasStationAttendantFacade(IGasStationAttendantRepository gasStationAttendantRepository, IMapper mapper)
        {
            _mapper = mapper;
            _gasStationAttendantRepository = gasStationAttendantRepository;
        }

        public async Task AddAsync(GasStationAttendantInputDto inputDto)
        {
            var gasStationAttendant = _mapper.Map<GasStationAttendant>(inputDto);
            await _gasStationAttendantRepository.AddAsync(gasStationAttendant);
        }

        public async Task DeleteAsync(int id)
        {
            await _gasStationAttendantRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<GasStationAttendantOutputDto>> GetAllAsync()
        {
            var gasStationAttendants = await _gasStationAttendantRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GasStationAttendantOutputDto>>(gasStationAttendants);
        }

        public async Task<GasStationAttendantOutputDto> GetByIdAsync(int id)
        {
            var gasStationAttendant = await _gasStationAttendantRepository.GetAsync(id);
            return _mapper.Map<GasStationAttendantOutputDto>(gasStationAttendant);
        }

        public async Task UpdateAsync(int id, GasStationAttendantInputDto inputDto)
        {
            var gasStationAttendant = await _gasStationAttendantRepository.GetAsync(id);
            if (gasStationAttendant == null)
            {
                throw new KeyNotFoundException("Administrator not found.");
            }
            _mapper.Map(inputDto, gasStationAttendant);
            await _gasStationAttendantRepository.UpdateAsync(gasStationAttendant);
        }
    }
}
