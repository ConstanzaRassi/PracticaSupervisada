using System;
using System.Collections.Generic;

#nullable disable

namespace vitamoveAPI.Models
{
    public partial class Ejercicio
    {
        public Ejercicio()
        {
            RutinasEjercicios = new HashSet<RutinasEjercicio>();
        }

        public int IdEjercicio { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<RutinasEjercicio> RutinasEjercicios { get; set; }
    }
}
