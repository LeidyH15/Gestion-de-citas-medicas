using AutoMapper;
using MicroserviceCitas.Application.DTOs;
using MicroserviceCitas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroserviceCitas.Application.Mappings
{
    public class CitaProfile : Profile
    {
        public CitaProfile()
        {
            CreateMap<Cita, CitaDTO>()
                .ReverseMap();

        }
    }
}