﻿using System;
using System.Collections.Generic;

#nullable disable

namespace vitamoveAPI.Models
{
    public partial class Sexo
    {
        public Sexo()
        {
            Alumnos = new HashSet<Alumno>();
            Profesores = new HashSet<Profesore>();
        }

        public int IdSexo { get; set; }
        public string Sexo1 { get; set; }

        public virtual ICollection<Alumno> Alumnos { get; set; }
        public virtual ICollection<Profesore> Profesores { get; set; }
    }
}
