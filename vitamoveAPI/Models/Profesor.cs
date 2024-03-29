﻿using System;
using System.Collections.Generic;

#nullable disable

namespace vitamoveAPI.Models
{
    public partial class Profesor
    {
        public Profesor()
        {
            Clases = new HashSet<Clase>();
        }

        public int IdProfesor { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public DateTime FecNacimiento { get; set; }
        public string Email { get; set; }
        public int Estado { get; set; }
        public int IdSexo { get; set; }

        public virtual Sexo IdSexoNavigation { get; set; }
        public virtual ICollection<Clase> Clases { get; set; }
    }
}
