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

        // GET: api/Progreso/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Progreso>> GetProgreso(int id)
        {
            var progreso = await Context.Progreso.FindAsync(id);

            if (progreso == null)
            {
                return NotFound();
            }

            return progreso;
        }

        // PUT: api/Progreso/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProgreso(int id, Progreso progreso)
        {
            if (id != progreso.Id)
            {
                return BadRequest();
            }

            Context.Entry(progreso).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgresoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Progreso
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Progreso>> PostProgreso(Progreso progreso)
        {
            Context.Progreso.Add(progreso);
            await Context.SaveChangesAsync();

            return CreatedAtAction("GetProgreso", new { id = progreso.Id }, progreso);
        }

        // DELETE: api/Progreso/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Progreso>> DeleteProgreso(int id)
        {
            var progreso = await Context.Progreso.FindAsync(id);
            if (progreso == null)
            {
                return NotFound();
            }

            Context.Progreso.Remove(progreso);
            await Context.SaveChangesAsync();

            return progreso;
        }

        private bool ProgresoExists(int id)
        {
            return Context.Progreso.Any(e => e.Id == id);
        }
    }
}
