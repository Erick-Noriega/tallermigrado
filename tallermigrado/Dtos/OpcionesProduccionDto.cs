namespace tallermigrado.Dtos
{
    public class OpcionesProduccionDto
    {
        public List<OpcionDto> Prendas { get; set; } = new List<OpcionDto>();
        public List<OpcionDto> Empresas { get; set; } = new List<OpcionDto>();
        public List<OpcionDto> Colores { get; set; } = new List<OpcionDto>();
        public List<OpcionDto> Talles { get; set; } = new List<OpcionDto>();
        public List<OpcionDto> Cortes { get; set; } = new List<OpcionDto>();
        public List<OpcionDto> Operaciones { get; set; } = new List<OpcionDto>();
        public List<EmpleadoOpcionDto> Empleados { get; set; } = new List<EmpleadoOpcionDto>();
        public int? Cantidad { get; set; }
        public double? Precio { get; set; }
    }

    public class OpcionDto
    {
        public int Id { get; set; }
        public string Texto { get; set; } = string.Empty;
    }

    public class EmpleadoOpcionDto
    {
        public string Id { get; set; } = string.Empty;
        public string Texto { get; set; } = string.Empty;
    }
}
