using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MicroserviceRecetas.Domain.Entities
{
	public class Receta
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int IdCita { get; set; }
        public int IdPaciente { get; set; }
        public string Paciente { get; set; }
        public int IdMedico { get; set; }
        public string Medico { get; set; }
        public DateTime Fecha_Emision { get; set; }
        public string Descriptor { get; set; }

        public virtual ICollection<Medicamento> Medicamentos { get; set; } = new List<Medicamento>();
    }
}