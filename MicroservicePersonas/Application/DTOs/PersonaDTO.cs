using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroservicePersonas.Application.DTOs
{
	public class PersonaDTO
	{
        public string TipoPersona { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public string Genero { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Especialidad { get; set; }
    }
}