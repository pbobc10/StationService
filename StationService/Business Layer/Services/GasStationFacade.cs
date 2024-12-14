using AutoMapper;
using StationService.Business_Layer.Interfaces;
using StationService.DTOs;
using StationService.Interfaces;
using StationService.Models;
using StationService.Services;

namespace StationService.Business_Layer.Services
{
    public class GasStationFacade : IGasStationFacade
    {
        private readonly IMapper _mapper;
        private readonly IGasStationRepository _gasStationRepository;


        public GasStationFacade(IMapper mapper, IGasStationRepository gasStationRepository)
        {
            _mapper = mapper;
            _gasStationRepository = gasStationRepository;
        }

        public async Task AddAsync(GasStationInputDto inputDto)
        {
            var gasStation = _mapper.Map<GasStation>(inputDto);
            await _gasStationRepository.AddAsync(gasStation);
        }

        public async Task DeleteAsync(int id)
        {
            await _gasStationRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<GasStationOutputDto>> GetAllAsync()
        {
            var gasStations = await _gasStationRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GasStationOutputDto>>(gasStations);
        }

        public async Task<GasStationOutputDetailDto> GetByIdAsync(int id)
        {
            var gasStation = await _gasStationRepository.GetAsync(id);
            return _mapper.Map<GasStationOutputDetailDto>(gasStation);
        }

        public async Task UpdateAsync(int id, GasStationInputDto inputDto)
        {
            var gasStation = await _gasStationRepository.GetAsync(id);

            if (gasStation == null)
            {

                throw new KeyNotFoundException("gasStation not found.");
            }
            _mapper.Map(inputDto, gasStation);
            await _gasStationRepository.UpdateAsync(gasStation);
        }
    }
}
