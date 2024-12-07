using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StationService.Interfaces;
using StationService.Models;

namespace StationService.Controllers
{
    public class FuelPipeController : Controller
    {
        private readonly IFuelPipeRepository _fuelPipeRepository;
        private readonly ILogger<FuelPipeController> _logger;

        public FuelPipeController(IFuelPipeRepository fuelPipeController, ILogger<FuelPipeController> logger)
        {
            _fuelPipeRepository = fuelPipeController;
            _logger = logger;

        }

        // GET: FuelPipeController
        public async Task<IActionResult> Index()
        {
            try
            {
                var fuelPipes = await _fuelPipeRepository.GetAllAsync();
                return View(fuelPipes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all fuelPipes.");
                return View("Error");
            }

        }

        // GET: FuelPipeController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var fuelPipe = await _fuelPipeRepository.GetAsync(id);
                if (fuelPipe == null)
                {
                    return NotFound();
                }
                return View(fuelPipe);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the details of fuelPipe with ID {Id}.", id);
                return View("Error");
            }
        }

        // GET: FuelPipeController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FuelPipeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FuelPipe fuelPipe)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _fuelPipeRepository.AddAsync(fuelPipe);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while creating the fuelPipe.");
                    ModelState.AddModelError("", "An error occurred while creating the fuelPipe.");
                }
            }
            return View(fuelPipe);
        }

        // GET: FuelPipeController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var fuelPipe = await _fuelPipeRepository.GetAsync(id);
            if (fuelPipe == null)
            {
                _logger.LogWarning("fuelPipe with ID {Id} not found for editing.", id);
                return NotFound();
            }
            return View(fuelPipe);
        }

        // POST: FuelPipeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FuelPipe fuelPipe)
        {
            if (id != fuelPipe.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _fuelPipeRepository.UpdateAsync(fuelPipe);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating the fuelPipe with ID {Id}.", id);
                    ModelState.AddModelError("", "An error occurred while updating the fuelPipe.");
                }
            }
            return View(fuelPipe);
        }

        // GET: FuelPipeController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var fuelPipe = await _fuelPipeRepository.GetAsync(id);
            if (fuelPipe == null)
            {
                _logger.LogWarning("fuelPipe with ID {Id} not found for deletion.", id);
                return NotFound();
            }
            return View(fuelPipe);
        }

        // POST: FuelPipeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, FuelPipe fuelPipe)
        {
            try
            {
                await _fuelPipeRepository.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the fuelPipe with ID {Id}.", id);
                ModelState.AddModelError("", "An error occurred while deleting the fuelPipe.");
            }
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}
