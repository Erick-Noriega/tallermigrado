namespace tallermigrado.Models
{
    public class Operaciones
    {
        public int Id { get; set; }
        public string NombreOperacion { get; set; } = string.Empty;
        public int? prendasId { get; set; }
        public Prendas? Prendas { get; set; }
        public List<OperacionesEmpresa> OperacionesEmpresas { get; set; } = new List<OperacionesEmpresa>();
        public List<ProduccionEmpleados> produempleado { get; set; } = new List<ProduccionEmpleados>();

    }
}
