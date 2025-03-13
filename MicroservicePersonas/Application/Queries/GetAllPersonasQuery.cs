using MediatR;
using MicroservicePersonas.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroservicePersonas.Application.Queries
{
    public class GetAllPersonasQuery : IRequest<List<PersonaDTO>> { 
    
    }
}