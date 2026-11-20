using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using tallermigrado.Models;

namespace tallermigrado.Data
{
    public class TallerDbContext:IdentityDbContext<Empleados>
       
    {
        public TallerDbContext(DbContextOptions<TallerDbContext> options) : base(options)
        {
        }
        public DbSet<ProduccionEmpleados> produempleado { get; set; }
        public DbSet<PagosEmpleado> pagosempleado { get; set; }
        public DbSet<Operaciones> operaciones { get; set; }
        public DbSet<OperacionesEmpresa> operacionesempresa { get; set; }
        public DbSet<Colores> colores { get; set; }
        public DbSet<Ficha> fichas { get; set; }
        public DbSet<Talles> talles { get; set; }
        public DbSet<Prendas> prendas { get; set; }
        public DbSet<Empresas> empresas { get; set; }


    }
}
