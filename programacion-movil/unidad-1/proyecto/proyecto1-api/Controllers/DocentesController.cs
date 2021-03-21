using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyecto1_api.Models;
using proyecto1_api.Repositories;
using proyecto1_api.Helpers;

namespace proyecto1_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocentesController : ControllerBase
    {
        public sistem14_proyecto1_alondra_jesmeContext Context { get; }

        public DocentesController(sistem14_proyecto1_alondra_jesmeContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            DocentesRepository r = new DocentesRepository(Context);
            var datos = (r.GetAll().Select(x => new { x.Id, x.Nombre, x.Apellido, x.Correo }));
            return Ok(datos);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Docente d)
        {
            if (string.IsNullOrWhiteSpace(d.Correo))
            {
                return BadRequest("Necesitas escribir tu correo electrónico y contraseña.");
            }
            if (string.IsNullOrWhiteSpace(d.Contrasena))
            {
                return BadRequest("Necesitas escribir tu correo electrónico y contraseña.");
            }
            DocentesRepository r = new DocentesRepository(Context);
            var contra = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(d.Contrasena));
            var docente = r.Get(HashHelper.GetHash(contra));
            if (docente == null)
            {
                return Unauthorized("El correo electrónico o la contrasña son incorrectos.");
            }
            return Ok(new { docente.Id, docente.Nombre, docente.Apellido, docente.Correo });
        }
    }
}
