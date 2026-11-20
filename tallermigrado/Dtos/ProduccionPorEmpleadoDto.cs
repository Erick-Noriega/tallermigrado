namespace tallermigrado.Dtos
{
    public class ProduccionPorEmpleadoDto
    {
        public string EmpleadoId { get; set; } = string.Empty;
        public string NombreEmpleado { get; set; } = string.Empty;
        public List<ProduccionItemDto> Registros { get; set; } = new List<ProduccionItemDto>();
    }

    public class ProduccionItemDto
    {
        public int Id { get; set; }
        public string Operacion { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Talle { get; set; } = string.Empty;
        public string Corte { get; set; } = string.Empty;
        public string Empresa { get; set; } = string.Empty;
        public string Prenda { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public double? Precio { get; set; }
        public DateTime Fecha { get; set; }
    }
}
