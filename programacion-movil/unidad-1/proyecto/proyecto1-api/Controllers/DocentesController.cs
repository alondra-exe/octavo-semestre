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
    }
}
