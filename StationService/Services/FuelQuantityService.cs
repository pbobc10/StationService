using Microsoft.EntityFrameworkCore;
using StationService.Data;
using StationService.Interfaces;
using StationService.Models;


namespace StationService.Services
{
    public class FuelQuantityService : IFuelQuantityRepository
    {
        private readonly StationeServiceContext _context;
        private readonly ILogger<FuelQuantityService> _logger;

        public FuelQuantityService(StationeServiceContext context, ILogger<FuelQuantityService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAsync(FuelQuantity entity)
        {
            await _context.FuelQuantities.AddAsync(entity);
            // save change to the database
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var fuelQuantity = await _context.FuelQuantities.FindAsync(id);
                if (fuelQuantity != null)
                {
                    _context.FuelQuantities.Remove(fuelQuantity);
                    // save chagne to the database
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("FuelQuantity with ID {Id} was successfully deleted.", fuelQuantity.Id);

                }
                else {
                    _logger.LogInformation("FuelQuantity with ID {Id} was not exist.", id);
                }
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "An error occurred while deleting the FuelQuantity with ID {Id}", id);

                // Rethrow the exception if needed
                throw;
            }
        }

        public async Task<IEnumerable<FuelQuantity>> GetAllAsync()
        {
            return await _context.FuelQuantities.ToListAsync();
        }

        public async Task<FuelQuantity> GetAsync(int id)
        {
            return await _context.FuelQuantities.FindAsync(id);
        }

        public async Task UpdateAsync(FuelQuantity entity)
        {
            try
            {
                // Check if the entity exists
                var existingFuelQuantity = await _context.FuelQuantities.FindAsync(entity.Id);

                if (existingFuelQuantity == null)
                {
                    _logger.LogWarning("FuelQuantity with ID {Id} not found for update.", entity.Id);
                    throw new InvalidOperationException($"FuelQuantity with ID {entity.Id} not found.");
                }

                // Update the entity properties
                existingFuelQuantity = entity;

                // Save changes to the database
                await _context.SaveChangesAsync();

                _logger.LogInformation("FuelQuantity with ID {Id} was successfully updated.", entity.Id);
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "An error occurred while updating the FuelQuantity with ID {Id}", entity.Id);

                // Rethrow the exception if needed
                throw;
            }
        }

    }
}
