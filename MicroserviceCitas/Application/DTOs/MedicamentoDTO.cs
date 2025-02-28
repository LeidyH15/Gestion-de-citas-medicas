using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroserviceCitas.Application.DTOs
{
	public class MedicamentoDTO
	{
        public string Medicamento { get; set; }
        public string Dosis { get; set; }
        public string Frecuencia { get; set; }
    }
}