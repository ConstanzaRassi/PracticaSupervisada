using System;
using System.Collections.Generic;

#nullable disable

namespace vitamoveAPI.Models
{
    public partial class Barrio
    {
        public Barrio()
        {
            Sucursales = new HashSet<Sucursal>();
        }

        public int IdBarrio { get; set; }
        public string Barrio1 { get; set; }

        public virtual ICollection<Sucursal> Sucursales { get; set; }
    }
}
