﻿using proyecto1_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto1_api.Repositories
{
    public class AlumnosRepository : Repository<Alumno>
    {
        public AlumnosRepository(sistem14_proyecto1_alondra_jesmeContext context)
        {
            Context = context;
        }

        public override bool IsValid(Alumno entity, out List<string> errors)
        {
            errors = new List<string>();
            if (string.IsNullOrWhiteSpace(entity.Nombre))
            {
                errors.Add("El nombre del alumno no puede estar vacío.");
            }
            if (string.IsNullOrWhiteSpace(entity.Apellido))
            {
                errors.Add("El apellido del alumno no puede estar vacío.");
            }
            if (string.IsNullOrWhiteSpace(entity.Correo))
            {
                errors.Add("El correo electrónico del alumno no puede estar vacío.");
            }
            if (string.IsNullOrWhiteSpace(entity.Contrasena))
            {
                errors.Add("Debe asignarla une contraseña al alumno.");
            }
            if (Context.Alumno.Any(x => x.Correo == entity.Correo && x.Eliminado == 0))
            {
                errors.Add("Este correo electrónico ya está en uso.");
            }
            return errors.Count == 0;
        }
    }
}
