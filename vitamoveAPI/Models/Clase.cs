﻿using System;
using System.Collections.Generic;

#nullable disable

namespace vitamoveAPI.Models
{
    public partial class Clase
    {
        public int IdClase { get; set; }
        public int IdDisciplina { get; set; }
        public int IdSucursal { get; set; }
        public int IdProfesor { get; set; }
        public int Cupo { get; set; }
        public int DiaSemana { get; set; }
        public string HoraDesde { get; set; }
        public string HoraHasta { get; set; }
        public int Estado { get; set; }

        public virtual Disciplina IdDisciplinaNavigation { get; set; }
        public virtual Profesor IdProfesorNavigation { get; set; }
        public virtual Sucursal IdSucursalNavigation { get; set; }
    }
}
