using StationService.DTOs;

namespace StationService.Business_Layer.Interfaces
{
    public interface IAdministratorFacade
    {
        Task<AdministratorOutputDetailDto> GetByIdAsync(int id);
        Task<IEnumerable<AdministratorOutputDto>> GetAllAsync();
        Task AddAsync(AdministratorInputDto inputDto);
        Task UpdateAsync(int id, AdministratorInputDto inputDto);
        Task DeleteAsync(int id);
    }
}
