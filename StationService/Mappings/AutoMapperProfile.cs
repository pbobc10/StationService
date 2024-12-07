using AutoMapper;
using StationService.DTOs;
using StationService.Models;

namespace StationService.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            // Administrator mappings
            CreateMap<AdministratorInputDto, Administrator>();
            CreateMap<Administrator, AdministratorOutputDetailDto>();
            CreateMap<Administrator, AdministratorOutputDto>();

            // Dispensing mappings
            CreateMap<DispensingUnitInputDto, DispensingUnit>();
            CreateMap<DispensingUnit, DispensingUnitOutputDto>()
                .ForMember(dest => dest.GasStationName, opt => opt.MapFrom(src => src.GasStation.Name));

            // FuelPipe mappings
            CreateMap<FuelPipeInputDto, FuelPipe>();
            CreateMap<FuelPipe, FuelPipeOutputDto>()
                .ForMember(dest => dest.DispensingUnitName, opt => opt.MapFrom(src => src.DispensingUnit.UnitNumber))
                .ForMember(dest => dest.Meter, opt => opt.MapFrom(src => src.Meter));

            // GasMeter mappings
            CreateMap<GasMeterInputDto, GasMeter>();
            CreateMap<GasMeter, GasMeterOutputDto>()
                .ForMember(dest => dest.FuelPipeType, opt => opt.MapFrom(src => src.FuelPipe.Type.ToString()));

            // GasStationAttendant mappings
            CreateMap<GasStationAttendantInputDto, GasStationAttendant>();
            CreateMap<GasStationAttendant, GasStationAttendantOutputDto>().ForMember(dest => dest.GasStationName, opt => opt.MapFrom(src => src.GasStation.Name));

            // GasStation mappings
            CreateMap<GasMeterInputDto, GasStation>();
            CreateMap<GasStation, GasStationOutputDto>().ForMember(dest => dest.SupervisorFamilyName, opt => opt.MapFrom(src => src.Supervisor.FirstName))
                .ForMember(dest => dest.SupervisorFamilyName, opt => opt.MapFrom(src => src.Supervisor.FamilyName))
                .ForMember(dest => dest.AdministratorFamilyName, opt => opt.MapFrom(src => src.Administrator.FirstName))
                .ForMember(dest => dest.AdministratorFamilyName, opt => opt.MapFrom(src => src.Administrator.FirstName));

            // Supervisor
            CreateMap<SupervisorInputDto, Supervisor>();
            CreateMap<Supervisor, SupervisorOutputDto>().ForMember(dest => dest.GasStationName, opt => opt.MapFrom(src => src.Station.Name));

        }
    }
}
