using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using proyecto1_api.Models;

namespace proyecto1_api.Repositories
{
    public class ProgresoRepository : Repository<Progreso>
    {
        public ProgresoRepository(sistem14_proyecto1_alondra_jesmeContext context)
        {
            Context = context;
        }

        public override Progreso Get(object id)
        {
            return Context.Progreso.FirstOrDefault(x => x.Id == (int)id);
        }

        public override IEnumerable<Progreso> GetAll()
        {
            return Context.Progreso.Include(x => x.IdAlumnoNavigation).OrderByDescending(x => x.IdAlumno);
        }
    }
}