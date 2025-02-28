using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroserviceRecetas.Application.DTOs
{
	public class MedicamentoDTO
	{
        public string NombreMedicamento { get; set; }
        public string Dosis { get; set; }
        public string Frecuencia { get; set; }
    }
}