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
    public class RutinaController : ControllerBase //hereda de controllerbase
    {

        private readonly vitamove2Context db = new vitamove2Context();
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

        [HttpPost] //nosotros ingresamos los datos
        [Route("[controller]/AltaRutina")]
        public ActionResult<ResultAPI> AltaClase([FromBody] comandoCrearClase comando)
        {
            var resultado = new ResultAPI();

            if (comando.IdSucursal.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese sucursal";
                return resultado;
            }
            if (comando.IdProfesor.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese profesor a cargo";
                return resultado;
            }
            if (comando.Cupo.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese cupo";
                return resultado;
            }
            if (comando.DiaSemana.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese día de semana";
                return resultado;
            }
            if (comando.HoraDesde.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese desde que hora";
                return resultado;
            }
            if (comando.HoraHasta.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese hasta que hora";
                return resultado;
            }


            var clase = new Clase();
            clase.IdDisciplina = comando.IdDisciplina;
            clase.IdSucursal = comando.IdSucursal;
            clase.IdProfesor = comando.IdProfesor;
            clase.Cupo = comando.Cupo;
            clase.DiaSemana = comando.DiaSemana;
            clase.HoraDesde = comando.HoraDesde;
            clase.HoraHasta = comando.HoraHasta;


            db.Clases.Add(clase);
            db.SaveChanges(); //siempre despues de un insert, update etc hacer el SaveChanges()

            resultado.Ok = true;
            resultado.Return = db.Clases.ToList();

            return resultado;
        }

        [HttpPut]
        [Route("[controller]/UpdateClase")]
        public ActionResult<ResultAPI> UpdateClase([FromBody] comandoUpdateClase comando)
        {
            var resultado = new ResultAPI();
            if (comando.IdDisciplina.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese disciplina";
                return resultado;
            }
            if (comando.IdSucursal.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese sucursal";
                return resultado;
            }
            if (comando.IdProfesor.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese profesor a cargo";
                return resultado;
            }
            if (comando.Cupo.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese cupo";
                return resultado;
            }
            if (comando.DiaSemana.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese día de semana";
                return resultado;
            }
            if (comando.HoraDesde.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese desde que hora";
                return resultado;
            }
            if (comando.HoraHasta.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese hasta que hora";
                return resultado;
            }

            var clase = db.Clases.Where(c => c.IdClase == comando.IdClase).FirstOrDefault();
            if (clase != null)
            {
                clase.IdDisciplina = comando.IdDisciplina;
                clase.IdSucursal = comando.IdSucursal;
                clase.IdProfesor = comando.IdProfesor;
                clase.Cupo = comando.Cupo;
                clase.DiaSemana = comando.DiaSemana;
                clase.HoraDesde = comando.HoraDesde;
                clase.HoraHasta = comando.HoraHasta;
                db.Clases.Update(clase);
                db.SaveChanges();
            }

            resultado.Ok = true;
            resultado.Return = db.Clases.ToList();

            return resultado;
        }

    }
}
