using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StationService.Interfaces;
using StationService.Models;

namespace StationService.Controllers
{
    public class GasStationController : Controller
    {
        private readonly IGasStationRepository _gasStationRepository;
        private readonly IAdministratorRepository _administratorRepository;
        private readonly ILogger<GasStationController> _logger;

        public GasStationController(IGasStationRepository gasStationRepository, IAdministratorRepository administratorRepository, ILogger<GasStationController> logger)
        {
            _gasStationRepository = gasStationRepository;
            _administratorRepository = administratorRepository;
            _logger = logger;

        }

        // GET: GasStationController
        public async Task<IActionResult> Index()
        {
            try
            {
                var gasStations = await _gasStationRepository.GetAllAsync();
                return View(gasStations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all gasStations.");
                return View("Error");
            }

        }

        // GET: GasStationController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var gasStation = await _gasStationRepository.GetAsync(id);
                if (gasStation == null)
                {
                    return NotFound();
                }
                return View(gasStation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the details of gasStation with ID {Id}.", id);
                return View("Error");
            }
        }

        // GET: GasStationController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Administrator = await _administratorRepository.GetAllAsync();
            return View();
        }

        // POST: GasStationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GasStation gasStation)
        {
            //var admin = await _administratorRepository.GetAsync(gasStation.AdministratorId);
            //gasStation.Administrator = admin;
            //  ModelState.Remove("Supervisor");
            //ModelState.Remove("Administrator");

            if (ModelState.IsValid)
            {
                try
                {
                    await _gasStationRepository.AddAsync(gasStation);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while creating the gasStation.");
                    ModelState.AddModelError("", "An error occurred while creating the gasStation.");
                }
            }
            ViewBag.Administrator = (await  _administratorRepository.GetAllAsync());
            return View(gasStation);
        }

        // GET: GasStationController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var gasStation = await _gasStationRepository.GetAsync(id);
            if (gasStation == null)
            {
                _logger.LogWarning("gasStation with ID {Id} not found for editing.", id);
                return NotFound();
            }
            return View(gasStation);
        }

        // POST: GasStationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GasStation gasStation)
        {
            if (id != gasStation.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _gasStationRepository.UpdateAsync(gasStation);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating the gasStation with ID {Id}.", id);
                    ModelState.AddModelError("", "An error occurred while updating the gasStation.");
                }
            }
            return View(gasStation);
        }

        // GET: GasStationController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var gasStation = await _gasStationRepository.GetAsync(id);
            if (gasStation == null)
            {
                _logger.LogWarning("gasStation with ID {Id} not found for deletion.", id);
                return NotFound();
            }
            return View(gasStation);
        }

        // POST: GasStationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, GasStation gasStation)
        {
            try
            {
                await _gasStationRepository.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the gasStation with ID {Id}.", id);
                ModelState.AddModelError("", "An error occurred while deleting the gasStation.");
            }
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}
