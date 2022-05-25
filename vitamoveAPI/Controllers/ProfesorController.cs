using System;
using System.Linq;
using vitamoveAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vitamoveAPI.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using vitamoveAPI.Comands;

namespace vitamoveAPI.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class ProfesorController : ControllerBase //hereda de controllerbase
    {

        private readonly vitamoveContext db = new vitamoveContext();
        private readonly ILogger<ProfesorController> _logger; //movimientos que los clientes hacen, registro de lo que sucede en el sistema

        public ProfesorController(ILogger<ProfesorController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("[controller]/ObtenerProfesor")]
        public ActionResult<ResultAPI> Get()
        {
            var resultado = new ResultAPI();
            resultado.Ok = true;
            resultado.Return = db.Profesores.Include(c => c.IdSexoNavigation)
                                            .ToList();
            return resultado;
        }

        [HttpGet]
        [Route("[controller]/ObtenerProfesor/{id}")] //{igual que el get(idUsuario)}
        public ActionResult<ResultAPI> Get(int id)
        {
            var resultado = new ResultAPI();
            try
            {

                var prof = db.Profesores.Where(c => c.IdProfesor == id).FirstOrDefault();
                resultado.Ok = true;
                resultado.Return = prof;

                return resultado;
            }

            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Profesor no encontrado";

                return resultado;
            }
        }

        

        [HttpPost] //nosotros ingresamos los datos
        [Route("[controller]/AltaProfesor")]
        public ActionResult<ResultAPI> AltaProfesor([FromBody] comandoCrearProfesor comando)
        {
            var resultado = new ResultAPI();
            if (comando.Nombre.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese nombre";
                return resultado;
            }
            if (comando.Apellido.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese apellido";
                return resultado;
            }
            if (comando.Dni.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese documento";
                return resultado;
            }
            if (comando.FecNacimiento.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese fecha de nacimiento";
                return resultado;
            }            
            if (comando.IdSexo.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese sexo";
                return resultado;
            }


            var prof = new Profesor();
            prof.Nombre = comando.Nombre;
            prof.Apellido = comando.Apellido;
            prof.Dni = comando.Dni;
            prof.FecNacimiento = comando.FecNacimiento;
            prof.Estado = 1;
            prof.IdSexo = comando.IdSexo;


            db.Profesores.Add(prof);
            db.SaveChanges();

            resultado.Ok = true;
            resultado.Return = db.Profesores.ToList();

            return resultado;
        }

        [HttpPut]
        [Route("[controller]/UpdateProfesor")]
        public ActionResult<ResultAPI> UpdateProfesor([FromBody] comandoUpdateProfesor comando)
        {
            var resultado = new ResultAPI();
            if (comando.Nombre.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese nombre";
                return resultado;
            }
            if (comando.Apellido.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese apellido";
                return resultado;
            }
            if (comando.Dni.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese documento";
                return resultado;
            }
            if (comando.FecNacimiento.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese fecha de nacimiento";
                return resultado;
            }
            if (comando.IdSexo.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese sexo";
                return resultado;
            }

            var prof = db.Profesores.Where(c => c.IdProfesor == comando.IdProfesor).FirstOrDefault();
            if (prof != null)
            {
                prof.Nombre = comando.Nombre;
                prof.Apellido = comando.Apellido;
                prof.Dni = comando.Dni;
                prof.FecNacimiento = comando.FecNacimiento;
                prof.Estado = 1;
                prof.IdSexo = comando.IdSexo;
                db.Profesores.Update(prof);
                db.SaveChanges();
            }

            resultado.Ok = true;
            resultado.Return = db.Profesores.ToList();

            return resultado;
        }
        [HttpPut]
        [Route("[controller]/UpdateEstadoProfesor/{id}")]
        public ActionResult<ResultAPI> UpdateEstadoProfesor(int id)
        {
            var resultado = new ResultAPI();

            var prof = db.Profesores.Where(c => c.IdProfesor == id).FirstOrDefault();
            if (prof != null && prof.Estado == 1)
            {
                prof.Estado = 0;
                db.Profesores.Update(prof);
                db.SaveChanges();
            }
            else if (prof != null && prof.Estado == 0)
            {
                prof.Estado = 1;
                db.Profesores.Update(prof);
                db.SaveChanges();
            }

            resultado.Ok = true;
            resultado.Return = db.Profesores.ToList();

            return resultado;
        }

    }
}
