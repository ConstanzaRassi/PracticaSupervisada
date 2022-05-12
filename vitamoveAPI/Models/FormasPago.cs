using System;
using System.Collections.Generic;

#nullable disable

namespace vitamoveAPI.Models
{
    public partial class FormasPago
    {
        public FormasPago()
        {
            Facturas = new HashSet<Factura>();
        }

        public int CodPago { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
