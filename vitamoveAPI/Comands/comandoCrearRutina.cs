﻿using System;

namespace vitamoveAPI.Comands
{
    public class comandoCrearRutina
    {
       
        public int? IdDisciplina { get; set; }
        public int? IdSucursal { get; set; }
        public int? IdProfesor { get; set; }
        public int? Cupo { get; set; }
        public int? DiaSemana { get; set; }
        public DateTime? HoraDesde { get; set; }
        public DateTime? HoraHasta { get; set; }
    }
}
