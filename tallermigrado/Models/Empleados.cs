using Microsoft.AspNetCore.Identity;

namespace tallermigrado.Models
{
    public class Empleados : IdentityUser
    {
        public string nombre { get; set; } = string.Empty;
        public List<ProduccionEmpleados> produempleado { get; set; } = new List<ProduccionEmpleados>();
        public List<PagosEmpleado> pagosempleado { get; set; } = new List<PagosEmpleado>();

    }
}
