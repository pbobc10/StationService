using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StationService.Business_Layer.Interfaces;
using StationService.DTOs;
using StationService.Interfaces;
using StationService.Models;

namespace StationService.Controllers
{
    public class SupervisorController : Controller
    {
        private readonly ISupervisorFacade _supervisorFacade;
        private readonly ILogger<SupervisorController> _logger;
        private readonly IGasStationFacade _gasStationFacade;
        private readonly IMapper _mapper;

        public SupervisorController(ISupervisorFacade supervisorFacade, IGasStationFacade gasStationFacade, IMapper mapper, ILogger<SupervisorController> logger)
        {
            _supervisorFacade = supervisorFacade;
            _gasStationFacade = gasStationFacade;
            _logger = logger;
            _mapper = mapper;

        }

        // GET: SupervisorController
        public async Task<IActionResult> Index()
        {
            try
            {
                var supervisors = await _supervisorFacade.GetAllAsync();
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
                var supervisor = await _supervisorFacade.GetByIdAsync(id);
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
        public async Task<IActionResult> Create()
        {
            // Load the gas Stations
            var gasStations = await _gasStationFacade.GetAllAsync();

            var viewModel = new SupervisorCreateViewModel
            {
                GasStations = gasStations.Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = $"{g.Name}"

                }).ToList()
            };

            return View(viewModel);
        }

        // POST: SupervisorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupervisorInputDto supervisor)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await _supervisorFacade.AddAsync(supervisor);
                    TempData["SuccessMessage"] = "Supervisor created successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while creating the supervisor.");
                    ModelState.AddModelError("", "An error occurred while creating the supervisor.");
                    TempData["ErrorMessage"] = "An error occurred while creating the supervisor.";
                }
            }

            // If the model state is invalid, reload the dropdowns
            var gasStations = await _gasStationFacade.GetAllAsync();

            var viewModel = new SupervisorCreateViewModel
            {
                Supervisor = supervisor,
                GasStations = gasStations.Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = $"{g.Name}"

                }).ToList()
            };

            return View(viewModel);
        }

        // GET: SupervisorController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var supervisor = await _supervisorFacade.GetByIdAsync(id);
            if (supervisor == null)
            {
                _logger.LogWarning("supervisor with ID {Id} not found for editing.", id);
                return NotFound();
            }

            // Load the Gas Stations
            var gasStations = await _gasStationFacade.GetAllAsync();

            var viewModel = new SupervisorCreateViewModel
            {
                Supervisor = _mapper.Map<SupervisorInputDto>(supervisor),
                GasStations = gasStations.Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = $"{g.Name}"

                }).ToList()
            };

            return View(viewModel);
        }

        // POST: SupervisorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SupervisorInputDto supervisor)
        {
            if (id != supervisor.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _supervisorFacade.UpdateAsync(id, supervisor);
                    TempData["SuccessMessage"] = "Supervisor edited successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating the supervisor with ID {Id}.", id);
                    ModelState.AddModelError("", "An error occurred while updating the supervisor.");
                    TempData["ErrorMessage"] = "An error occurred while updating the supervisor.";
                }
            }

            // If the model state is invalid, reload the dropdowns
            var gasStations = await _gasStationFacade.GetAllAsync();

            var viewModel = new SupervisorCreateViewModel
            {
                Supervisor = _mapper.Map<SupervisorInputDto>(supervisor),
                GasStations = gasStations.Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = $"{g.Name}"

                }).ToList()
            };

            return View(viewModel);
        }


        // POST: SupervisorController/Delete/5
        public async Task<IActionResult> Delete(int id,SupervisorOutputDto supervisor)
        {
            try
            {
                if (id == supervisor.Id)
                {
                    await _supervisorFacade.DeleteAsync(id);
                    TempData["SuccessMessage"] = "Supervisor Deleted successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Supervisor id not exist ";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the supervisor with ID {Id}.", id);
                TempData["ErrorMessage"] = "An error occurred while deleting the supervisor.";
            }

            // Rediriger vers la liste des supervisors
            return RedirectToAction(nameof(Index));
        }
    }
}
