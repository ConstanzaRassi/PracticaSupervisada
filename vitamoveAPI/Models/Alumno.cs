using System;
using System.Collections.Generic;

#nullable disable

namespace vitamoveAPI.Models
{
    public partial class Alumno
    {
        public Alumno()
        {
            ClaseAlumnos = new HashSet<ClaseAlumno>();
            Facturas = new HashSet<Factura>();
            Rutinas = new HashSet<Rutina>();
        }

        public int IdAlumno { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string Pass { get; set; }
        public DateTime FecNacimiento { get; set; }
        public string Email { get; set; }
        public DateTime Vencimiento { get; set; }
        public int Estado { get; set; }
        public int IdSexo { get; set; }

        public virtual Sexo IdSexoNavigation { get; set; }
        public virtual ICollection<ClaseAlumno> ClaseAlumnos { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }
        public virtual ICollection<Rutina> Rutinas { get; set; }
    }
}
