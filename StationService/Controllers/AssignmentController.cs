using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StationService.Interfaces;
using StationService.Models;

namespace StationService.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly ILogger<AssignmentController> _logger;

        public AssignmentController(IAssignmentRepository administratorRepository, ILogger<AssignmentController> logger)
        {
            _assignmentRepository = administratorRepository;
            _logger = logger;

        }

        // GET: AssignmentController
        public async Task<IActionResult> Index()
        {
            try
            {
                var assignments = await _assignmentRepository.GetAllAsync();
                return View(assignments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all assignments.");
                return View("Error");
            }

        }

        // GET: AssignmentController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var assignment = await _assignmentRepository.GetAsync(id);
                if (assignment == null)
                {
                    return NotFound();
                }
                return View(assignment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the details of assignment with ID {Id}.", id);
                return View("Error");
            }
        }

        // GET: AssignmentController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AssignmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _assignmentRepository.AddAsync(assignment);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while creating the assignment.");
                    ModelState.AddModelError("", "An error occurred while creating the assignment.");
                }
            }
            return View(assignment);
        }

        // GET: AssignmentController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var assignment = await _assignmentRepository.GetAsync(id);
            if (assignment == null)
            {
                _logger.LogWarning("assignment with ID {Id} not found for editing.", id);
                return NotFound();
            }
            return View(assignment);
        }

        // POST: AssignmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Assignment assignment)
        {
            if (id != assignment.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _assignmentRepository.UpdateAsync(assignment);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating the assignment with ID {Id}.", id);
                    ModelState.AddModelError("", "An error occurred while updating the assignment.");
                }
            }
            return View(assignment);
        }

        // GET: AssignmentController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var assignment = await _assignmentRepository.GetAsync(id);
            if (assignment == null)
            {
                _logger.LogWarning("assignment with ID {Id} not found for deletion.", id);
                return NotFound();
            }
            return View(assignment);
        }

        // POST: AssignmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Assignment assignment)
        {
            try
            {
                await _assignmentRepository.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the assignment with ID {Id}.", id);
                ModelState.AddModelError("", "An error occurred while deleting the assignment.");
            }
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}
