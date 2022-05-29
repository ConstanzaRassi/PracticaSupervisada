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
    public class ClaseController : ControllerBase
    {
        private readonly vitamove2Context db = new vitamove2Context();
        private readonly ILogger<ClaseController> _logger;

        public ClaseController(ILogger<ClaseController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("[controller]/ObtenerClase")]
        public ActionResult<ResultAPI> Get()
        {
            var resultado = new ResultAPI();
            resultado.Ok = true;
            resultado.Return = db.Clases.Include(c => c.IdDisciplinaNavigation)
                                          .Include(c => c.IdSucursalNavigation)
                                          .Include(c => c.IdProfesorNavigation)
                                          .Where(c => c.Estado == 1)
                                          .OrderBy(c => c.HoraDesde)
                                          .ThenBy(c => c.IdDisciplina)
                                          .ToList();
            return resultado;
        }



        [HttpGet]
        [Route("[controller]/ObtenerClase/{id}")]
        public ActionResult<ResultAPI> GetById(int id)
        {
            var resultado = new ResultAPI();
            try
            {

                var clase = db.Clases.Where(c => c.IdClase == id)
                                     .Include(c => c.IdSucursalNavigation)  
                                     .FirstOrDefault();
                resultado.Ok = true;
                resultado.Return = clase;

                return resultado;
            }

            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "clase no encontrado";

                return resultado;
            }
        }

        [HttpGet]
        [Route("[controller]/ObtenerClaseByDisciplina/{id}")]
        public ActionResult<ResultAPI> GetByDisciplina(int id)
        {
            var resultado = new ResultAPI();
            try
            {
                resultado.Ok = true;
                resultado.Return = db.Clases.Where(c => c.IdDisciplina == id)
                                            .Include(c => c.IdSucursalNavigation)
                                            .Include(c => c.IdProfesorNavigation)
                                            .OrderBy(c => c.HoraDesde)

                                            //.GroupBy(c=>c.IdSucursal)
                                            .ToList();
                return resultado;
            }

            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "clase no encontrada";

                return resultado;
            }
        }

        [HttpGet]
        [Route("[controller]/ObtenerClasesByAlumno/{id}")]
        public ActionResult<ResultAPI> GetByAlumno(int id)
        {
            var resultado = new ResultAPI();
            try
            {
                resultado.Ok = true;
                resultado.Return = db.ClaseAlumnos.Where(c => c.IdAlumno == id)
                                            .Include(c => c.IdClaseNavigation)
                                            .ThenInclude(c => c.IdSucursalNavigation)
                                            .Include(c => c.IdClaseNavigation)
                                            .ThenInclude(c => c.IdDisciplinaNavigation)
                                            .Include(c => c.IdAlumnoNavigation)
                                            .OrderBy(c => c.IdClase)
                                            .ToList();
                return resultado;
            }

            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "clases no encontradas para este alumno";

                return resultado;
            }
        }

        [HttpDelete]
        [Route("[controller]/DeleteAlumnoDeClase")]
        public ActionResult<ResultAPI> eliminarById(int idAlumno, int idClase)
        {
            var resultado = new ResultAPI();
            var alumno = db.ClaseAlumnos.Where(c => c.IdAlumno == idAlumno && c.IdClase==idClase).FirstOrDefault();
            db.ClaseAlumnos.Remove(alumno);
            db.SaveChanges();

            resultado.Ok = true;
            resultado.Return = db.ClaseAlumnos.ToList();
            return resultado;
        }

        //[HttpGet]
        //[Route("[controller]/ObtenerDisciplinas")]
        //public ActionResult<ResultAPI> getDisciplinas()
        //{
        //    var resultado = new ResultAPI();
        //    try
        //    {
        //        resultado.Ok = true;
        //        resultado.Return = db.Disciplinas.ToList();

        //        return resultado;
        //    }
        //    catch (Exception ex)
        //    {
        //        resultado.Ok = false;
        //        resultado.Error = "Error al encontrar disciplinas";

        //        return resultado;
        //    }
        //}
        //[HttpGet]
        //[Route("[controller]/ObtenerSucursales")]
        //public ActionResult<ResultAPI> getSucursales()
        //{
        //    var resultado = new ResultAPI();
        //    try
        //    {
        //        resultado.Ok = true;
        //        resultado.Return = db.Sucursales.ToList();

        //        return resultado;
        //    }
        //    catch (Exception ex)
        //    {
        //        resultado.Ok = false;
        //        resultado.Error = "Error al encontrar sucursales";

        //        return resultado;
        //    }
        //}
        //[HttpGet]
        //[Route("[controller]/ObtenerProfesores")]
        //public ActionResult<ResultAPI> getSProfesores()
        //{
        //    var resultado = new ResultAPI();
        //    try
        //    {
        //        resultado.Ok = true;
        //        resultado.Return = db.Profesores.ToList();

        //        return resultado;
        //    }
        //    catch (Exception ex)
        //    {
        //        resultado.Ok = false;
        //        resultado.Error = "Error al encontrar profesores";

        //        return resultado;
        //    }
        //}

        [HttpPost]
        [Route("[controller]/AltaClase")]
        public ActionResult<ResultAPI> AltaClase([FromBody] comandoCrearClase comando)
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


            var clase = new Clase();
            clase.IdDisciplina = comando.IdDisciplina;
            clase.IdSucursal = comando.IdSucursal;
            clase.IdProfesor = comando.IdProfesor;
            clase.Cupo = comando.Cupo;
            clase.DiaSemana = comando.DiaSemana;
            clase.HoraDesde = comando.HoraDesde;
            clase.HoraHasta = comando.HoraHasta;
            clase.Estado = 1;


            db.Clases.Add(clase);
            db.SaveChanges();

            resultado.Ok = true;
            resultado.Return = db.Clases.ToList();

            return resultado;
        }

        [HttpGet]
        [Route("[controller]/VerificarHoraClase")]
        public ActionResult<ResultAPI> VerificarHoraClase(int id, string hora)
        {
            var resultado = new ResultAPI();
            try
            {
                resultado.Ok = true;
                resultado.Return = db.ClaseAlumnos.Where(c => c.IdAlumno == id && c.IdClaseNavigation.HoraDesde == hora).Count();
                                    
                return resultado;
            }

            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "El alumno no esta anotado a ninguna clase a esta hora";

                return resultado;
            }
        }

        [HttpPost]
        [Route("[controller]/AltaAlumnosXClase")]
        public ActionResult<ResultAPI> AltaClaseXAlumnos([FromBody] comandoCrearAlumnosXClase comando)
        {
            var resultado = new ResultAPI();
            if (comando.IdClase.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese disciplina";
                return resultado;
            }
            if (comando.IdAlumno.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese sucursal";
                return resultado;
            }

            var clase = new ClaseAlumno();
            clase.IdClase = comando.IdClase;
            clase.IdAlumno = comando.IdAlumno;
            clase.Fecha = DateTime.Now;
            clase.Estado = 1;

            db.ClaseAlumnos.Add(clase);
            db.SaveChanges();

            resultado.Ok = true;
            resultado.Return = db.ClaseAlumnos.ToList();

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
                clase.Estado = 1;
                db.Clases.Update(clase);
                db.SaveChanges();
            }

            resultado.Ok = true;
            resultado.Return = db.Clases.ToList();

            return resultado;
        }
    }
}
