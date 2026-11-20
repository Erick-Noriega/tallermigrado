namespace tallermigrado.Dtos
{
    public class CrearProduccionEmpleadoDto
    {
        public string EmpleadoId { get; set; } = string.Empty;
        public int PrendasId { get; set; }
        public int EmpresasId { get; set; }
        public int ColorId { get; set; }
        public int TalleId { get; set; }
        public string Corte { get; set; } = string.Empty;
        public int OperacionId { get; set; }
        public DateTime Fecha { get; set; }
    }
}
