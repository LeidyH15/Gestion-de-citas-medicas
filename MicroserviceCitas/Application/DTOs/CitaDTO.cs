using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroserviceCitas.Application.DTOs
{
	public class CitaDTO
	{
        public string Paciente { get; set; }
        public string Medico { get; set; }
        public DateTime Fecha_Hora { get; set; }
        public string Motivo { get; set; }
        public string Lugar { get; set; }
        public string Estado { get; set; }

    }
}