using System;
using System.Collections.Generic;

#nullable disable

namespace vitamoveAPI.Models
{
    public partial class RutinasEjercicio
    {
        public int IdRutina { get; set; }
        public int IdEjercicio { get; set; }
        public int? Repeticiones { get; set; }
        public string Tiempo { get; set; }

        public virtual Ejercicio IdEjercicioNavigation { get; set; }
        public virtual Rutina IdRutinaNavigation { get; set; }
    }
}
