using StationService.DTOs;

namespace StationService.Business_Layer.Interfaces
{
    public interface IGasStationFacade
    {
        Task<GasStationOutputDetailDto> GetByIdAsync(int id);
        Task<IEnumerable<GasStationOutputDto>> GetAllAsync();
        Task AddAsync(GasStationInputDto inputDto);
        Task UpdateAsync(int id, GasStationInputDto inputDto);
        Task DeleteAsync(int id);
    }
}
