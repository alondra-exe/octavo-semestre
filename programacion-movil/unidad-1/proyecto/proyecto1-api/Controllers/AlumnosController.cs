﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyecto1_api.Models;
using proyecto1_api.Repositories;
using proyecto1_api.Helpers;
using proyecto1_api.DataModels;

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
        public IActionResult Get()
        {
            AlumnosRepository r = new AlumnosRepository(Context);
            var datos = (r.GetAll().Select(x => new { x.Id, x.Nombre, x.Apellido, x.Correo, x.IdDocente }));
            return Ok(datos);
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
                    return Ok(new { alumno.Id, alumno.Nombre, alumno.Apellido, alumno.Correo, alumno.IdDocente });
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
                    alumno.Contrasena = System.Text.Encoding.Unicode.GetString(
                        Convert.FromBase64String(alumno.Contrasena));
                    alumno.Id = 0;
                    Alumno a = new Alumno
                    {
                        Nombre = alumno.Nombre,
                        Apellido = alumno.Apellido,
                        Correo = alumno.Correo,
                        Contrasena = HashHelper.GetHash(alumno.Contrasena + alumno.Correo),
                        IdDocente = alumno.IdDocente,
                        Eliminado = 0
                    };
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

        [HttpPut]
        public IActionResult Put([FromBody] Alumno a)
        {
            try
            {
                AlumnosRepository r = new AlumnosRepository(Context);
                var alumno = r.Get(a.Id);
                if (alumno == null)
                {
                    return NotFound();
                }
                if (r.IsValid(a, out List<string> errores))
                {
                    alumno.Nombre = a.Nombre;
                    alumno.Apellido = a.Apellido;
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

        [HttpPut("cambio")]
        public IActionResult Cambiar([FromBody] CambioDataModel c)
        {
            AlumnosRepository r = new AlumnosRepository(Context);
            var alumno = r.Get(c.Id);
            var anterior = System.Text.Encoding.Unicode.GetString
                (Convert.FromBase64String(c.ContraA));
            if (alumno == null)
            {
                return NotFound();
            }
            if (alumno.Contrasena == HashHelper.GetHash(anterior + alumno.Correo))
            {
                var nuevo = System.Text.Encoding.Unicode.GetString(Convert.FromBase64String(c.ContraN));
                alumno.Contrasena = HashHelper.GetHash(nuevo + alumno.Correo);
                r.Update(alumno);
                return Ok();
            }
            else
                return Forbid();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Alumno a)
        {
            if (string.IsNullOrWhiteSpace(a.Correo))
            {
                return BadRequest("Necesitas escribir tu correo electrónico y contraseña.");
            }
            if (string.IsNullOrWhiteSpace(a.Contrasena))
            {
                return BadRequest("Necesitas escribir tu correo electrónico y contraseña.");
            }
            AlumnosRepository r = new AlumnosRepository(Context);
            var contra = System.Text.Encoding.Unicode.GetString(Convert.FromBase64String(a.Contrasena));
            var alumno = r.Get(HashHelper.GetHash(contra + a.Correo));
            return Ok(new { alumno.Id, alumno.Nombre, alumno.Apellido, alumno.Correo });
        }


        [HttpDelete("{id}")]
        private IActionResult Delete(int id)
        {
            AlumnosRepository r = new AlumnosRepository(Context);
            var alumno = r.Get(id);
            if (alumno == null)
            {
                return NotFound();
            }
            else
            {
                r.Delete(alumno);
                return Ok();
            }
        }

        [HttpDelete]
        private IActionResult Delete([FromBody] Alumno a)
        {
            AlumnosRepository r = new AlumnosRepository(Context);
            var alumno = r.Get(a.Id);
            if (alumno == null)
            {
                return NotFound();
            }
            else
            {
                r.Delete(alumno);
                return Ok();
            }
        }
    }
}