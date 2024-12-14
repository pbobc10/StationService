using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StationService.Business_Layer.Interfaces;
using StationService.DTOs;
using StationService.Interfaces;
using StationService.Models;
using StationService.Services;

namespace StationService.Controllers
{
    public class GasStationController : Controller
    {
        private readonly ISupervisorFacade _supervisorFacade;
        private readonly IAdministratorFacade _administratorFacade;
        private readonly IGasStationFacade _gasStationFacade;
        private readonly ILogger<GasStationController> _logger;
        private readonly IMapper _mapper;

        public GasStationController(IGasStationFacade gasStationFacade, ILogger<GasStationController> logger,IAdministratorFacade administratorFacade, ISupervisorFacade supervisorFacade, IMapper mapper)
        {
            _gasStationFacade = gasStationFacade;
            _logger = logger;
            _supervisorFacade = supervisorFacade;
            _administratorFacade = administratorFacade;
            _mapper = mapper;

        }

        // GET: GasStationController
        public async Task<IActionResult> Index()
        {
            try
            {
                var gasStations = await _gasStationFacade.GetAllAsync();
                if (gasStations == null)
                {
                    return NotFound();
                }
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
                var gasStation = await _gasStationFacade.GetByIdAsync(id);
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
            var supervisors = await _supervisorFacade.GetAllAsync();
            var administrators = await _administratorFacade.GetAllAsync();

            var viewModel = new GasStationCreateViewModel
            {
                Supervisors = supervisors.Select(s => new SelectListItem
                {
                    Value =  s.Id.ToString(),
                    Text = $"{s.FirstName} {s.FamilyName}"
                }).ToList(),
                Administrators = administrators.Select(a => new SelectListItem
                { 
                    Value= a.Id.ToString(),
                    Text = $"{a.FirstName} {a.FamilyName}"
                }).ToList()
            };

            return View(viewModel);
        }

        // POST: GasStationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GasStationInputDto gasStation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _gasStationFacade.AddAsync(gasStation);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while creating the gasStation.");
                    ModelState.AddModelError("", "An error occurred while creating the gasStation.");
                }
            }

            // If the model state is invalid, reload the dropdowns
            var supervisors = await _supervisorFacade.GetAllAsync();
            var administrators = await _administratorFacade.GetAllAsync();

            var viewModel = new GasStationCreateViewModel
            {
                GasStation = gasStation,
                Supervisors = supervisors.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"{s.FirstName} {s.FamilyName}"
                }).ToList(),
                Administrators = administrators.Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = $"{a.FirstName} {a.FamilyName}"
                }).ToList()
            };

            return View(viewModel);
        }

        // GET: GasStationController/Edit/5
        // GET: GasStationController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            // Charger la station-service existante
            var gasStation = await _gasStationFacade.GetByIdAsync(id);
            if (gasStation == null)
            {
                _logger.LogWarning("GasStation with ID {Id} not found for editing.", id);
                return NotFound();
            }

            // Charger les superviseurs et administrateurs
            var supervisors = await _supervisorFacade.GetAllAsync();
            var administrators = await _administratorFacade.GetAllAsync();

            // Préparer le ViewModel
            var viewModel = new GasStationCreateViewModel
            {
                // Convert GasStationOutputDetailDto to GasStationInputDto using AutoMapper
                GasStation = _mapper.Map<GasStationInputDto>(gasStation),
                Supervisors = supervisors.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"{s.FirstName} {s.FamilyName}",
                    Selected = s.Id == gasStation.SupervisorId // Sélectionne le superviseur existant
                }).ToList(),
                Administrators = administrators.Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = $"{a.FirstName} {a.FamilyName}",
                    Selected = a.Id == gasStation.AdministratorId // Sélectionne l'administrateur existant
                }).ToList()
            };

            return View(viewModel);
        }


        // POST: GasStationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GasStationInputDto gasStation)
        {
            if (id != gasStation.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _gasStationFacade.UpdateAsync(id, gasStation);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating the gasStation with ID {Id}.", id);
                    ModelState.AddModelError("ModelError", "An error occurred while updating the gasStation.");
                }
            }


            // Charger les superviseurs et administrateurs
            var supervisors = await _supervisorFacade.GetAllAsync();
            var administrators = await _administratorFacade.GetAllAsync();

            // Préparer le ViewModel
            var viewModel = new GasStationCreateViewModel
            {
                // Convert GasStationOutputDetailDto to GasStationInputDto using AutoMapper
                GasStation = _mapper.Map<GasStationInputDto>(gasStation),
                Supervisors = supervisors.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"{s.FirstName} {s.FamilyName}",
                    Selected = s.Id == gasStation.SupervisorId // Sélectionne le superviseur existant
                }).ToList(),
                Administrators = administrators.Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = $"{a.FirstName} {a.FamilyName}",
                    Selected = a.Id == gasStation.AdministratorId // Sélectionne l'administrateur existant
                }).ToList()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Appeler la suppression via le facade
                await _gasStationFacade.DeleteAsync(id);
                TempData["SuccessMessage"] = "gasStation deleted successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the gasStation with ID {Id}.", id);
                TempData["ErrorMessage"] = "An error occurred while deleting the gasStation.";
            }

            // Rediriger vers la liste des administrateurs
            return RedirectToAction(nameof(Index));
        }
    }
}
