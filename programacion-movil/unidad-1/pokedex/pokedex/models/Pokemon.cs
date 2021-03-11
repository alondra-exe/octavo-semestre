namespace pokedex.models
{
    public partial class Pokemon
    {
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
    }
}