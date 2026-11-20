using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using tallermigrado.Dtos;
using tallermigrado.Services;

namespace tallermigrado.Controllers
{
    [Authorize]
    public class FichaController : Controller
    {
        private readonly IFichaService fichaService;
        private readonly IprendasService prendasService;
        private readonly ITallesService tallesService;
        private readonly IColoresService coloresService;
        private readonly IEmpresasService empresasService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public FichaController(
            IFichaService fichaService,
            IprendasService prendasService,
            ITallesService tallesService,
            IColoresService coloresService,
            IEmpresasService empresasService,
            IWebHostEnvironment webHostEnvironment)
        {
            this.fichaService = fichaService;
            this.prendasService = prendasService;
            this.tallesService = tallesService;
            this.coloresService = coloresService;
            this.empresasService = empresasService;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var fichas = await fichaService.ListarFichas();
            return View(fichas);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            await CargarListas();
            var dto = new CrearFichaDto { Fecha = DateTime.Today };
            if (TempData["FichaPrevia"] is string previa)
            {
                dto = System.Text.Json.JsonSerializer.Deserialize<CrearFichaDto>(previa, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? dto;
                dto.NumeroDeCorte = string.Empty;
                dto.Cantidad = string.Empty;
                dto.ImagenUrl = string.Empty;
            }
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(CrearFichaDto dto)
        {
            if (!ModelState.IsValid)
            {
                await CargarListas();
                return View(dto);
            }

            var extensionesPermitidas = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            string? rutaArchivo = null;

            if (dto.ImagenArchivo is { Length: > 0 })
            {
                var extension = Path.GetExtension(dto.ImagenArchivo.FileName).ToLowerInvariant();
                if (!extensionesPermitidas.Contains(extension) || !dto.ImagenArchivo.ContentType.StartsWith("image/"))
                {
                    ModelState.AddModelError(nameof(dto.ImagenArchivo), "Seleccione un archivo de imagen válido (jpg, png, gif o webp).");
                    await CargarListas();
                    return View(dto);
                }

                var carpeta = Path.Combine(webHostEnvironment.WebRootPath, "uploads", "fichas");
                Directory.CreateDirectory(carpeta);

                var nombreArchivo = $"{Guid.NewGuid():N}{extension}";
                var rutaFisica = Path.Combine(carpeta, nombreArchivo);
                await using (var stream = new FileStream(rutaFisica, FileMode.Create))
                {
                    await dto.ImagenArchivo.CopyToAsync(stream);
                }

                rutaArchivo = rutaFisica;
                dto.ImagenUrl = $"/uploads/fichas/{nombreArchivo}";
            }

            try
            {
                await fichaService.CrearFicha(dto);
            }
            catch (InvalidOperationException ex)
            {
                if (rutaArchivo != null && System.IO.File.Exists(rutaArchivo))
                {
                    System.IO.File.Delete(rutaArchivo);
                }
                ModelState.AddModelError(string.Empty, ex.Message);
                await CargarListas();
                return View(dto);
            }

            TempData["Mensaje"] = "Ficha creada correctamente.";
            TempData["FichaPrevia"] = System.Text.Json.JsonSerializer.Serialize(new { dto.PrendasId, dto.TalleId, dto.ColorId, dto.EmpresasId, dto.Fecha });
            return RedirectToAction(nameof(Crear));
        }

        private async Task CargarListas()
        {
            var prendas = await prendasService.ListarPrendas();
            var talles = await tallesService.ListarTalles();
            var colores = await coloresService.ListarColores();
            var empresas = await empresasService.ListarEmpresas();

            ViewBag.Prendas = new SelectList(prendas, "Id", "ModeloPrenda");
            ViewBag.Talles = new SelectList(talles, "Id", "DescripcionTalle");
            ViewBag.Colores = new SelectList(colores, "Id", "Nombre");
            ViewBag.Empresas = new SelectList(empresas, "Id", "NombreEmpresa");
        }
    }
}
