using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StationService.Business_Layer.Interfaces;
using StationService.DTOs;
using StationService.Helpers;
using StationService.Interfaces;
using StationService.Models;

namespace StationService.Controllers
{
    public class GasStationAttendantController : Controller
    {
        private readonly IGasStationAttendantFacade _gasStationAttendantFacade;
        private readonly ILogger<GasStationAttendantController> _logger;
        private readonly IGasStationFacade _gasStationFacade;
        private readonly IMapper _mapper;

        public GasStationAttendantController(IGasStationAttendantFacade gasStationAttendantFacade, IGasStationFacade gasStationFacade, IMapper mapper, ILogger<GasStationAttendantController> logger)
        {
            _gasStationAttendantFacade = gasStationAttendantFacade;
            _gasStationFacade = gasStationFacade;
            _logger = logger;
            _mapper = mapper;

        }

        // GET: GasStationAttendantController
        public async Task<IActionResult> Index()
        {
            try
            {
                var gasStationAttendants = await _gasStationAttendantFacade.GetAllAsync();
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
                var gasStationAttendant = await _gasStationAttendantFacade.GetByIdAsync(id);
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
        public async Task<IActionResult> Create()
        {
            // Load the gas Stations
            var gasStations = await _gasStationFacade.GetAllAsync();


            var viewModel = new GasStationAttendantCreateViewModel
            {
                GasStations = gasStations.Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = $"{g.Name}"

                }).ToList(),
                //Populate the shift dropdown
                Shifts = EnumHelper.GetEnumSelectList<ShiftType>()
            };

            return View(viewModel);
        }

        // POST: GasStationAttendantController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GasStationAttendantInputDto gasStationAttendant)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await _gasStationAttendantFacade.AddAsync(gasStationAttendant);
                    TempData["SuccessMessage"] = "gasStationAttendant created successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while creating the gasStationAttendant.");
                    ModelState.AddModelError("", "An error occurred while creating the gasStationAttendant.");
                    TempData["ErrorMessage"] = "An error occurred while creating the gasStationAttendant.";
                }
            }

            // If the model state is invalid, reload the dropdowns
            var gasStations = await _gasStationFacade.GetAllAsync();

            var viewModel = new GasStationAttendantCreateViewModel
            {
                GasStationAttendant = gasStationAttendant,
                GasStations = gasStations.Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = $"{g.Name}"

                }).ToList(),
                //Populate the shift dropdown
                Shifts = EnumHelper.GetEnumSelectList<ShiftType>()
            };

            return View(viewModel);
        }

        // GET: GasStationAttendantController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var gasStationAttendant = await _gasStationAttendantFacade.GetByIdAsync(id);
            if (gasStationAttendant == null)
            {
                _logger.LogWarning("gasStationAttendant with ID {Id} not found for editing.", id);
                return NotFound();
            }

            // Load the Gas Stations
            var gasStations = await _gasStationFacade.GetAllAsync();

            var viewModel = new GasStationAttendantCreateViewModel
            {
                GasStationAttendant = _mapper.Map<GasStationAttendantInputDto>(gasStationAttendant),
                GasStations = gasStations.Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = $"{g.Name}"

                }).ToList(),
                //Populate the shift dropdown
                Shifts = EnumHelper.GetEnumSelectList<ShiftType>()
            };

            return View(viewModel);
        }

        // POST: GasStationAttendantController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GasStationAttendantInputDto gasStationAttendant)
        {
            if (id != gasStationAttendant.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _gasStationAttendantFacade.UpdateAsync(id, gasStationAttendant);
                    TempData["SuccessMessage"] = "gasStationAttendant edited successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating the gasStationAttendant with ID {Id}.", id);
                    ModelState.AddModelError("", "An error occurred while updating the gasStationAttendant.");
                    TempData["ErrorMessage"] = "An error occurred while updating the gasStationAttendant.";
                }
            }

            // If the model state is invalid, reload the dropdowns
            var gasStations = await _gasStationFacade.GetAllAsync();

            var viewModel = new GasStationAttendantCreateViewModel
            {
                GasStationAttendant = _mapper.Map<GasStationAttendantInputDto>(gasStationAttendant),
                GasStations = gasStations.Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = $"{g.Name}"

                }).ToList(),
                //Populate the shift dropdown
                Shifts = EnumHelper.GetEnumSelectList<ShiftType>()
            };

            return View(viewModel);
        }


        // POST: GasStationAttendantController/Delete/5
        public async Task<IActionResult> Delete(int id, GasStationAttendantOutputDto gasStationAttendant)
        {
            try
            {
                if (id == gasStationAttendant.Id)
                {
                    await _gasStationAttendantFacade.DeleteAsync(id);
                    TempData["SuccessMessage"] = "gasStationAttendant Deleted successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = "gasStationAttendant id not exist ";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the gasStationAttendant with ID {Id}.", id);
                TempData["ErrorMessage"] = "An error occurred while deleting the gasStationAttendant.";
            }

            // Rediriger vers la liste des gasStationAttendants
            return RedirectToAction(nameof(Index));
        }
    }
}
