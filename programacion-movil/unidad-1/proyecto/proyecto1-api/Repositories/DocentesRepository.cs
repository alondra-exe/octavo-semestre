using proyecto1_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto1_api.Repositories
{
    public class DocentesRepository: Repository<Docente>
    {
        public DocentesRepository(sistem14_proyecto1_alondra_jesmeContext context)
        {
            Context = context;
        }

        public override Docente Get(object id)
        {
            return Context.Docente.FirstOrDefault(x => x.Id == (int)id);
        }

        public override IEnumerable<Docente> GetAll()
        {
            return Context.Docente.OrderBy(x => x.Id);
        }

        public Docente Get(string contra)
        {
            return Context.Docente.FirstOrDefault(x => x.Contrasena == contra);
        }

        public override bool IsValid(Docente entity, out List<string> errors)
        {
            errors = new List<string>();
            if (string.IsNullOrWhiteSpace(entity.Nombre))
            {
                errors.Add("El nombre del docente no puede estar vacío.");
            }
            if (string.IsNullOrWhiteSpace(entity.Apellido))
            {
                errors.Add("El apellido del docente no puede estar vacío.");
            }
            if (string.IsNullOrWhiteSpace(entity.Correo))
            {
                errors.Add("El correo electrónico del docente no puede estar vacío.");
            }
            if (string.IsNullOrWhiteSpace(entity.Contrasena))
            {
                errors.Add("Debe asignarla une contraseña al docente.");
            }
            if (Context.Alumno.Any(x => x.Correo.ToLower() == entity.Correo.ToLower()))
            {
                errors.Add("Este correo electrónico ya está en uso.");
            }
            return errors.Count == 0;
        }
    }
}
