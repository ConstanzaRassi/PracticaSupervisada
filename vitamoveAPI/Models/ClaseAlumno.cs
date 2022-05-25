using System;
using System.Collections.Generic;

#nullable disable

namespace vitamoveAPI.Models
{
    public partial class ClaseAlumno
    {
        public int IdClase { get; set; }
        public int IdAlumno { get; set; }
        public DateTime? Fecha { get; set; }
        public int? Estado { get; set; }

        public virtual Alumno IdAlumnoNavigation { get; set; }
        public virtual Clase IdClaseNavigation { get; set; }
    }
}
