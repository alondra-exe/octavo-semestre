﻿using System;
using System.Collections.Generic;


namespace NoticiasWEB.Models
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
