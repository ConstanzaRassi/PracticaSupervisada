using System;
using System.Collections.Generic;

#nullable disable

namespace vitamoveAPI.Models
{
    public partial class Clase
    {
        public Clase()
        {
            ClaseAlumnos = new HashSet<ClaseAlumno>();
        }

        public int IdClase { get; set; }
        public int? IdDisciplina { get; set; }
        public int? IdSucursal { get; set; }
        public int? IdProfesor { get; set; }
        public int? Cupo { get; set; }
        public int? DiaSemana { get; set; }
        public DateTime? HoraDesde { get; set; }
        public DateTime? HoraHasta { get; set; }

        public virtual Disciplina IdDisciplinaNavigation { get; set; }
        public virtual Profesore IdProfesorNavigation { get; set; }
        public virtual Sucursale IdSucursalNavigation { get; set; }
        public virtual ICollection<ClaseAlumno> ClaseAlumnos { get; set; }
    }
}
