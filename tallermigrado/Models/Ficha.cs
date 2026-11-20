namespace tallermigrado.Models
{
    public class Ficha
    {
        public int Id { get; set; }
        public Talles? talle { get; set; }
        public int talleId { get; set; }
        public Colores? Color { get; set; }
        public int colorId { get; set; }
        public Prendas? prendas { get; set; }
        public int prendasID { get; set; }
        public Empresas? Empresas { get; set; }
        public int EmpresasId { get; set; }
        public string ImagenUrl { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string NumerodeCorte { get; set; } = string.Empty;
        public int cantidad { get; set; }
      




    }
}
