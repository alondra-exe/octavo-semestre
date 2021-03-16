using proyecto1_api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto1_api.Repositories
{
    public abstract class Repository<T> where T : class
    {
        public sistem14_proyecto1_alondra_jesmeContext Context { get; set; }
        public virtual T Get(object id)
        {
            return Context.Find<T>(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Context.Set<T>();
        }

        public virtual void Insert(T entity)
        {
            Context.Add(entity);
            Context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            Context.Update(entity);
            Context.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            Context.Remove(entity);
            Context.SaveChanges();
        }

        public virtual bool IsValid(T entity, out List<string> errors)
        {
            errors = new List<string>();
            return true;
        }
    }
}