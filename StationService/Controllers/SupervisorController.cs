using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StationService.Interfaces;
using StationService.Models;

namespace StationService.Controllers
{
    public class SupervisorController : Controller
    {
        private readonly ISupervisorRepository _supervisorRepository;
        private readonly ILogger<SupervisorController> _logger;

        public SupervisorController(ISupervisorRepository supervisorRepository, ILogger<SupervisorController> logger)
        {
            _supervisorRepository = supervisorRepository;
            _logger = logger;

        }

        // GET: SupervisorController
        public async Task<IActionResult> Index()
        {
            try
            {
                var supervisors = await _supervisorRepository.GetAllAsync();
                return View(supervisors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all supervisors.");
                return View("Error");
            }

        }

        // GET: SupervisorController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var supervisor = await _supervisorRepository.GetAsync(id);
                if (supervisor == null)
                {
                    return NotFound();
                }
                return View(supervisor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the details of supervisor with ID {Id}.", id);
                return View("Error");
            }
        }

        // GET: SupervisorController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SupervisorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Supervisor supervisor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _supervisorRepository.AddAsync(supervisor);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while creating the supervisor.");
                    ModelState.AddModelError("", "An error occurred while creating the supervisor.");
                }
            }
            return View(supervisor);
        }

        // GET: SupervisorController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var supervisor = await _supervisorRepository.GetAsync(id);
            if (supervisor == null)
            {
                _logger.LogWarning("supervisor with ID {Id} not found for editing.", id);
                return NotFound();
            }
            return View(supervisor);
        }

        // POST: SupervisorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Supervisor supervisor)
        {
            if (id != supervisor.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _supervisorRepository.UpdateAsync(supervisor);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating the supervisor with ID {Id}.", id);
                    ModelState.AddModelError("", "An error occurred while updating the supervisor.");
                }
            }
            return View(supervisor);
        }

        // GET: SupervisorController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var supervisor = await _supervisorRepository.GetAsync(id);
            if (supervisor == null)
            {
                _logger.LogWarning("supervisor with ID {Id} not found for deletion.", id);
                return NotFound();
            }
            return View(supervisor);
        }

        // POST: SupervisorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Supervisor supervisor)
        {
            try
            {
                await _supervisorRepository.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the supervisor with ID {Id}.", id);
                ModelState.AddModelError("", "An error occurred while deleting the supervisor.");
            }
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}
