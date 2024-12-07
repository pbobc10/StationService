using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StationService.Interfaces;
using StationService.Models;

namespace StationService.Controllers
{
    public class FuelQuantityController : Controller
    {
        private readonly IFuelQuantityRepository _fuelQuantityRepository;
        private readonly ILogger<FuelQuantityController> _logger;

        public FuelQuantityController(IFuelQuantityRepository fuelQuantityRepository, ILogger<FuelQuantityController> logger)
        {
            _fuelQuantityRepository = fuelQuantityRepository;
            _logger = logger;

        }

        // GET: FuelQuantityController
        public async Task<IActionResult> Index()
        {
            try
            {
                var fuelQuantities = await _fuelQuantityRepository.GetAllAsync();
                return View(fuelQuantities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all fuelQuantities.");
                return View("Error");
            }

        }

        // GET: FuelQuantityController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var fuelQuantity = await _fuelQuantityRepository.GetAsync(id);
                if (fuelQuantity == null)
                {
                    return NotFound();
                }
                return View(fuelQuantity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the details of fuelQuantity with ID {Id}.", id);
                return View("Error");
            }
        }

        // GET: FuelQuantityController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FuelQuantityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FuelQuantity fuelQuantity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _fuelQuantityRepository.AddAsync(fuelQuantity);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while creating the fuelQuantity.");
                    ModelState.AddModelError("", "An error occurred while creating the fuelQuantity.");
                }
            }
            return View(fuelQuantity);
        }

        // GET: FuelQuantityController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var fuelQuantity = await _fuelQuantityRepository.GetAsync(id);
            if (fuelQuantity == null)
            {
                _logger.LogWarning("fuelQuantity with ID {Id} not found for editing.", id);
                return NotFound();
            }
            return View(fuelQuantity);
        }

        // POST: FuelQuantityController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FuelQuantity fuelQuantity)
        {
            if (id != fuelQuantity.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _fuelQuantityRepository.UpdateAsync(fuelQuantity);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating the fuelQuantity with ID {Id}.", id);
                    ModelState.AddModelError("", "An error occurred while updating the fuelQuantity.");
                }
            }
            return View(fuelQuantity);
        }

        // GET: FuelQuantityController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var fuelQuantity = await _fuelQuantityRepository.GetAsync(id);
            if (fuelQuantity == null)
            {
                _logger.LogWarning("fuelQuantity with ID {Id} not found for deletion.", id);
                return NotFound();
            }
            return View(fuelQuantity);
        }

        // POST: FuelQuantityController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, FuelQuantity fuelQuantity)
        {
            try
            {
                await _fuelQuantityRepository.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the fuelQuantity with ID {Id}.", id);
                ModelState.AddModelError("", "An error occurred while deleting the fuelQuantity.");
            }
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}
