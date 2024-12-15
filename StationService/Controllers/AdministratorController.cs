using Microsoft.AspNetCore.Mvc;
using StationService.Business_Layer.Interfaces;
using StationService.DTOs;
using StationService.Models;

namespace StationService.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly IAdministratorFacade _administratorFacade;
        private readonly ILogger<AdministratorController> _logger;

        public AdministratorController(IAdministratorFacade administratorFacade, ILogger<AdministratorController> logger)
        {
            _administratorFacade = administratorFacade;
            _logger = logger;

        }

        // GET: AdministratorController
        public async Task<IActionResult> Index()
        {
            try
            {
                var administrators = await _administratorFacade.GetAllAsync();
                if (administrators == null)
                {
                    return NotFound();
                }
                return View(administrators);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all administrators.");
                return View("Error");
            }

        }

        // GET: AdministratorController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var administrator = await _administratorFacade.GetByIdAsync(id);
                if (administrator == null)
                {
                    return NotFound();
                }
                return View(administrator);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the details of administrator with ID {Id}.", id);
                return View("Error");
            }
        }

        // GET: AdministratorController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdministratorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdministratorInputDto administrator)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _administratorFacade.AddAsync(administrator);
                    TempData["SuccessMessage"] = "Administrator created successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while creating the administrator.");
                    ModelState.AddModelError("", "An error occurred while creating the administrator.");
                    TempData["ErrorMessage"] = "An error occurred while creating the administrator.";
                }
            }
            return View(administrator);
        }

        // GET: AdministratorController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Action = "edit";

            var administrator = await _administratorFacade.GetByIdAsync(id);
            if (administrator == null)
            {
                _logger.LogWarning("Administrator with ID {Id} not found for editing.", id);
                return NotFound();
            }
            return View(administrator);
        }

        // POST: AdministratorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AdministratorInputDto administrator)
        {
            if (id != administrator.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _administratorFacade.UpdateAsync(id, administrator);
                    TempData["SuccessMessage"] = "Administrator edited successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating the administrator with ID {Id}.", id);
                    ModelState.AddModelError("ModelError", "An error occurred while updating the administrator.");
                    TempData["ErrorMessage"] = "An error occurred while updating the administrator.";
                }
            }
            return View(administrator);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Appeler la suppression via le facade
                await _administratorFacade.DeleteAsync(id);
                TempData["SuccessMessage"] = "Administrator deleted successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the administrator with ID {Id}.", id);
                TempData["ErrorMessage"] = "An error occurred while deleting the administrator.";
            }

            // Rediriger vers la liste des administrateurs
            return RedirectToAction(nameof(Index));
        }

    }
}
