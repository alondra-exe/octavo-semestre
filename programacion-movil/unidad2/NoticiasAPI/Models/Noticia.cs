using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace NoticiasAPI.Models
{
    public partial class Noticia
    {
        public int Id { get; set; }
        public string Encabezado { get; set; }
        public string Autor { get; set; }
        public string Lugar { get; set; }
        public DateTime Fecha { get; set; }
        public string Contenido { get; set; }
        public ulong Eliminado { get; set; }
    }
}
