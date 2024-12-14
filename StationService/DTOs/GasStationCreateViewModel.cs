using Microsoft.AspNetCore.Mvc.Rendering;

namespace StationService.DTOs
{
    public class GasStationCreateViewModel
    {
        public GasStationInputDto GasStation { get; set; }

        public List<SelectListItem> Supervisors { get; set; }

        public List<SelectListItem> Administrators { get; set; }
    }
}
