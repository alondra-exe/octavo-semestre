using System;
using System.Collections.Generic;
using System.Text;

namespace mijardin.Models
{
    public partial class Registro
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Cientifico { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
    }
}
