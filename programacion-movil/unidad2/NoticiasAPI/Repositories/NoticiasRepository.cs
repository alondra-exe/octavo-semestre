using Microsoft.EntityFrameworkCore;
using NoticiasAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoticiasAPI.Repositories
{
    public class NoticiasRepository:Repository<Noticia>
    {
        public NoticiasRepository(sistem14_noticiasaloContext context)
        {
            Context = context;
        }

        public override Noticia Get(object id)
        {
            return Context.Noticia.FirstOrDefault(x => x.Id == (int)id && x.Eliminado == 1);
        }

        public override IEnumerable<Noticia> GetAll()
        {
            return Context.Noticia.Where(x => x.Eliminado == 1).OrderBy(x => x.Fecha);
        }

        public override void Delete(Noticia entity)
        {
            if (Context.Noticia.Any(x => x.Id == entity.Id))
            {
                entity.Eliminado = 0;
                Update(entity);
            }
            else
                base.Delete(entity);
        }

        public override bool IsValid(Noticia noticia, out List<string> errors)
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
