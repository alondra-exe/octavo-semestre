using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pokemon_api.models;

namespace pokemon_api.repositories
{
    public class pokemonRepository
    {
        public sistem14_pokemonContext Context { get; set; }
        public pokemonRepository(sistem14_pokemonContext context)
        {
            Context = context;
        }
        const int tamaño = 50;
        public IEnumerable<Pokemon> GetList(int pagina)
        {
            int inf = tamaño * (pagina - 1) + 1;
            //return Context.Pokemon.Skip(inf).Take(tamaño);
            return Context.Pokemon.Where(x => x.Id >= inf && x.Id < inf + tamaño);
        }

        public Pokemon GetPokemon(int id)
        {
            return Context.Pokemon.FirstOrDefault(x => x.Id == id);
        }
    }
}