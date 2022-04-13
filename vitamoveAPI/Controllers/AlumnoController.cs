using System;
using System.Linq;
using vitamoveAPI.Models;
using Microsoft.AspNetCore.Mvc;
using vitamoveAPI.Results;
using Microsoft.Extensions.Logging;
using vitamoveAPI.Comands;

namespace vitamoveAPI.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class AlumnoController : ControllerBase //hereda de controllerbase
    {

        private readonly vitamoveContext db = new vitamoveContext();
        private readonly ILogger<AlumnoController> _logger; //movimientos que los clientes hacen, registro de lo que sucede en el sistema

        public AlumnoController(ILogger<AlumnoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("[controller]/ObtenerAlumno")]
        public ActionResult<ResultAPI> Get()
        {
            var resultado = new ResultAPI();
            resultado.Ok = true;
            resultado.Return = db.Alumnos.ToList(); //ese alumnos es lo que tengo en context
            return resultado;
        }

        [HttpGet]
        [Route("[controller]/ObtenerAlumno/{id}")] //{igual que el get(idUsuario)}
        public ActionResult<ResultAPI> Get3(int id)
        {
            var resultado = new ResultAPI();
            try
            {

                var alu = db.Alumnos.Where(c => c.IdAlumno == id).FirstOrDefault();
                resultado.Ok = true;
                resultado.Return = alu;

                return resultado;
            }

            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Alumno no encontrado";

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
        [Route("[controller]/AltaAlumno")]
        public ActionResult<ResultAPI> AltaAlumno([FromBody] comandoCrearAlumno comando)
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


            var alu = new Alumno();
            alu.Nombre = comando.Nombre;
            alu.Apellido = comando.Apellido;
            alu.Dni = comando.Dni;
            alu.FecNacimiento = comando.FecNacimiento;
            alu.IdSexo = comando.IdSexo;


            db.Alumnos.Add(alu);
            db.SaveChanges(); //siempre despues de un insert, update etc hacer el SaveChanges()

            resultado.Ok = true;
            resultado.Return = db.Alumnos.ToList();

            return resultado;
        }

        [HttpPut]
        [Route("[controller]/UpdateAlumno")]
        public ActionResult<ResultAPI> UpdateAlumno([FromBody] comandoUpdateAlumno comando)
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

            var alu = db.Alumnos.Where(c => c.IdAlumno == comando.IdAlumno).FirstOrDefault();
            if (alu != null)
            {
                alu.Nombre = comando.Nombre;
                alu.Apellido = comando.Apellido;
                alu.Dni = comando.Dni;
                alu.FecNacimiento = comando.FecNacimiento;
                alu.IdSexo = comando.IdSexo;
                db.Alumnos.Update(alu);
                db.SaveChanges();
            }

            resultado.Ok = true;
            resultado.Return = db.Alumnos.ToList();

            return resultado;
        }

    }
}
