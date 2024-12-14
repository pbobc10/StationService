using AutoMapper;
using StationService.DTOs;
using StationService.Models;

namespace StationService.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
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
            CreateMap<GasStationInputDto, GasStation>();
            CreateMap<GasStation, GasStationOutputDto>()
                .ForMember(dest => dest.DispensingUnitCount, opt => opt.MapFrom(src => src.DispensingUnits.Count))
                .ForMember(dest => dest.GasStationAttendantCount, opt => opt.MapFrom(src => src.GasStationAttendants.Count))
                .ForMember(dest => dest.SupervisorName, opt => opt.MapFrom(src => src.Supervisor != null ? $"{src.Supervisor.FirstName} {src.Supervisor.FamilyName}" : null))
                .ForMember(dest => dest.AdministratorName, opt => opt.MapFrom(src => $"{src.Administrator.FirstName} {src.Administrator.FamilyName}"));

            CreateMap<GasStation, GasStationOutputDetailDto>()
                .ForMember(dest => dest.SupervisorName, opt => opt.MapFrom(src => src.Supervisor != null ? $"{src.Supervisor.FirstName} {src.Supervisor.FamilyName}" : null))
                 .ForMember(dest => dest.AdministratorName, opt => opt.MapFrom(src => $"{src.Administrator.FirstName} {src.Administrator.FamilyName}"));

            CreateMap<GasStationOutputDetailDto, GasStationInputDto>()
                .ForMember(dest => dest.SupervisorId, opt => opt.MapFrom(src => src.SupervisorId))
                .ForMember(dest => dest.AdministratorId, opt => opt.MapFrom(src => src.AdministratorId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));

            // Supervisor
            CreateMap<SupervisorInputDto, Supervisor>();
            CreateMap<Supervisor, SupervisorOutputDto>()
                .ForMember(dest => dest.GasStationName, opt => opt.MapFrom(src => src.Station.Name));


        }
    }
}
