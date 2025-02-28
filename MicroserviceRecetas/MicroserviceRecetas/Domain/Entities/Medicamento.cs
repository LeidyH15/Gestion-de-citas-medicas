using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MicroserviceRecetas.Domain.Entities
{
	public class Medicamento
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Receta")]
        public int IdReceta { get; set; }

        public string NombreMedicamento { get; set; }
        public string Dosis { get; set; }
        public string Frecuencia { get; set; }

        
        public virtual Receta Receta { get; set; }
    }
}