using System;
using System.Collections.Generic;

#nullable disable

namespace vitamoveAPI.Models
{
    public partial class DetallesPago
    {
        public int IdFactura { get; set; }
        public int CodPago { get; set; }
        public double? Importe { get; set; }
        public double? Recargo { get; set; }

        public virtual FormasPago CodPagoNavigation { get; set; }
        public virtual Factura IdFacturaNavigation { get; set; }
    }
}
