using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace proyecto1_api.Models
{
    public partial class Alumno
    {
        public Alumno()
        {
            Progreso = new HashSet<Progreso>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public int IdDocente { get; set; }
        public ulong Eliminado { get; set; }

        public virtual Docente IdDocenteNavigation { get; set; }
        public virtual ICollection<Progreso> Progreso { get; set; }
    }
}
