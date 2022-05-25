using System;
using System.Linq;
using vitamoveAPI.Models;
using Microsoft.AspNetCore.Mvc;
using vitamoveAPI.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using vitamoveAPI.Comands;

namespace vitamoveAPI.Controllers
{
    [ApiController]    
    public class AlumnoController : ControllerBase
    {

        private readonly vitamoveContext db = new vitamoveContext();
        private readonly ILogger<AlumnoController> _logger;

        public AlumnoController(ILogger<AlumnoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("[controller]/ObtenerAlumno")]
        public ActionResult<ResultAPI> GetAll()
        {
            var resultado = new ResultAPI();
            resultado.Ok = true;
            resultado.Return = db.Alumnos.Include(c => c.IdSexoNavigation)
                                         .OrderBy(c => c.IdAlumno)
                                         .ToList();
            return resultado;
        }

        [HttpGet]
        [Route("[controller]/ObtenerById/{id}")]
        public ActionResult<ResultAPI> GetById(int id)
        {
            var resultado = new ResultAPI();
            try
            {
                resultado.Ok = true;
                resultado.Return = db.Alumnos.Include(c => c.IdSexoNavigation)
                                    .Where(c => c.IdAlumno == id)
                                    .FirstOrDefault();
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
        [Route("[controller]/ObtenerByDni/{dni}")]
        public ActionResult<ResultAPI> GetByDni(string dni)
        {
            var resultado = new ResultAPI();
            try
            {
                resultado.Ok = true;
                resultado.Return = db.Alumnos.Include(c => c.IdSexoNavigation)
                                    .Where(c => c.Dni == dni)
                                    .FirstOrDefault();
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

        [HttpPost]
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
            if (comando.Email.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese email";
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
            alu.Email = comando.Email;
            alu.FecNacimiento = comando.FecNacimiento;
            alu.Estado = 1;
            alu.IdSexo = comando.IdSexo;

            db.Alumnos.Add(alu);
            db.SaveChanges();

            resultado.Ok = true;
            resultado.Return = db.Alumnos.ToList();

            return resultado;
        }


        //[HttpPut]
        //[Route("[controller]/ModificarAlumno/{id}")] //{igual que el get(idUsuario)}
        //public ActionResult<ResultAPI> ModificarAlumno([FromBody] comandoUpdateAlumno comando)
        //{
        //    var resultado = new ResultAPI();
        //    try
        //    {

        //        var alu = db.Alumnos.Where(c => c.IdAlumno == id).FirstOrDefault();
        //        resultado.Ok = true;
        //        resultado.Return = alu;

        //        return resultado;
        //    }

        //    catch (Exception ex)
        //    {
        //        resultado.Ok = false;
        //        resultado.Error = "Alumno no encontrado";

        //        return resultado;
        //    }
        //}
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
                alu.Estado = 1;
                db.Alumnos.Update(alu);
                db.SaveChanges();
            }

            resultado.Ok = true;
            resultado.Return = db.Alumnos.ToList();

            return resultado;
        }

        [HttpPut]
        [Route("[controller]/UpdateEstado/{id}")]
        public ActionResult<ResultAPI> UpdateEstadoAlumno(int id)
        {
            var resultado = new ResultAPI();

            var alum = db.Alumnos.Where(c => c.IdAlumno == id).FirstOrDefault();
            if (alum != null && (alum.Estado == 1 || alum.Estado == 2))
            {
                alum.Estado = 0;
                db.Alumnos.Update(alum);
                db.SaveChanges();
            }
            else if (alum != null && alum.Estado == 0)
            {
                alum.Estado = 1;
                db.Alumnos.Update(alum);
                db.SaveChanges();
            }

            resultado.Ok = true;
            resultado.Return = db.Alumnos.ToList();

            return resultado;
        }
    }
}
