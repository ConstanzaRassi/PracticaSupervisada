using System;
using System.Data;
using System.Linq;
using vitamoveAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vitamoveAPI.Results;
using Microsoft.Extensions.Logging;
using vitamoveAPI.Comands;

namespace vitamoveAPI.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class FacturaController : ControllerBase //hereda de controllerbase
    {

        private readonly vitamoveContext db = new vitamoveContext();
        private readonly ILogger<FacturaController> _logger; //movimientos que los clientes hacen, registro de lo que sucede en el sistema

        public FacturaController(ILogger<FacturaController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("[controller]/ObtenerFacturas")]
        public ActionResult<ResultAPI> Get()
        {
            var resultado = new ResultAPI();
            try
            {
                resultado.Ok = true;
                resultado.Return = db.Facturas.Include(c => c.IdAlumnoNavigation)
                                              .Include(c => c.IdPlanNavigation)
                                              .Include(c => c.CodPagoNavigation)
                                              .ToList();
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Error " + ex.Message;
                return resultado;
            }

        }

        [HttpGet]
        [Route("[controller]/ObtenerFactura/{id}")] //{igual que el get(idUsuario)}
        public ActionResult<ResultAPI> Get(int id)
        {
            var resultado = new ResultAPI();
            try
            {

                var factura = db.Facturas.Where(c => c.IdFactura == id).FirstOrDefault()
                   ;
                resultado.Ok = true;

                resultado.Return = factura;

                return resultado;
            }

            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Factura no encontrada";

                return resultado;
            }
        }

        [HttpGet]
        [Route("[controller]/ObtenerPlanes")]
        public ActionResult<ResultAPI> getPlanes()
        {
            var resultado = new ResultAPI();
            try
            {
                resultado.Ok = true;
                resultado.Return = db.Planes.ToList();

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Error al encontrar planes";

                return resultado;
            }
        }

        [HttpGet]
        [Route("[controller]/ObtenerAlumnos")]
        public ActionResult<ResultAPI> getAlumnos()
        {
            var resultado = new ResultAPI();
            try
            {
                resultado.Ok = true;
                resultado.Return = db.Alumnos.ToList();

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Error al encontrar alumnos";

                return resultado;
            }
        }

        [HttpPost] //nosotros ingresamos los datos
        [Route("[controller]/AltaFactura")]
        public ActionResult<ResultAPI> AltaFactura([FromBody] comandoCrearFactura comando)
        {
            var resultado = new ResultAPI();
            if (comando.IdAlumno.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese sexo";
                return resultado;
            }
            if (comando.IdPlan.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese fecha de nacimiento";
                return resultado;
            }
            if (comando.Fecha.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese documento";
                return resultado;
            }
            if (comando.Total.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese total";
                return resultado;
            }


            var factura = new Factura();
            factura.IdAlumno = comando.IdAlumno;
            factura.IdPlan = comando.IdPlan;
            factura.Fecha = comando.Fecha;
            factura.Total = comando.Total;


            db.Facturas.Add(factura);
            db.SaveChanges();

            resultado.Ok = true;
            resultado.Return = db.Facturas.ToList();

            return resultado;
        }

    }
}
