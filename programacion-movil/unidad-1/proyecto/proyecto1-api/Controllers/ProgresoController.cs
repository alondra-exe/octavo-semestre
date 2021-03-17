using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyecto1_api.Models;

namespace proyecto1_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgresoController : ControllerBase
    {
        private readonly sistem14_proyecto1_alondra_jesmeContext _context;

        public ProgresoController(sistem14_proyecto1_alondra_jesmeContext context)
        {
            _context = context;
        }

        // GET: api/Progreso
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Progreso>>> GetProgreso()
        {
            return await _context.Progreso.ToListAsync();
        }

        // GET: api/Progreso/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Progreso>> GetProgreso(int id)
        {
            var progreso = await _context.Progreso.FindAsync(id);

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

            _context.Entry(progreso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
            _context.Progreso.Add(progreso);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProgreso", new { id = progreso.Id }, progreso);
        }

        // DELETE: api/Progreso/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Progreso>> DeleteProgreso(int id)
        {
            var progreso = await _context.Progreso.FindAsync(id);
            if (progreso == null)
            {
                return NotFound();
            }

            _context.Progreso.Remove(progreso);
            await _context.SaveChangesAsync();

            return progreso;
        }

        private bool ProgresoExists(int id)
        {
            return _context.Progreso.Any(e => e.Id == id);
        }
    }
}
