using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace pokemon_api.Models
{
    public partial class Pokemon
    {
        public Pokemon()
        {
            InverseEvolucionNavigation = new HashSet<Pokemon>();
            InversePreevolucionNavigation = new HashSet<Pokemon>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public float? Altura { get; set; }
        public float? Peso { get; set; }
        public string Categoria { get; set; }
        public string Abilidades { get; set; }
        public string Tipo1 { get; set; }
        public string Tipo2 { get; set; }
        public int? Preevolucion { get; set; }
        public int? Evolucion { get; set; }
        public string Imagen1 { get; set; }
        public string Imagen2 { get; set; }

        public virtual Pokemon EvolucionNavigation { get; set; }
        public virtual Pokemon PreevolucionNavigation { get; set; }
        public virtual ICollection<Pokemon> InverseEvolucionNavigation { get; set; }
        public virtual ICollection<Pokemon> InversePreevolucionNavigation { get; set; }
    }
}
