using Microsoft.AspNetCore.Mvc.Rendering;

namespace StationService.DTOs
{
    public class GasStationEditViewModel
    {
        public GasStationOutputDetailDto GasStation { get; set; }

        public List<SelectListItem> Supervisors { get; set; }

        public List<SelectListItem> Administrators { get; set; }
    }
}
