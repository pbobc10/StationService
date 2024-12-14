namespace StationService.DTOs
{
    public class GasStationOutputDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string SupervisorName { get; set; } // Nom complet du superviseur

        public string AdministratorName { get; set; } // Nom complet de l'administrateur

        public int DispensingUnitCount { get; set; }

        public int GasStationAttendantCount { get; set; }
    }
}
