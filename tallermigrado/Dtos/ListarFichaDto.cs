namespace tallermigrado.Dtos
{
    public class ListarFichaDto
    {
        public int Id { get; set; }
        public string ModeloPrenda { get; set; } = string.Empty;
        public string NumeroDeCorte { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public string Talle { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Empresa { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string ImagenUrl { get; set; } = string.Empty;
    }
}
