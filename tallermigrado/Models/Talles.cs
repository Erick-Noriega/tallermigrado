namespace tallermigrado.Models
{
    public class Talles
    {
        public int Id { get; set; }
        public string DescripcionTalle { get; set; } = string.Empty;
        public List<Ficha> fichas { get; set; } = new List<Ficha>();
    }
}
