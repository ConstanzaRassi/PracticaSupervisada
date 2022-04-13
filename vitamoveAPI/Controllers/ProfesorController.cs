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
            resultado.Return = db.Profesores.ToList(); //ese alumnos es lo que tengo en context
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

        [HttpGet]
        [Route("[controller]/ObtenerSexos")]
        public ActionResult<ResultAPI> getSexos()
        {
            var resultado = new ResultAPI();
            try
            {
                resultado.Ok = true;
                resultado.Return = db.Sexos.ToList();

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Error al encontrar sexos";

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
            prof.IdSexo = comando.IdSexo;


            db.Profesores.Add(prof);
            db.SaveChanges(); //siempre despues de un insert, update etc hacer el SaveChanges()

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
                prof.IdSexo = comando.IdSexo;
                db.Profesores.Update(prof);
                db.SaveChanges();
            }

            resultado.Ok = true;
            resultado.Return = db.Profesores.ToList();

            return resultado;
        }

    }
}
