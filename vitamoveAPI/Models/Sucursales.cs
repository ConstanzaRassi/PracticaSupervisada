using System;
using System.Collections.Generic;

#nullable disable

namespace vitamoveAPI.Models
{
    public partial class Sucursales
    {
        public Sucursales()
        {
            Clases = new HashSet<Clase>();
        }

        public int IdSucursal { get; set; }
        public string Nombre { get; set; }
        public string Dirección { get; set; }
        public int? IdBarrio { get; set; }

        public virtual Barrio IdBarrioNavigation { get; set; }
        public virtual ICollection<Clase> Clases { get; set; }
    }
}
