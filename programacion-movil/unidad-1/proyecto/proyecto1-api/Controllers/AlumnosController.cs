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
    public class AlumnosController : ControllerBase
    {
        public sistem14_proyecto1_alondra_jesmeContext Context { get; }

        public AlumnosController(sistem14_proyecto1_alondra_jesmeContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IEnumerable<Alumno> Get()
        {
            AlumnosRepository r = new AlumnosRepository(Context);
            return r.GetAll();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                AlumnosRepository r = new AlumnosRepository(Context);
                var alumno = r.Get(id);
                if (alumno == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(alumno);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Alumno alumno)
        {
            try
            {
                AlumnosRepository r = new AlumnosRepository(Context);
                if (r.IsValid(alumno, out List<string> errores))
                {
                    r.Insert(alumno);
                    return Ok(alumno);
                }
                else
                    return BadRequest(errores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Alumno alumno)
        {
            try
            {
                AlumnosRepository r = new AlumnosRepository(Context);
                if (r.IsValid(alumno, out List<string> errores))
                {
                    r.Update(alumno);
                    return Ok(alumno);
                }
                else
                    return BadRequest(errores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private bool AlumnoExists(int id)
        {
            return Context.Alumno.Any(e => e.Id == id);
        }
    }
}
