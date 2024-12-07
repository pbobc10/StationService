using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StationService.Interfaces;
using StationService.Models;

namespace StationService.Controllers
{
    public class GasMeterController : Controller
    {
        private readonly IGasMeterRepository _gasMeterRepository;
        private readonly ILogger<GasMeterController> _logger;

        public GasMeterController(IGasMeterRepository gasMeterRepository, ILogger<GasMeterController> logger)
        {
            _gasMeterRepository = gasMeterRepository;
            _logger = logger;

        }

        // GET: GasMeterController
        public async Task<IActionResult> Index()
        {
            try
            {
                var gasMeters = await _gasMeterRepository.GetAllAsync();
                return View(gasMeters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all gasMeters.");
                return View("Error");
            }

        }

        // GET: GasMeterController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var gasMeter = await _gasMeterRepository.GetAsync(id);
                if (gasMeter == null)
                {
                    return NotFound();
                }
                return View(gasMeter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the details of gasMeter with ID {Id}.", id);
                return View("Error");
            }
        }

        // GET: GasMeterController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GasMeterController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GasMeter gasMeter)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _gasMeterRepository.AddAsync(gasMeter);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while creating the gasMeter.");
                    ModelState.AddModelError("", "An error occurred while creating the gasMeter.");
                }
            }
            return View(gasMeter);
        }

        // GET: GasMeterController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var gasMeter = await _gasMeterRepository.GetAsync(id);
            if (gasMeter == null)
            {
                _logger.LogWarning("gasMeter with ID {Id} not found for editing.", id);
                return NotFound();
            }
            return View(gasMeter);
        }

        // POST: GasMeterController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GasMeter gasMeter)
        {
            if (id != gasMeter.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _gasMeterRepository.UpdateAsync(gasMeter);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating the gasMeter with ID {Id}.", id);
                    ModelState.AddModelError("", "An error occurred while updating the gasMeter.");
                }
            }
            return View(gasMeter);
        }

        // GET: GasMeterController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var gasMeter = await _gasMeterRepository.GetAsync(id);
            if (gasMeter == null)
            {
                _logger.LogWarning("gasMeter with ID {Id} not found for deletion.", id);
                return NotFound();
            }
            return View(gasMeter);
        }

        // POST: GasMeterController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, GasMeter gasMeter)
        {
            try
            {
                await _gasMeterRepository.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the gasMeter with ID {Id}.", id);
                ModelState.AddModelError("", "An error occurred while deleting the gasMeter.");
            }
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}
