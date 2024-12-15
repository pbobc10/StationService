using Microsoft.AspNetCore.Mvc.Rendering;

namespace StationService.DTOs
{
    public class GasStationAttendantCreateViewModel
    {
        public GasStationAttendantInputDto GasStationAttendant { get; set; }

        public List<SelectListItem> GasStations { get; set; }

        public List<SelectListItem> Shifts { get; set; }

    }
}
