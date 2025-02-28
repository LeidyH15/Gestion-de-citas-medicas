using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MicroservicePersonas.Domain.Entities
{
	public class Persona
	{
		public Persona()
		{
            this.Activo = true;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Range(1, 2, ErrorMessage = "El IdTipoPersona solo puede ser 1 (Médico) o 2 (Paciente).")]
        public int IdTipoPersona { get; set; } // 1 - Médico o 2 - Paciente
        [Index(IsUnique = true)]
        [StringLength(20)]
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        [Column(TypeName = "date")]
        public DateTime Fecha_Nacimiento { get; set; }
        [RegularExpression("^[FM]$", ErrorMessage = "El género debe ser 'F' o 'M'.")]
        [StringLength(1)]
        public string Genero { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public bool Activo { get; set; }
        public string Especialidad { get; set; }

        [ForeignKey("IdTipoPersona")]
        public virtual TipoPersona TipoPersona { get; set; }

    }
}