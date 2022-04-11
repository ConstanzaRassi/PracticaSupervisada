using System;
using System.Collections.Generic;

#nullable disable

namespace vitamoveAPI.Models
{
    public partial class Factura
    {
        public Factura()
        {
            DetallesPagos = new HashSet<DetallesPago>();
        }

        public int IdFactura { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdPlan { get; set; }
        public DateTime? Fecha { get; set; }

        public virtual Alumno IdAlumnoNavigation { get; set; }
        public virtual Plane IdPlanNavigation { get; set; }
        public virtual ICollection<DetallesPago> DetallesPagos { get; set; }
    }
}
