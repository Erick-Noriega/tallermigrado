namespace tallermigrado.Models
{
    public class Colores
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public List<Ficha> fichas { get; set; } = new List<Ficha>();
        public List<ProduccionEmpleados> produempleado { get; set; } = new List<ProduccionEmpleados>();

    }
}
