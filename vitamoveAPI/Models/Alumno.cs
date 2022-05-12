using System;
using System.Collections.Generic;

#nullable disable

namespace vitamoveAPI.Models
{
    public partial class Alumno
    {
        public Alumno()
        {
            AlumnoRutinas = new HashSet<AlumnoRutina>();
            ClaseAlumnos = new HashSet<ClaseAlumno>();
            Facturas = new HashSet<Factura>();
        }

        public int IdAlumno { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public DateTime FecNacimiento { get; set; }
        public byte[] Imagen { get; set; }
        public string Email { get; set; }
        public int? IdSexo { get; set; }

        public virtual Sexo IdSexoNavigation { get; set; }
        public virtual ICollection<AlumnoRutina> AlumnoRutinas { get; set; }
        public virtual ICollection<ClaseAlumno> ClaseAlumnos { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
