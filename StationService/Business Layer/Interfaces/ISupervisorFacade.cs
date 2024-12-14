using StationService.DTOs;

namespace StationService.Business_Layer.Interfaces
{
    public interface ISupervisorFacade
    {
        Task<SupervisorOutputDto> GetByIdAsync(int id);
        Task<IEnumerable<SupervisorOutputDto>> GetAllAsync();
        Task AddAsync(SupervisorInputDto inputDto);
        Task UpdateAsync(int id, SupervisorInputDto inputDto);
        Task DeleteAsync(int id);
    }
}
