using System;
using System.Collections.Generic;

#nullable disable

namespace vitamoveAPI.Models
{
    public partial class AlumnoRutina
    {
        public int IdAlumno { get; set; }
        public int IdRutina { get; set; }

        public virtual Alumno IdAlumnoNavigation { get; set; }
        public virtual Rutina IdRutinaNavigation { get; set; }
    }
}
