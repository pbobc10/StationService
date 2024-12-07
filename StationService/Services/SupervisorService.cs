using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using StationService.Data;
using StationService.Interfaces;
using StationService.Models;


namespace StationService.Services
{
    public class SupervisorService : ISupervisorRepository
    {
        private readonly StationeServiceContext _context;
        private readonly ILogger<SupervisorService> _logger;

        public SupervisorService(StationeServiceContext context, ILogger<SupervisorService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAsync(Supervisor entity)
        {
          await  _context.Supervisors.AddAsync(entity);
            // save change to the database
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id )
        {
            try
            {
                var supervisor = await _context.Supervisors.FindAsync(id);
                if (supervisor != null)
                {
                    _context.Supervisors.Remove(supervisor);
                    // save chagne to the database
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Supervisor with ID {Id} was successfully deleted.", supervisor.Id);

                }
                else
                {
                    _logger.LogInformation("Supervisor with ID {Id} not exist.", id);
                }
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "An error occurred while deleting the Supervisor with ID {Id}", id);

                // Rethrow the exception if needed
                throw;
            }
        }

        public async Task<IEnumerable<Supervisor>> GetAllAsync()
        {
            return await _context.Supervisors.ToListAsync();
        }

        public async Task<Supervisor> GetAsync(int id)
        {
            return await _context.Supervisors.FindAsync(id);
        }

        public async Task UpdateAsync(Supervisor entity)
        {
            try
            {
                // Check if the entity exists
                var existingSupervisors = await _context.Supervisors.FindAsync(entity.Id);

                if (existingSupervisors == null)
                {
                    _logger.LogWarning("Supervisor with ID {Id} not found for update.", entity.Id);
                    throw new InvalidOperationException($"Supervisor with ID {entity.Id} not found.");
                }

                // Update the entity properties
                existingSupervisors = entity;

                // Save changes to the database
                await _context.SaveChangesAsync();

                _logger.LogInformation("Supervisor with ID {Id} was successfully updated.", entity.Id);
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "An error occurred while updating the Supervisor with ID {Id}", entity.Id);

                // Rethrow the exception if needed
                throw;
            }
        }

    }
}
