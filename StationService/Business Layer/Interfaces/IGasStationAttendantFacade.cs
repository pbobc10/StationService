using StationService.DTOs;

namespace StationService.Business_Layer.Interfaces
{
    public interface IGasStationAttendantFacade
    {
        Task<GasStationAttendantOutputDto> GetByIdAsync(int id);
        Task<IEnumerable<GasStationAttendantOutputDto>> GetAllAsync();
        Task AddAsync(GasStationAttendantInputDto inputDto);
        Task UpdateAsync(int id, GasStationAttendantInputDto inputDto);
        Task DeleteAsync(int id);
    }
}
