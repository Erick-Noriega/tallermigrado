namespace tallermigrado.Models
{
    public class Prendas
    {
        public int Id { get; set; }
        public string ModeloPrenda { get; set; }=string.Empty;
        public List<OperacionesEmpresa> OperacionesEmpresas { get; set; } = new List<OperacionesEmpresa>();
        public List<Ficha> fichas { get; set; } = new List<Ficha>();


    }
}
