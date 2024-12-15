using Microsoft.AspNetCore.Mvc.Rendering;

namespace StationService.DTOs
{
    public class SupervisorCreateViewModel
    {
        public SupervisorInputDto Supervisor { get; set; }

        public List<SelectListItem> GasStations { get; set; }

    }
}
