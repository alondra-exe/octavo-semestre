﻿using System;
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
        public IActionResult Get()
        {
            AlumnosRepository r = new AlumnosRepository(Context);
            return Ok(r.GetAll().Select(x => new { x.Id, x.Nombre, x.Apellido, x.Correo, x.Contrasena, x.IdDocente }));
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
                    alumno.Id = 0;
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
        public IActionResult Put([FromBody] Alumno alumno)
        {
            try
            {
                AlumnosRepository r = new AlumnosRepository(Context);
                if (r.IsValid(alumno, out List<string> errores))
                {
                    var obj = r.Get(alumno.Id);
                    if (obj == null)
                        return NotFound();
                    obj.Nombre = alumno.Nombre;
                    obj.Apellido = alumno.Apellido;
                    obj.Contrasena = alumno.Contrasena;
                    r.Update(obj);
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