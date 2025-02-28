using AutoMapper;
using MicroservicePersonas.Application.DTOs;
using MicroservicePersonas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroservicePersonas.Application.Mappings
{
	public class PersonaProfile : Profile
    {
        public PersonaProfile()
        {
            CreateMap<PersonaDTO, Persona>()
            .ForMember(dest => dest.TipoPersona, opt => opt.Ignore()); // Ignora el campo si no se usa

            CreateMap<Persona, PersonaDTO>();

            // Si `TipoPersona` es una entidad y `PersonaDTO` lo maneja como un `string`, define la conversión:
            CreateMap<string, TipoPersona>()
                .ConvertUsing(str => new TipoPersona { Descriptor = str });

        }
    }
}