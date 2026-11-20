namespace tallermigrado.Models
{
    public class OperacionesEmpresa
    {
        public int Id { get; set; }
        public Operaciones? Operaciones { get; set; }
        public int OperacionesId { get; set; }
        public Empresas? Empresas { get; set; }
        public int EmpresasId { get; set; }

        public double  precio { get; set; }
        public Prendas? prendas { get; set; }
        public int prendasId { get; set; }

    }
}
