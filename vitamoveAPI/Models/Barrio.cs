using System;
using System.Collections.Generic;

#nullable disable

namespace vitamoveAPI.Models
{
    public partial class Barrio
    {
        public Barrio()
        {
            Sucursales = new HashSet<Sucursales>();
        }

        public int IdBarrio { get; set; }
        public string Barrio1 { get; set; }

        public virtual ICollection<Sucursales> Sucursales { get; set; }
    }
}
