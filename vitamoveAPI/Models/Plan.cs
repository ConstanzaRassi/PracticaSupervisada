using System;
using System.Collections.Generic;

#nullable disable

namespace vitamoveAPI.Models
{
    public partial class Plan
    {
        public Plan()
        {
            Facturas = new HashSet<Factura>();
        }

        public int IdPlan { get; set; }
        public string Descripcion { get; set; }
        public int? CantMeses { get; set; }
        public double? Precio { get; set; }
        public int? Estado { get; set; }

        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
