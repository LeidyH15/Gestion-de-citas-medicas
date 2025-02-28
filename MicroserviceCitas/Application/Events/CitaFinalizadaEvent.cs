using MicroserviceCitas.Application.DTOs;
using System;
using System.Collections.Generic;

namespace MicroserviceCitas.Application.Events
{
	public class CitaFinalizadaEvent
	{
        public int IdCita { get; set; }
        public string Paciente { get; set; }
        public string Medico { get; set; }
        public DateTime FechaEmision { get; set; }
        public string Descriptor { get; set; }
        public List<MedicamentoDTO> Medicamentos { get; set; }
    }
}