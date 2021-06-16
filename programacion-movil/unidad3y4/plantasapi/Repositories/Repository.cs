using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using plantasapi.Models;

namespace plantasapi.Repositories
{
    public class Repository<T> where T : class
    {
        public sistem14_plantas_aedsContext Context { get; set; }

        public virtual T Get(object id)
        {
            return Context.Find<T>(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Context.Set<T>();
        }

        public virtual bool IsValid(T entity, out List<string> errors)
        {
            errors = new List<string>();
            return true;
        }
    }
}
