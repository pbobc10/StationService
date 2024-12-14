using Microsoft.EntityFrameworkCore;
using StationService.Data;
using StationService.Interfaces;
using StationService.Models;


namespace StationService.Services
{
    public class GasStationService : IGasStationRepository
    {
        private readonly StationeServiceContext _context;
        private readonly ILogger<GasStationService> _logger;

        public GasStationService(StationeServiceContext context, ILogger<GasStationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAsync(GasStation entity)
        {
            await _context.GasStations.AddAsync(entity);
            // save change to the database
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var gasStation = await _context.GasStations.FindAsync(id);
                if (gasStation != null)
                {
                    _context.GasStations.Remove(gasStation);
                    // save chagne to the database
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("GasStation with ID {Id} was successfully deleted.", gasStation.Id);

                }
                else
                {
                    _logger.LogInformation("GasStation with ID {Id}  not exist.", id);
                }

            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "An error occurred while deleting the GasStation with ID {Id}", id);

                // Rethrow the exception if needed
                throw;
            }
        }

        public async Task<IEnumerable<GasStation>> GetAllAsync()
        {
            return await _context.GasStations.AsNoTracking().Include(gs => gs.Administrator).Include(gs => gs.Supervisor).ToListAsync();
        }

        public async Task<GasStation> GetAsync(int id)
        {
            return await _context.GasStations.AsNoTracking().Include(gs => gs.Administrator).Include(gs => gs.Supervisor)
                .Include(gs => gs.GasStationAttendants).Include(gs => gs.DispensingUnits)
                .FirstOrDefaultAsync(gs => gs.Id == id);
        }

        public async Task UpdateAsync(GasStation entity)
        {
            try
            {
                // Check if the entity exists
                var existingGasStations = await _context.GasStations.AsNoTracking().Include(g => g.DispensingUnits).Include(g => g.GasStationAttendants).FirstOrDefaultAsync(g => g.Id == entity.Id);

                if (existingGasStations == null)
                {
                    _logger.LogWarning("GasStation with ID {Id} not found for update.", entity.Id);
                    throw new InvalidOperationException($"GasStation with ID {entity.Id} not found.");
                }

                // Update the entity properties
                existingGasStations = entity;

                // Mark the entity as modified
                _context.Entry(existingGasStations).State = EntityState.Modified;

                // Save changes to the database
                await _context.SaveChangesAsync();

                _logger.LogInformation("GasStation with ID {Id} was successfully updated.", entity.Id);
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "An error occurred while updating the GasStation with ID {Id}", entity.Id);

                // Rethrow the exception if needed
                throw;
            }
        }

    }
}
