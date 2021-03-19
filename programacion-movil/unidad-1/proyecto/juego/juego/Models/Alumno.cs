using System;
using System.Collections.Generic;
using System.Text;

namespace juego.Models
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
