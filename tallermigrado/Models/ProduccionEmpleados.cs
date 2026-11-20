namespace tallermigrado.Models
{
    public class ProduccionEmpleados
    {
        public int Id { get; set; }
        public Empleados? empleados { get; set; }
        public string empleadoId { get; set; } = string.Empty;
        public Operaciones? Operaciones { get; set; }
        public int operacionesId { get; set; }
        public Colores? color { get; set; }
        public int coloresId { get; set; }
        public Talles? talle { get; set; }
        public int talleId { get; set; }
        public int? prendasId { get; set; }
        public Prendas? Prendas { get; set; }
        public int? empresasId { get; set; }
        public Empresas? Empresas { get; set; }
        public string corte { get; set; } = string.Empty;

        public int CantidadDePrendas { get; set; }
        public DateTime Fecha { get; set; }
        public double? precio { get; set; }
        public int? pagosEmpleadoId { get; set; }
        public PagosEmpleado? PagoEmpleado { get; set; }
       



    }
}
