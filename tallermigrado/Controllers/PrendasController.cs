using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tallermigrado.Dtos;
using tallermigrado.Services;

namespace tallermigrado.Controllers
{
    [Authorize]
    public class PrendasController : Controller
    {
        private readonly IprendasService prendasService;

        public PrendasController(IprendasService prendasService)
        {
            this.prendasService = prendasService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var prendas = await prendasService.ListarPrendas();
            return View(prendas);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View(new CrearPrendaDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(CrearPrendaDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            try
            {
                await prendasService.CrearPrenda(dto);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(dto);
            }

            TempData["Mensaje"] = "Prenda creada correctamente.";
            return RedirectToAction(nameof(Crear));
        }
    }
}
