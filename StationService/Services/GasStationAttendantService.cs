using Microsoft.EntityFrameworkCore;
using StationService.Data;
using StationService.Interfaces;
using StationService.Models;


namespace StationService.Services
{
    public class GasStationAttendantService : IGasStationAttendantRepository
    {
        private readonly StationeServiceContext _context;
        private readonly ILogger<GasStationAttendantService> _logger;

        public GasStationAttendantService(StationeServiceContext context, ILogger<GasStationAttendantService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAsync(GasStationAttendant entity)
        {
            await _context.GasStationAttendants.AddAsync(entity);
            // save change to the database
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var gasStationAttendant = await _context.GasStationAttendants.FindAsync(id);
                if (gasStationAttendant != null)
                {
                    _context.GasStationAttendants.Remove(gasStationAttendant);
                    // save chagne to the database
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("GasStationAttendant with ID {Id} was successfully deleted.", gasStationAttendant.Id);
                }
                else
                {
                    _logger.LogInformation("GasStationAttendant with ID {Id} was not exist.", id);
                }
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "An error occurred while deleting the GasStationAttendant with ID {Id}", id);

                // Rethrow the exception if needed
                throw;
            }
        }

        public async Task<IEnumerable<GasStationAttendant>> GetAllAsync()
        {
            return await _context.GasStationAttendants.ToListAsync();
        }

        public async Task<GasStationAttendant> GetAsync(int id)
        {
            return await _context.GasStationAttendants.AsNoTracking().Include(g => g.GasStation).FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task UpdateAsync(GasStationAttendant entity)
        {
            try
            {
                // Check if the entity exists
                var existingGasStationAttendants = await _context.GasStationAttendants.AsNoTracking().FirstOrDefaultAsync(g => g.Id == entity.Id);

                if (existingGasStationAttendants == null)
                {
                    _logger.LogWarning("GasStationAttendant with ID {Id} not found for update.", entity.Id);
                    throw new InvalidOperationException($"GasStationAttendant with ID {entity.Id} not found.");
                }

                // Update the entity properties
                existingGasStationAttendants = entity;

                // Mark the entity as modified
                _context.Entry(existingGasStationAttendants).State = EntityState.Modified;

                // Save changes to the database
                await _context.SaveChangesAsync();

                _logger.LogInformation("GasStationAttendant with ID {Id} was successfully updated.", entity.Id);
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "An error occurred while updating the GasStationAttendant with ID {Id}", entity.Id);

                // Rethrow the exception if needed
                throw;
            }
        }

    }
}
