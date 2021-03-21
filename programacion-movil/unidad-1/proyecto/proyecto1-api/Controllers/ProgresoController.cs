using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyecto1_api.Models;
using proyecto1_api.Repositories;

namespace proyecto1_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgresoController : ControllerBase
    {
        public sistem14_proyecto1_alondra_jesmeContext Context { get; }

        public ProgresoController(sistem14_proyecto1_alondra_jesmeContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            ProgresoRepository r = new ProgresoRepository(Context);
            var datos = r.GetAll().Select(x => new { x.Id, x.Puntuacion, x.Intentos, x.Fecha, x.IdAlumno });
            return Ok(datos);
        }

        [HttpGet("{id}")]
        public IActionResult GetProgresoAlumno(int id)
        {
            try
            {
                ProgresoRepository r = new ProgresoRepository(Context);
                var datos = r.GetAll().
                    Where(x => x.IdAlumno == id).
                    Select(x => new { x.Id, x.Puntuacion, x.Intentos, x.Fecha, x.IdAlumno });
                return Ok(datos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Progreso progreso)
        {
            try
            {
                ProgresoRepository r = new ProgresoRepository(Context);
                if (r.IsValid(progreso, out List<string> errores))
                {
                    progreso.Id = 0;
                    Progreso p = new Progreso
                    {
                        Puntuacion = progreso.Puntuacion,
                        Intentos = progreso.Intentos,
                        Fecha = DateTime.Now,
                        IdAlumno = progreso.IdAlumno
                    };
                    r.Insert(progreso);
                    return Ok(progreso);
                }
                else
                    return BadRequest(errores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}