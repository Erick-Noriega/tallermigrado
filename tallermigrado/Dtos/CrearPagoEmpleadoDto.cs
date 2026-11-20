using System.ComponentModel.DataAnnotations;

namespace tallermigrado.Dtos
{
    public class CrearPagoEmpleadoDto
    {
        [Required(ErrorMessage = "Debe indicar el mes del pago.")]
        public string Mes { get; set; } = string.Empty;

        public List<int> ProduccionIds { get; set; } = new List<int>();
    }
}
