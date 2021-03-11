using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pokemon_api.models;
using pokemon_api.repositories;

namespace pokemon_api.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class pokedexController : ControllerBase
    {
        public sistem14_pokemonContext Context { get; set; }
        public pokedexController(sistem14_pokemonContext context)
        {
            Context = context;
        }

        // Regresa el número, nombre, tipo e imagen del pokémon.
        [HttpGet("lista/{id}")]
        public IActionResult GetPokemonList(int id)
        {
            pokemonRepository repos = new pokemonRepository(Context);
            var datos = repos.GetList(id).Select(x => new Pokemon
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Tipo1 = x.Tipo1,
                Tipo2 = x.Tipo2,
                Imagen1 = x.Imagen1
            }).ToList();
            if (datos != null)
                return Ok(datos);
            else
                return NotFound();
        }

        [HttpGet("datos/{id}")]
        public IActionResult GetPokemonData(int id)
        {
            pokemonRepository repos = new pokemonRepository(Context);
            var datos = repos.GetPokemon(id);
            if (datos != null)
                return Ok(datos);
            else
                return NotFound();
        }
    }
}