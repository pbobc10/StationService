using Microsoft.EntityFrameworkCore;
using StationService.Data;
using StationService.Interfaces;
using StationService.Models;


namespace StationService.Services
{
    public class DispensingUnitService : IDispensingUnitRepository
    {
        private readonly StationeServiceContext _context;
        private readonly ILogger<DispensingUnitService> _logger;

        public DispensingUnitService(StationeServiceContext context, ILogger<DispensingUnitService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAsync(DispensingUnit entity)
        {
            await _context.DispensingUnits.AddAsync(entity);
            // save change to the database
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var dispenserUnit = await _context.DispensingUnits.FindAsync(id);
                if (dispenserUnit != null)
                {
                    _context.DispensingUnits.Remove(dispenserUnit);
                    // save chagne to the database
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("DispensingUnit with ID {Id} was successfully deleted.", dispenserUnit.Id);
                }
                else {
                    _logger.LogInformation("DispensingUnit with ID {Id} was not exist.", id);
                }
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "An error occurred while deleting the DispensingUnit with ID {Id}", id);

                // Rethrow the exception if needed
                throw;
            }
        }

        public async Task<IEnumerable<DispensingUnit>> GetAllAsync()
        {
            return await _context.DispensingUnits.ToListAsync();
        }

        public async Task<DispensingUnit> GetAsync(int id)
        {
            return await _context.DispensingUnits.FindAsync(id);
        }

        public async Task UpdateAsync(DispensingUnit entity)
        {
            try
            {
                // Check if the entity exists
                //var existingDispensingUnit = await _context.DispensingUnits
                //    .Include(a => a.FuelQuantities) // Include related FuelQuantities to update
                //    .FirstOrDefaultAsync(a => a.Id == entity.Id);

                var existingDispensingUnit = await _context.DispensingUnits.Include(d => d.Pipes).FirstOrDefaultAsync(d => d.Id == entity.Id);

                if (existingDispensingUnit == null)
                {
                    _logger.LogWarning("DispensingUnit with ID {Id} not found for update.", entity.Id);
                    throw new InvalidOperationException($"DispensingUnit with ID {entity.Id} not found.");
                }

                // Update the entity properties
                existingDispensingUnit = entity;

                // Save changes to the database
                await _context.SaveChangesAsync();

                _logger.LogInformation("DispensingUnit with ID {Id} was successfully updated.", entity.Id);
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "An error occurred while updating the DispensingUnit with ID {Id}", entity.Id);

                // Rethrow the exception if needed
                throw;
            }
        }

    }
}
