using System;
using System.Collections.Generic;
using System.Text;

namespace juego.Models
{
    public partial class Docente
    {
        public Docente()
        {
            Alumno = new HashSet<Alumno>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public int Clave { get; set; }
        public string Contrasena { get; set; }

        public virtual ICollection<Alumno> Alumno { get; set; }
    }
}
