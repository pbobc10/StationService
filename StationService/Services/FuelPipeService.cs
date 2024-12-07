using Microsoft.EntityFrameworkCore;
using StationService.Data;
using StationService.Interfaces;
using StationService.Models;


namespace StationService.Services
{
    public class FuelPipeService : IFuelPipeRepository
    {
        private readonly StationeServiceContext _context;
        private readonly ILogger<FuelPipeService> _logger;

        public FuelPipeService(StationeServiceContext context, ILogger<FuelPipeService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAsync(FuelPipe entity)
        {
            await _context.FuelPipes.AddAsync(entity);
            // save change to the database
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var fuelPipe = await _context.FuelPipes.FindAsync(id);
                if (fuelPipe != null)
                {
                    _context.FuelPipes.Remove(fuelPipe);
                    // save chagne to the database
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("FuelPipe with ID {Id} was successfully deleted.", fuelPipe.Id);

                }
                else
                {
                    _logger.LogInformation("FuelPipe with ID {Id} was not exist.", fuelPipe.Id);
                }
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "An error occurred while deleting the FuelPipe with ID {Id}", id);

                // Rethrow the exception if needed
                throw;
            }
        }

        public async Task<IEnumerable<FuelPipe>> GetAllAsync()
        {
            return await _context.FuelPipes.ToListAsync();
        }

        public async Task<FuelPipe> GetAsync(int id)
        {
            return await _context.FuelPipes.FindAsync(id);
        }

        public async Task UpdateAsync(FuelPipe entity)
        {
            try
            {
                // Check if the entity exists
                //var existingFuelPipe = await _context.FuelPipes
                //    .Include(a => a.FuelQuantities) // Include related FuelQuantities to update
                //    .FirstOrDefaultAsync(a => a.Id == entity.Id);

                var existingFuelPipe = await _context.FuelPipes.Include(d => d.Meter).FirstOrDefaultAsync(d => d.Id == entity.Id);

                if (existingFuelPipe == null)
                {
                    _logger.LogWarning("FuelPipe with ID {Id} not found for update.", entity.Id);
                    throw new InvalidOperationException($"FuelPipe with ID {entity.Id} not found.");
                }

                // Update the entity properties
                existingFuelPipe = entity;

                // Save changes to the database
                await _context.SaveChangesAsync();

                _logger.LogInformation("FuelPipe with ID {Id} was successfully updated.", entity.Id);
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "An error occurred while updating the FuelPipe with ID {Id}", entity.Id);

                // Rethrow the exception if needed
                throw;
            }
        }

    }
}
