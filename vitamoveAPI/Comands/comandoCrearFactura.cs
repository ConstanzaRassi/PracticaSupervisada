using System;

namespace vitamoveAPI.Comands
{
    public class comandoCrearFactura
    {
       
        public int? IdAlumno { get; set; }
        public int IdPlan { get; set; }       
        public int? CodPago { get; set; }       
        public DateTime? Fecha { get; set; }
        public double Total { get; set; }


    }
}
