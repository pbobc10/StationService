using Microsoft.EntityFrameworkCore;
using StationService.Data;
using StationService.Interfaces;
using StationService.Models;

namespace StationService.Services
{
    public class AssignmentService : IAssignmentRepository
    {
        private readonly StationeServiceContext _context;
        private readonly ILogger<AssignmentService> _logger;

        public AssignmentService(StationeServiceContext context, ILogger<AssignmentService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAsync(Assignment entity)
        {
            await _context.Assignments.AddAsync(entity);
            // save change to the database
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var assignment = await _context.Assignments.FindAsync(id);
                if (assignment != null)
                {
                    await _context.SaveChangesAsync();
                    _context.Assignments.Remove(assignment);
                    // save chagne to the database
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Assignment with ID {Id} was successfully deleted.", assignment.Id);

                }
                else
                {
                    _logger.LogInformation("Assignment with ID {Id} not exist.", id);
                }
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "An error occurred while deleting the Assignment with ID {Id}", id);

                // Rethrow the exception if needed
                throw;
            }
        }

        public async Task<IEnumerable<Assignment>> GetAllAsync()
        {
            return await _context.Assignments.ToListAsync();
        }

        public async Task<Assignment> GetAsync(int id)
        {
            return await _context.Assignments.FindAsync(id);
        }

        public async Task UpdateAsync(Assignment entity)
        {
            try
            {
                // Check if the entity exists
                var existingAssignment = await _context.Assignments
                    .Include(a => a.FuelQuantities) // Include related FuelQuantities to update
                    .FirstOrDefaultAsync(a => a.Id == entity.Id);

                if (existingAssignment == null)
                {
                    _logger.LogWarning("Assignment with ID {Id} not found for update.", entity.Id);
                    throw new InvalidOperationException($"Assignment with ID {entity.Id} not found.");
                }

                // Update the entity properties
                existingAssignment = entity;
                _context.Entry(existingAssignment).State = EntityState.Modified;

                // Save changes to the database
                await _context.SaveChangesAsync();

                _logger.LogInformation("Assignment with ID {Id} was successfully updated.", entity.Id);
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "An error occurred while updating the Assignment with ID {Id}", entity.Id);

                // Rethrow the exception if needed
                throw;
            }
        }

    }
}
