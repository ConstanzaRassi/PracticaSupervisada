using System;
using System.Collections.Generic;

#nullable disable

namespace vitamoveAPI.Models
{
    public partial class Factura
    {
        public int IdFactura { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdPlan { get; set; }
        public int? CodPago { get; set; }
        public DateTime? Fecha { get; set; }
        public double? Total { get; set; }

        public virtual FormasPago CodPagoNavigation { get; set; }
        public virtual Alumno IdAlumnoNavigation { get; set; }
        public virtual Plan IdPlanNavigation { get; set; }
    }
}
