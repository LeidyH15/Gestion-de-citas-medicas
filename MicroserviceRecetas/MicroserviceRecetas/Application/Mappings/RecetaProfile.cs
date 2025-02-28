using AutoMapper;
using MicroserviceRecetas.Application.DTOs;
using MicroserviceRecetas.Domain.Entities;


namespace MicroserviceRecetas.Application.Mappings
{
    public class RecetaProfile : Profile
    {
        public RecetaProfile()
        {
            CreateMap<Receta, RecetaDTO>()
                .ForMember(dest => dest.Medicamentos, opt => opt.MapFrom(src => src.Medicamentos))
                .ReverseMap();

            CreateMap<Medicamento, MedicamentoDTO>()
                .ReverseMap();
        }
    }
}