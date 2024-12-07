using Microsoft.EntityFrameworkCore;
using StationService.Data;
using StationService.Interfaces;
using StationService.Models;

namespace StationService.Services
{
    public class AdministratorService : IAdministratorRepository
    {
        private readonly StationeServiceContext _context;
        private readonly ILogger<AdministratorService> _logger;

        public AdministratorService(StationeServiceContext context, ILogger<AdministratorService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAsync(Administrator entity)
        {
            await _context.Administrators.AddAsync(entity);

            // save changes to the database
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                 //Find the Administrator by id
                var administrator = await _context.Administrators.FindAsync(id);

                // Check if the Administrator exists
                if (administrator == null)
                {
                    _logger.LogWarning("Administrator with ID {Id} not found for deletion.", id);
                    throw new InvalidOperationException($"Administrator with ID {id} not found.");
                }

                // Remove the Administrator from the DbSet
                _context.Administrators.Remove(administrator);

                // save changes to the database
                await _context.SaveChangesAsync();

                _logger.LogInformation("Administrator with ID {Id} was successfully deleted.", administrator.Id);
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "An error occurred while deleting the Administrator with ID {Id}", id);

                // Rethrow the exception if needed
                throw;
            }

        }

       
        public async Task<IEnumerable<Administrator>> GetAllAsync()
        {
            return await _context.Administrators.ToListAsync();
        }

        public async Task<Administrator> GetAsync(int id)
        {
            return await _context.Administrators.AsNoTracking().Include(s => s.Stations).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task UpdateAsync(Administrator entity)
        {
            try
            {
                // check if the entity exists
                var existingAdministrator = await _context.Administrators.AsNoTracking().FirstOrDefaultAsync(a => a.Id == entity.Id);
                              
                if (existingAdministrator == null)
                {
                    _logger.LogWarning("Administrator with ID {Id} not found for update.", entity.Id);
                    throw new InvalidOperationException($"Administrator with ID {entity.Id} not found.");
                }

                // Update the entity properties
              existingAdministrator = entity;

                // Mark the entity as modified

                _context.Entry(existingAdministrator).State = EntityState.Modified;

                // save changes to the database
                await _context.SaveChangesAsync();

                _logger.LogInformation("Administrator with ID {Id} was successfully updated.", entity.Id);
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "An error occurred while updating the Administrator with ID {Id}", entity.Id);

                // Rethrow the exception if needed
                throw;
            }
        }
    }
}
