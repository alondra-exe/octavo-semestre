using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using plantasapi.Models;
using plantasapi.Repositories;

namespace plantasapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroController : ControllerBase
    {
        public sistem14_plantas_aedsContext Context { get; }

        public RegistroController(sistem14_plantas_aedsContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            RegistroRepository r = new RegistroRepository(Context);
            var datos = (r.GetAll().Select(x => new { x.Id, x.Nombre, x.Cientifico, x.Descripcion, x.Imagen }));
            return Ok(datos);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                RegistroRepository r = new RegistroRepository(Context);
                var registro = r.Get(id);
                if (registro == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(new { registro.Id, registro.Nombre, registro.Cientifico, registro.Descripcion, registro.Imagen });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
