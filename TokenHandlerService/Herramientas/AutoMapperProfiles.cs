using AutoMapper;
using Dominio.Entidades;
using Dto;

namespace Services.Herramientas
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Modelo, ModeloDto>();
            CreateMap<ModeloDto, Modelo>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Color, ColorDto>();
            CreateMap<ColorDto, Color>();
            CreateMap<LineaDeTrabajo, LineaDeTrabajoDto>();
            CreateMap<LineaDeTrabajoDto, LineaDeTrabajo>();
            CreateMap<Defecto, DefectoDto>();
            CreateMap<DefectoDto, Defecto>();
            CreateMap<TurnoDto, Turno>();
            CreateMap<Turno, TurnoDto>();
            CreateMap<Incidencia, IncidenciaDto>();
            CreateMap<IncidenciaDto, Incidencia>();
        }
    }
}
