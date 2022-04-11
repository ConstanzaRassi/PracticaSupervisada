using System;
using System.Collections.Generic;

#nullable disable

namespace vitamoveAPI.Models
{
    public partial class FormasPago
    {
        public FormasPago()
        {
            DetallesPagos = new HashSet<DetallesPago>();
        }

        public int CodPago { get; set; }
        public string Descripcion { get; set; }
        public double? PorcRecargo { get; set; }

        public virtual ICollection<DetallesPago> DetallesPagos { get; set; }
    }
}
