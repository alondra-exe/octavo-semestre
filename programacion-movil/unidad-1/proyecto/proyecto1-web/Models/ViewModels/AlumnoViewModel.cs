using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto1_web.Models.ViewModels
{
    public class AlumnoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public int IdDocente { get; set; }
        public ulong Eliminado { get; set; }

        public Progreso P { get; set; }
    }
}
