using System;
using System.Collections.Generic;

#nullable disable

namespace vitamoveAPI.Models
{
    public partial class Disciplina
    {
        public Disciplina()
        {
            Clases = new HashSet<Clase>();
            Rutinas = new HashSet<Rutina>();
        }

        public int IdDisciplina { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Clase> Clases { get; set; }
        public virtual ICollection<Rutina> Rutinas { get; set; }
    }
}
