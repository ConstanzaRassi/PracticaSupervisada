using System;
using System.Linq;
using vitamoveAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vitamoveAPI.Results;
using Microsoft.Extensions.Logging;
using vitamoveAPI.Comands;
using Microsoft.EntityFrameworkCore;

namespace vitamoveAPI.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class RutinaController : ControllerBase //hereda de controllerbase
    {

        private readonly vitamoveContext db = new vitamoveContext();
        private readonly ILogger<RutinaController> _logger; //movimientos que los clientes hacen, registro de lo que sucede en el sistema

        public RutinaController(ILogger<RutinaController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("[controller]/ObtenerRutina")]
        public ActionResult<ResultAPI> Get()
        {
            var resultado = new ResultAPI();
            resultado.Ok = true;
            resultado.Return = db.Rutinas.ToList(); //ese alumnos es lo que tengo en context
            return resultado;
        }

        [HttpGet]
        [Route("[controller]/ObtenerRutina/{id}")]
        public ActionResult<ResultAPI> Get(int id)
        {
            var resultado = new ResultAPI();
            try
            {

                var rutina = db.Rutinas.Where(c => c.IdRutina == id).FirstOrDefault();
                resultado.Ok = true;
                resultado.Return = rutina;

                return resultado;
            }

            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Rutina no encontrado";

                return resultado;
            }
        }

        [HttpGet]
        [Route("[controller]/ObtenerRutinaByIdAlumno/{id}")]
        public ActionResult<ResultAPI> GetbyIdAlumno(int id)
        {
            var resultado = new ResultAPI();
            try
            {

                var rutina = db.Rutinas.Where(c => c.IdAlumno == id)
                                       .Include(c => c.IdDisciplinaNavigation)
                                       .Include(c => c.RutinasEjercicios)
                                       .ThenInclude(c => c.IdEjercicioNavigation)
                                       .ToList();
                resultado.Ok = true;
                resultado.Return = rutina;

                return resultado;
            }

            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Rutina no encontrado";

                return resultado;
            }
        }

        [HttpGet]
        [Route("[controller]/ObtenerDisciplinas")]
        public ActionResult<ResultAPI> getDisciplinas()
        {
            var resultado = new ResultAPI();
            try
            {
                resultado.Ok = true;
                resultado.Return = db.Disciplinas.ToList();

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Error al encontrar disciplinas";

                return resultado;
            }
        }

        [HttpGet]
        [Route("[controller]/ObtenerSucursales")]
        public ActionResult<ResultAPI> getSucursales()
        {
            var resultado = new ResultAPI();
            try
            {
                resultado.Ok = true;
                resultado.Return = db.Sucursales.ToList();

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Error al encontrar sucursales";

                return resultado;
            }
        }

        [HttpGet]
        [Route("[controller]/ObtenerProfesores")]
        public ActionResult<ResultAPI> getSProfesores()
        {
            var resultado = new ResultAPI();
            try
            {
                resultado.Ok = true;
                resultado.Return = db.Profesores.ToList();

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Error al encontrar profesores";

                return resultado;
            }
        }

        [HttpPost]
        [Route("[controller]/AltaRutina")]
        public ActionResult<ResultAPI> AltaRutina([FromBody] comandoCrearRutina comando)
        {
            var resultado = new ResultAPI();

            if (comando.IdDisciplina.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese disciplina";
                return resultado;
            }
            if (comando.IdAlumno.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese alumno";
                return resultado;
            }

            if (comando.Descripcion.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese descirpcion";
                return resultado;
            }

            var rutina = new Rutina();
            rutina.IdDisciplina = comando.IdDisciplina;
            rutina.IdAlumno = comando.IdAlumno;
            rutina.Descripcion = comando.Descripcion;

            db.Rutinas.Add(rutina);
            db.SaveChanges();

            foreach (var item in comando.RutinasEjercicios)
            {
                var ejer = new RutinasEjercicio();
                ejer.IdRutina = rutina.IdRutina;
                ejer.IdEjercicio = item.IdEjercicio;
                ejer.Repeticiones = item.Repeticiones;
                db.RutinasEjercicios.Add(ejer);
            }
            db.SaveChanges();

            resultado.Ok = true;
            resultado.Return = db.Rutinas.ToList();
            //resultado.Return = rutina.IdRutina;


            return resultado;
        }

        [HttpPost]
        [Route("[controller]/AltaEjerXRutina")]
        public ActionResult<ResultAPI> AltaEjerXRutina([FromBody] comandoCrearEjerXRutina comando)
        {
            var resultado = new ResultAPI();

            if (comando.IdRutina.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese disciplina";
                return resultado;
            }
            if (comando.IdEjercicio.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese alumno";
                return resultado;
            }

            if (comando.Repeticiones.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese descirpcion";
                return resultado;
            }

            var ejerXrutina = new RutinasEjercicio();
            ejerXrutina.IdRutina = comando.IdRutina;
            ejerXrutina.IdEjercicio = comando.IdEjercicio;
            ejerXrutina.Repeticiones = comando.Repeticiones;

            db.RutinasEjercicios.Add(ejerXrutina);
            db.SaveChanges();

            resultado.Ok = true;
            resultado.Return = db.RutinasEjercicios.ToList();

            return resultado;
        }
    }
}
