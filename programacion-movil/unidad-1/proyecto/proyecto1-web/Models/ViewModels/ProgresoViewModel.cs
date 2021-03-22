using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto1_web.Models.ViewModels
{
    public class ProgresoViewModel
    {
        public int Id { get; set; }
        public int Puntuacion { get; set; }
        public int Intentos { get; set; }
        public DateTime Fecha { get; set; }
    }
}
