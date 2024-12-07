using Microsoft.EntityFrameworkCore;
using StationService.Data;
using StationService.Interfaces;
using StationService.Models;


namespace StationService.Services
{
    public class GasMeterService : IGasMeterRepository
    {
        private readonly StationeServiceContext _context;
        private readonly ILogger<GasMeterService> _logger;

        public GasMeterService(StationeServiceContext context, ILogger<GasMeterService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAsync(GasMeter entity)
        {
           await _context.GasMeters.AddAsync(entity);
            // save change to the database
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var gasMeter = await _context.GasMeters.FindAsync(id);
                if (gasMeter != null)
                {
                    _context.GasMeters.Remove(gasMeter);
                    // save chagne to the database
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("GasMeter with ID {Id} was successfully deleted.", gasMeter.Id);

                }
                else
                {
                    _logger.LogInformation("GasMeter with ID {Id} was not exist.", id);
                }
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "An error occurred while deleting the GasMeter with ID {Id}",id);

                // Rethrow the exception if needed
                throw;
            }
        }

        public async Task<IEnumerable<GasMeter>> GetAllAsync()
        {
            return await _context.GasMeters.ToListAsync();
        }

        public async Task<GasMeter> GetAsync(int id)
        {
            return await _context.GasMeters.FindAsync(id);
        }

        public async Task UpdateAsync(GasMeter entity)
        {
            try
            {
                // Check if the entity exists
                var existingGasMeters = await _context.GasMeters.FindAsync(entity.Id);

                if (existingGasMeters == null)
                {
                    _logger.LogWarning("GasMeter with ID {Id} not found for update.", entity.Id);
                    throw new InvalidOperationException($"GasMeter with ID {entity.Id} not found.");
                }

                // Update the entity properties
                existingGasMeters = entity;

                // Save changes to the database
                await _context.SaveChangesAsync();

                _logger.LogInformation("GasMeter with ID {Id} was successfully updated.", entity.Id);
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "An error occurred while updating the GasMeter with ID {Id}", entity.Id);

                // Rethrow the exception if needed
                throw;
            }
        }

    }
}
