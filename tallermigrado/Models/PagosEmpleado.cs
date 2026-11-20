namespace tallermigrado.Models
{
    public class PagosEmpleado
    {
        public int Id { get; set; }
        public string Mes { get; set; } = string.Empty;
        public bool Pagado { get; set; }
        public double total { get; set; }
        public Empleados? empleados { get; set; }
        public string empleadosId { get; set; } = string.Empty;
        public List<ProduccionEmpleados> Registros { get; set; } = new List<ProduccionEmpleados>();


    }
}
