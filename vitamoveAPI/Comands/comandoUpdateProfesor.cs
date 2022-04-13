using System;

namespace vitamoveAPI.Comands
{
    public class comandoUpdateProfesor
    {
        public int IdProfesor { get; set; }   
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public DateTime FecNacimiento { get; set; }
        //public byte Imagen { get; set; }
        public int? IdSexo { get; set; }
    }
}
