using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using tallermigrado.Data;
using tallermigrado.Models;
using tallermigrado.Repository;
using tallermigrado.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var conexion = builder.Configuration.GetConnectionString("Conexion");

builder.Services.AddDbContext<TallerDbContext>(options =>
    options.UseSqlServer(conexion));
var builderConexion = new SqlConnectionStringBuilder(conexion);

builder.Services.AddIdentity<Empleados, IdentityRole>()
    .AddEntityFrameworkStores<TallerDbContext>()
    .AddDefaultTokenProviders();

// Repository Dependency Injections
builder.Services.AddScoped<IPrendasRepository, PrendasRepository>();
builder.Services.AddScoped<IColoresRepository, ColoresRepository>();
builder.Services.AddScoped<IEmpresasRepository, EmpresasRepository>();
builder.Services.AddScoped<IOperacionesRepository, OperacionesRepository>();
builder.Services.AddScoped<ITallesRepository, TallesRepository>();
builder.Services.AddScoped<IFichaRepository, FichaRepository>();
builder.Services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();
builder.Services.AddScoped<IProduccionEmpleadoRepository, ProduccionEmpleadoRepository>();
builder.Services.AddScoped<IOperacionesEmpresaRepository, OperacionesEmpresaRepository>();
builder.Services.AddScoped<IPagosEmpleadoRepository, PagosEmpleadoRepository>();

// Service Dependency Injections
builder.Services.AddScoped<IprendasService, Prendas_Service>();
builder.Services.AddScoped<IColoresService, ColoresService>();
builder.Services.AddScoped<IEmpresasService, EmpresasService>();
builder.Services.AddScoped<IOperacionesService, OperacionesService>();
builder.Services.AddScoped<ITallesService, TallesService>();
builder.Services.AddScoped<IFichaService, FichaService>();
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();
builder.Services.AddScoped<IProduccionEmpleadoService, ProduccionEmpleadoService>();
builder.Services.AddScoped<IOperacionesEmpresaService, OperacionesEmpresaService>();
builder.Services.AddScoped<IPagosEmpleadoService, PagosEmpleadoService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    foreach (var rol in new[] { "Admin", "Empleado" })
    {
        if (!await roleManager.RoleExistsAsync(rol))
        {
            await roleManager.CreateAsync(new IdentityRole(rol));
        }
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
