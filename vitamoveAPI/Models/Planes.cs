using System;
using System.Collections.Generic;

#nullable disable

namespace vitamoveAPI.Models
{
    public partial class Planes
    {
        public Planes()
        {
            Facturas = new HashSet<Factura>();
        }

        public int IdPlan { get; set; }
        public string Descripcion { get; set; }
        public int? CantMeses { get; set; }
        public double? Precio { get; set; }

        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
