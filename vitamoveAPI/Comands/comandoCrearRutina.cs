using System;
using System.Collections.Generic;
using vitamoveAPI.Models;

namespace vitamoveAPI.Comands
{
    public class comandoCrearRutina
    {
        public int IdAlumno { get; set; }
        public int IdDisciplina { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<RutinasEjercicio> RutinasEjercicios { get; set; }
    }
}
