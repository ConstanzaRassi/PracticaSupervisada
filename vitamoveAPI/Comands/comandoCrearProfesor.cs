using System;

namespace vitamoveAPI.Comands
{
    public class comandoCrearProfesor
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public DateTime FecNacimiento { get; set; }
        public int Estado { get; set; }
        public int? IdSexo { get; set; }
    }
}
