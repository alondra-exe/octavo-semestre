using System;
using System.Collections.Generic;
using System.Text;

namespace juego.Models
{
    public partial class Progreso
    {
        public int Id { get; set; }
        public int Puntuacion { get; set; }
        public int Intentos { get; set; }
        public DateTime Fecha { get; set; }
        public int IdAlumno { get; set; }

        public virtual Alumno IdAlumnoNavigation { get; set; }
    }
}
