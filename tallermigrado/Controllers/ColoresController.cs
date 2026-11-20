using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tallermigrado.Dtos;
using tallermigrado.Services;

namespace tallermigrado.Controllers
{
    [Authorize]
    public class ColoresController : Controller
    {
        private readonly IColoresService coloresService;

        public ColoresController(IColoresService coloresService)
        {
            this.coloresService = coloresService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var colores = await coloresService.ListarColores();
            return View(colores);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View(new CrearColorDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(CrearColorDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            try
            {
                await coloresService.CrearColor(dto);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(dto);
            }

            TempData["Mensaje"] = "Color creado correctamente.";
            return RedirectToAction(nameof(Crear));
        }
    }
}
