using MediatR;
using MicroserviceCitas.Application.Queries;
using MicroserviceCitas.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using MicroserviceCitas.Application.DTOs;
using MicroserviceCitas.Application.Interfaces;

namespace MicroserviceCitas.Application.Handlers
{
    public class GetCitaByIdHandler : IRequestHandler<GetCitaByIdQuery, CitaDTO>
    {
        private readonly ICitaService _citaService;
        public GetCitaByIdHandler(ICitaService citaService) => _citaService = citaService;
        public async Task<CitaDTO> Handle(GetCitaByIdQuery request, CancellationToken cancellationToken) 
            => await _citaService.GetById(request.Id);   
    }
}