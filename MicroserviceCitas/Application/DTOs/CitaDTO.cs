using System;

namespace MicroserviceCitas.Application.DTOs
{
	public class CitaDTO
	{
        public string IdPaciente { get; set; }
        public string Paciente { get; set; }
        public string IdMedico { get; set; }
        public string Medico { get; set; }
        public DateTime Fecha_Hora { get; set; }
        public string Motivo { get; set; }
        public string Lugar { get; set; }
        public string Estado { get; set; }

    }
}