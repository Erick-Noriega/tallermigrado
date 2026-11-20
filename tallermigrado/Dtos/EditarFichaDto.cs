namespace tallermigrado.Dtos
{
    public class EditarFichaDto
    {
        public int Id { get; set; }
        public int PrendasId { get; set; }
        public int TalleId { get; set; }
        public int ColorId { get; set; }
        public int EmpresasId { get; set; }
        public string ImagenUrl { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string NumeroDeCorte { get; set; } = string.Empty;
        public string Cantidad { get; set; } = string.Empty;
    }
}
