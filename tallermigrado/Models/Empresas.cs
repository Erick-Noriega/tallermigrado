namespace tallermigrado.Models
{
    public class Empresas
    {
        public int Id { get; set; }
        public string NombreEmpresa { get; set; } = string.Empty;
        public List<OperacionesEmpresa> OperacionesEmpresas { get; set; } = new List<OperacionesEmpresa>();
        public List<Ficha> fichas { get; set; } = new List<Ficha>();

    }
}
