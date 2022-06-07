using System;

namespace vitamoveAPI.Comands
{
    public class comandoUpdateClase
    {
        public int IdClase { get; set; }   
        public int IdDisciplina { get; set; }
        public int IdSucursal { get; set; }
        public int IdProfesor { get; set; }
        public int Cupo { get; set; }
        public int DiaSemana { get; set; }
        public string HoraDesde { get; set; }
        public string HoraHasta { get; set; }
    }
}
