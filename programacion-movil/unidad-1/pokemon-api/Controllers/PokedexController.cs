using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace pokemon_api.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class pokedexController : ControllerBase
    {
        // Regresa el número, nombre, tipo e imagen del pokémon.
        [HttpGet]
        public IActionResult GetPokemonList(int id)
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetPokemonData(int id)
        {
            return Ok();
        }
    }
}