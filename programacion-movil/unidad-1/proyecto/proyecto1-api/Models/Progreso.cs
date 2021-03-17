using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace proyecto1_api.Models
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
