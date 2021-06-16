using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using plantasapi.Models;

namespace plantasapi.Repositories
{
    public class RegistroRepository:Repository<Registro>
    {
        public RegistroRepository(sistem14_plantas_aedsContext context)
        {
            Context = context;
        }

        public override Registro Get(object id)
        {
            return Context.Registros.FirstOrDefault(x => x.Id == (int)id);
        }

        public override IEnumerable<Registro> GetAll()
        {
            return Context.Registros.OrderBy(x => x.Nombre);
        }
    }
}
