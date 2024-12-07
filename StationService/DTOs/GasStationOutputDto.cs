namespace StationService.DTOs
{
    public class GasStationOutputDto
    {
        public string Name { get; set; }
        public string Location { get; set; }
        //public string SupervisorID { get; set; }
        public string SupervisorFirstName { get; set; }
        public string SupervisorFamilyName { get; set; }
        //public string AdministratorID { get; set; }
        public string AdministratorFirstName { get; set; }
        public string AdministratorFamilyName { get; set; }

        //public ICollection<GasStationAttendantOutputDto> GasStationAttendants { get; set; }
        //public ICollection<DispensingUnitOutputDto> DispensingUnits { get; set; }
    }
}
