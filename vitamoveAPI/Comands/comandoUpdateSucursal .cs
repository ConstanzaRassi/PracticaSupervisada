using System;

namespace vitamoveAPI.Comands
{
    public class comandoUpdateSucursal
    {       
        public int IdSucursal { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int IdBarrio { get; set; }
        public int Estado { get; set; }
    }
}
