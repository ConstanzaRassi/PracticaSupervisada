using System;
using System.Linq;
using vitamoveAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vitamoveAPI.Results;
using Microsoft.Extensions.Logging;
using vitamoveAPI.Comands;

namespace vitamoveAPI.Controllers
{
    [ApiController]
    public class ReporteController : ControllerBase 
    {
        private readonly vitamoveContext db = new vitamoveContext();
        private readonly ILogger<ReporteController> _logger;

        public ReporteController(ILogger<ReporteController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("[controller]/ObtenerTotalFacturado")]
        public ActionResult<ResultAPI> GetTotalFacturado(DateTime desde, DateTime hasta)
        {
            var resultado = new ResultAPI();
            resultado.Ok = true;
            resultado.Return = db.Facturas.Where(c => c.Fecha >= desde && c.Fecha <= hasta)
                                            .Sum(c => c.Total);
                                           
            return resultado;
        }

        [HttpGet]
        [Route("[controller]/ObtenerClasesDictadas")]
        public ActionResult<ResultAPI> GetTotalClases(DateTime desde, DateTime hasta)
        {
            var resultado = new ResultAPI();
            resultado.Ok = true;
            resultado.Return = db.ClaseAlumnos.Where(c => c.Fecha >= desde && c.Fecha <= hasta)
                .GroupBy(c => c.Fecha, c => c.IdClase).Count(); 
                
                                              
                                              

            return resultado;
        }

        [HttpGet]
        [Route("[controller]/ObtenerPromedioAlumnos")]
        public ActionResult<ResultAPI> GetPromedioAlumnos(DateTime desde, DateTime hasta)
        {
            var resultado = new ResultAPI();
            resultado.Ok = true;
            double select1 = db.ClaseAlumnos.Where(c => c.Fecha >= desde && c.Fecha <= hasta)
                                           .GroupBy(c => new { c.Fecha, c.IdAlumno })
                                           .Count();

            double select2 = db.ClaseAlumnos.Where(c => c.Fecha >= desde && c.Fecha <= hasta)
                                           .GroupBy(c => new { c.Fecha, c.IdClase })
                                           .Count();

           
            resultado.Return = select1/select2;

            return resultado;
        }

        [HttpGet]
        [Route("[controller]/InicializaTotalFacturado")]
        public ActionResult<ResultAPI> InicializaFacturacion(DateTime desde, DateTime hasta)
        {
            var resultado = new ResultAPI();
            resultado.Ok = true;
            resultado.Return = db.Facturas.Where(c => c.Fecha >= desde && c.Fecha <= hasta)
                                            .Sum(c => c.Total);

            return resultado;
        }

        [HttpGet]
        [Route("[controller]/InicializaClasesDictadas")]
        public ActionResult<ResultAPI> InicializaTotalClases()
        {
            var resultado = new ResultAPI();
            resultado.Ok = true;
            resultado.Return = db.ClaseAlumnos.GroupBy(c => c.Fecha, c => c.IdClase).Count();

            return resultado;
        }

        [HttpGet]
        [Route("[controller]/InicializaPromedioAlumnos")]
        public ActionResult<ResultAPI> InicializaPromedioAlumnos()
        {
            var resultado = new ResultAPI();
            resultado.Ok = true;
            double select1 = db.ClaseAlumnos.GroupBy(c => new { c.Fecha, c.IdAlumno })
                                           .Count();
                                           

            double select2 = db.ClaseAlumnos.GroupBy(c => new { c.Fecha, c.IdClase })
                                           .Count();

            resultado.Return = select1 / select2;

            return resultado;
        }

        //[HttpGet]
        //[Route("[controller]/ObtenerAlumnosXDisciplina")]
        //public ActionResult<ResultAPI> GetAlumnosXDisciplina(DateTime desde, DateTime hasta)
        //{
        //    var resultado = new ResultAPI();
        //    resultado.Ok = true;
        //    resultado.Return = db.Facturas.Where(c => c.Fecha >= desde && c.Fecha <= hasta)
        //                                    .Sum(c => c.Total);

        //    return resultado;
        //}

        [HttpGet]
        [Route("[controller]/ObtenerDiaMayorConcurrencia")]
        public ActionResult<ResultAPI> GetDiaMayorConcurrencia()
        {
            var resultado = new ResultAPI();
            resultado.Ok = true;
            //resultado.Return = from ca in db.ClaseAlumnos
            //                   join cl in db.Clases on ca.IdClase equals cl.IdClase

            //                   group new { cl, ca } by new
            //                   {
            //                       cl.IdClase
            //                   } into g
            //                   orderby
            //                     g.Count(p => p.ca.IdAlumno != null) descending
            //                   select new
            //                   {
            //                       DiaSemana = (int?)g.Key.Cl.DiaSemana,
            //                       Column1 = g.Count(p => p.ca.IdAlumno != null)
            //                   };


            //resultado.Return = from ca in db.ClaseAlumnos
            //                   join cl in db.Clases on ca.IdClase equals cl.IdClase
            //                   join d in db.Disciplinas on cl.IdDisciplina equals d.IdDisciplina
            //                   group new { d, ca } by new
            //                   {
            //                       d.Descripcion
            //                   } into g
            //                   select new
            //                   {
            //                       Cantidad = g.Count(p => p.ca.IdAlumno != null),
            //                       g.Key.Descripcion
            //                   };

            return resultado;
        }

        [HttpGet]
        [Route("[controller]/ObtenerAlumnosActivos")]
        public ActionResult<ResultAPI> GetAlumnosActivos()
        {
            var resultado = new ResultAPI();
            resultado.Ok = true;
            resultado.Return = db.Alumnos.Where(c => c.Estado == 1).Count();
                                         //.Count(c => c.IdAlumno);

            return resultado;
        }

        //[HttpGet]
        //[Route("[controller]/ObtenerSucursalMayorConcurrencia")]
        //public ActionResult<ResultAPI> GetSucursalMayorConcurrencia()
        //{
        //    var resultado = new ResultAPI();
        //    resultado.Ok = true;
        //    resultado.Return = db.Facturas.Where(c => c.Fecha >= desde && c.Fecha <= hasta)
        //                                    .Sum(c => c.Total);

        //    return resultado;
        //}

        


    }
}
