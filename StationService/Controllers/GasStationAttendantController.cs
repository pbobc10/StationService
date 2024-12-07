using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StationService.Interfaces;
using StationService.Models;

namespace StationService.Controllers
{
    public class GasStationAttendantController : Controller
    {
        private readonly IGasStationAttendantRepository _gasStationAttendantRepository;
        private readonly ILogger<GasStationAttendantController> _logger;

        public GasStationAttendantController(IGasStationAttendantRepository gasStationAttendantRepository, ILogger<GasStationAttendantController> logger)
        {
            _gasStationAttendantRepository = gasStationAttendantRepository;
            _logger = logger;

        }

        // GET: GasStationAttendantController
        public async Task<IActionResult> Index()
        {
            try
            {
                var gasStationAttendants = await _gasStationAttendantRepository.GetAllAsync();
                return View(gasStationAttendants);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all gasStationAttendants.");
                return View("Error");
            }

        }

        // GET: GasStationAttendantController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var gasStationAttendant = await _gasStationAttendantRepository.GetAsync(id);
                if (gasStationAttendant == null)
                {
                    return NotFound();
                }
                return View(gasStationAttendant);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the details of gasStationAttendant with ID {Id}.", id);
                return View("Error");
            }
        }

        // GET: GasStationAttendantController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GasStationAttendantController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GasStationAttendant gasStationAttendant)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _gasStationAttendantRepository.AddAsync(gasStationAttendant);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while creating the gasStationAttendant.");
                    ModelState.AddModelError("", "An error occurred while creating the gasStationAttendant.");
                }
            }
            return View(gasStationAttendant);
        }

        // GET: GasStationAttendantController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var gasStationAttendant = await _gasStationAttendantRepository.GetAsync(id);
            if (gasStationAttendant == null)
            {
                _logger.LogWarning("gasStationAttendant with ID {Id} not found for editing.", id);
                return NotFound();
            }
            return View(gasStationAttendant);
        }

        // POST: GasStationAttendantController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GasStationAttendant gasStationAttendant)
        {
            if (id != gasStationAttendant.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _gasStationAttendantRepository.UpdateAsync(gasStationAttendant);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating the gasStationAttendant with ID {Id}.", id);
                    ModelState.AddModelError("", "An error occurred while updating the gasStationAttendant.");
                }
            }
            return View(gasStationAttendant);
        }

        // GET: GasStationAttendantController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var gasStationAttendant = await _gasStationAttendantRepository.GetAsync(id);
            if (gasStationAttendant == null)
            {
                _logger.LogWarning("gasStationAttendant with ID {Id} not found for deletion.", id);
                return NotFound();
            }
            return View(gasStationAttendant);
        }

        // POST: GasStationAttendantController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, GasStationAttendant gasStationAttendant)
        {
            try
            {
                await _gasStationAttendantRepository.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the gasStationAttendant with ID {Id}.", id);
                ModelState.AddModelError("", "An error occurred while deleting the gasStationAttendant.");
            }
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}
