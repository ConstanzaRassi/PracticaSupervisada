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

        //[HttpGet]
        //[Route("[controller]/ObtenerDiaMayorConcurrencia")]
        //public ActionResult<ResultAPI> GetDiaMayorConcurrencia()
        //{
        //    var resultado = new ResultAPI();
        //    resultado.Ok = true;
        //    //resultado.Return = db.ClaseAlumnos.Order By GroupBy(c => c.IdClase).Count().

        //    var hola = db.ClaseAlumnos.GroupBy(c => c.IdClase);


        //    var ja = from c in hola select new {c.IdClase, }

        //    return resultado;
        //}

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

        

        [HttpGet]
        [Route("[controller]/InicializaPromedio")]
        public ActionResult<ResultAPI> GetInicializaPromedio()
        {
            var resultado = new ResultAPI();
            resultado.Ok = true;
            resultado.Return = db.Facturas.Sum(c => c.Total);

            return resultado;
        }

        [HttpGet]
        [Route("[controller]/InicializaFacturacion")]
        public ActionResult<ResultAPI> GetInicializaFacturacion()
        {
            var resultado = new ResultAPI();
            resultado.Ok = true;
            resultado.Return = db.Facturas.Sum(c => c.Total);

            return resultado;
        }

        [HttpGet]
        [Route("[controller]/InicializaClasesDictadas")]
        public ActionResult<ResultAPI> GetInicializaClasesDictadas()
        {
            var resultado = new ResultAPI();
            resultado.Ok = true;
            resultado.Return = db.Facturas.Sum(c => c.Total);

            return resultado;
        }


    }
}
