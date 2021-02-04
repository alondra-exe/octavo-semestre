﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pokemon_api.Models;

namespace pokemon_api.Repositories
{
    public class PokemonRepository
    {
        public sistem14_pokemonContext Context { get; set; }
        public PokemonRepository(sistem14_pokemonContext context)
        {
            Context = context;
        }
        public IEnumerable<Pokemon> GetList(int pagina)
        {
            return ;
        }

        public Pokemon GetPokemon(int id)
        {
            return Context.Pokemon.FirstOrDefault(x => x.Id == id);
        }
    }
}