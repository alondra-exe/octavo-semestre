using SQLite;
using System;

namespace NoticiasMOVIL.Models
{
    [Table("Noticia")]
    public partial class Noticia
    {
        [AutoIncrement][PrimaryKey]
        public int Id { get; set; }
        [NotNull]
        public string Encabezado { get; set; }
        [NotNull]
        public string Autor { get; set; }
        [NotNull]
        public string Lugar { get; set; }
        [NotNull]
        public DateTime Fecha { get; set; }
        [NotNull]
        public string Contenido { get; set; }
    }
}