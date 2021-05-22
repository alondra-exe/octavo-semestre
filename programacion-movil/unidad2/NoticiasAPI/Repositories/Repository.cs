using NoticiasAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoticiasAPI.Repositories
{
    public class Repository
    {
        public sistem14_noticiasaloContext Context { get; set; }
        public Repository(sistem14_noticiasaloContext context)
        {
            Context = context;
        }

        public Noticia Get(int id)
        {
            return Context.Noticia.Find(id);
        }

        public IEnumerable<Noticia> GetAll()
        {
            return Context.Noticia.OrderBy(x => x.Fecha);
        }

        public void Insert(Noticia noticia)
        {
            Context.Add(noticia);
            Context.SaveChanges();
        }

        public void Update(Noticia noticia)
        {
            Context.Update(noticia);
            Context.SaveChanges();
        }

        public void Delete(Noticia noticia)
        {
            Context.Remove(noticia);
            Context.SaveChanges();
        }

        public virtual bool IsValid(Noticia noticia, out List<string> errors)
        {
            errors = new List<string>();
            if (string.IsNullOrEmpty(noticia.Encabezado))
                errors.Add("Escriba el encabezado de la noticia.");
            if (string.IsNullOrEmpty(noticia.Autor))
                errors.Add("Escriba el nombre del autor de la noticia.");
            if (string.IsNullOrEmpty(noticia.Lugar))
                errors.Add("Escriba el lugar donde ocurre la noticia.");
            if (string.IsNullOrEmpty(noticia.Contenido))
                errors.Add("Escriba el contenido de la noticia.");
            return errors.Count == 0;
        }
    }
}