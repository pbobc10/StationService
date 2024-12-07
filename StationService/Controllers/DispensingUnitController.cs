using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StationService.Interfaces;
using StationService.Models;

namespace StationService.Controllers
{
    public class DispensingUnitController : Controller
    {
        private readonly IDispensingUnitRepository _dispensingUnitRepository;
        private readonly ILogger<DispensingUnitController> _logger;

        public DispensingUnitController(IDispensingUnitRepository dispensingUnitRepository, ILogger<DispensingUnitController> logger)
        {
            _dispensingUnitRepository = dispensingUnitRepository;
            _logger = logger;

        }

        // GET: DispensingUnitController
        public async Task<IActionResult> Index()
        {
            try
            {
                var dispensingUnits = await _dispensingUnitRepository.GetAllAsync();
                return View(dispensingUnits);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all dispensingUnits.");
                return View("Error");
            }

        }

        // GET: DispensingUnitController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var dispensingUnit = await _dispensingUnitRepository.GetAsync(id);
                if (dispensingUnit == null)
                {
                    return NotFound();
                }
                return View(dispensingUnit);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the details of DispensingUnit with ID {Id}.", id);
                return View("Error");
            }
        }

        // GET: DispensingUnitController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DispensingUnitController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DispensingUnit dispensingUnit)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _dispensingUnitRepository.AddAsync(dispensingUnit);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while creating the DispensingUnit.");
                    ModelState.AddModelError("", "An error occurred while creating the DispensingUnit.");
                }
            }
            return View(dispensingUnit);
        }

        // GET: DispensingUnitController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var dispensingUnit = await _dispensingUnitRepository.GetAsync(id);
            if (dispensingUnit == null)
            {
                _logger.LogWarning("DispensingUnit with ID {Id} not found for editing.", id);
                return NotFound();
            }
            return View(dispensingUnit);
        }

        // POST: DispensingUnitController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DispensingUnit dispensingUnit)
        {
            if (id != dispensingUnit.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _dispensingUnitRepository.UpdateAsync(dispensingUnit);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating the DispensingUnit with ID {Id}.", id);
                    ModelState.AddModelError("", "An error occurred while updating the DispensingUnit.");
                }
            }
            return View(dispensingUnit);
        }

        // GET: DispensingUnitController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var dispensingUnit = await _dispensingUnitRepository.GetAsync(id);
            if (dispensingUnit == null)
            {
                _logger.LogWarning("DispensingUnit with ID {Id} not found for deletion.", id);
                return NotFound();
            }
            return View(dispensingUnit);
        }

        // POST: DispensingUnitController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DispensingUnit dispensingUnit)
        {
            try
            {
                await _dispensingUnitRepository.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the DispensingUnit with ID {Id}.", id);
                ModelState.AddModelError("", "An error occurred while deleting the DispensingUnit.");
            }
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}
