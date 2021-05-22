using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoticiasAPI.Models;
using NoticiasAPI.Repositories;

namespace NoticiasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticiasController : ControllerBase
    {
        public sistem14_noticiasaloContext Context { get; }
        public NoticiasController(sistem14_noticiasaloContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            Repository repository = new Repository(Context);
            var noticias = (repository.GetAll().Select(x => new { x.Id, x.Encabezado, x.Autor, x.Lugar, x.Fecha, x.Contenido }));
            return Ok(noticias);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Repository repository = new Repository(Context);
            try
            {
                var noticia = repository.Get(id);
                if (noticia == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(new { noticia.Id, noticia.Encabezado, noticia.Autor, noticia.Lugar, noticia.Fecha, noticia.Contenido });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Noticia noticia)
        {
            Repository repository = new Repository(Context);
            try
            {
                if (repository.IsValid(noticia, out List<string> errores))
                {
                    Noticia n = new Noticia
                    {
                        Encabezado = noticia.Encabezado,
                        Autor = noticia.Autor,
                        Lugar = noticia.Lugar,
                        Fecha = DateTime.Now,
                        Contenido = noticia.Contenido,
                        Eliminado = 1
                    };
                    repository.Insert(n);
                    return Ok();
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
        public IActionResult Put([FromBody] Noticia n)
        {
            try
            {
                Repository repository = new Repository(Context);
                var noticia = repository.Get(n.Id);
                if (noticia == null)
                {
                    return NotFound();
                }
                if (noticia.Eliminado == 0)
                {
                    return BadRequest("No se encontró ninguna noticia :(");
                }
                if (string.IsNullOrEmpty(noticia.Encabezado))
                {
                    return BadRequest("Escriba el encabezado de la noticia.");
                }
                if (string.IsNullOrEmpty(noticia.Contenido))
                {
                    return BadRequest("Escriba el contenido de la noticia.");
                }
                noticia.Encabezado = n.Encabezado;
                noticia.Fecha = DateTime.Now;
                noticia.Contenido = n.Contenido;
                repository.Update(noticia);
                return Ok(noticia);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] Noticia n)
        {
            Repository repository = new Repository(Context);
            var noticia = repository.Get(n.Id);
            if (noticia == null)
            {
                return NotFound();
            }
            else
            {
                repository.Delete(noticia);
                return Ok();
            }
        }
    }
}