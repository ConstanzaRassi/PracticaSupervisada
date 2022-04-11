using System;
using System.Collections.Generic;

#nullable disable

namespace vitamoveAPI.Models
{
    public partial class Rutina
    {
        public Rutina()
        {
            AlumnoRutinas = new HashSet<AlumnoRutina>();
            RutinasEjercicios = new HashSet<RutinasEjercicio>();
        }

        public int IdRutina { get; set; }
        public int? IdDisciplina { get; set; }
        public string Descripcion { get; set; }

        public virtual Disciplina IdDisciplinaNavigation { get; set; }
        public virtual ICollection<AlumnoRutina> AlumnoRutinas { get; set; }
        public virtual ICollection<RutinasEjercicio> RutinasEjercicios { get; set; }
    }
}
