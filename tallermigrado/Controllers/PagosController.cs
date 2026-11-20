using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tallermigrado.Dtos;
using tallermigrado.Models;
using tallermigrado.Services;

namespace tallermigrado.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PagosController : Controller
    {
        private readonly IPagosEmpleadoService _pagosService;
        private readonly UserManager<Empleados> _userManager;

        public PagosController(IPagosEmpleadoService pagosService, UserManager<Empleados> userManager)
        {
            _pagosService = pagosService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? empleadoId, string? mes)
        {
            ViewData["Title"] = "Pagos";
            var pagos = await _pagosService.ObtenerPagosPorEmpleado(empleadoId, mes);
            ViewBag.Empleados = new SelectList(
                (await _userManager.Users.ToListAsync()).Select(e => new { e.Id, Nombre = string.IsNullOrWhiteSpace(e.nombre) ? e.UserName : e.nombre }),
                "Id", "Nombre", empleadoId);
            ViewBag.Meses = new SelectList(
                (await _pagosService.ObtenerPagosPorEmpleado(null, null)).Select(p => p.Mes).Distinct().OrderBy(m => m),
                mes);
            ViewBag.EmpleadoId = empleadoId;
            ViewBag.Mes = mes;
            return View(pagos);
        }

        [HttpGet]
        public async Task<IActionResult> Detalle(int id)
        {
            ViewData["Title"] = "Detalle de pago";
            var pago = await _pagosService.ObtenerPagoPorId(id);
            if (pago == null)
            {
                return NotFound();
            }
            return View(pago);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarcarPagado(int id)
        {
            try
            {
                await _pagosService.MarcarComoPagado(id);
                TempData["Mensaje"] = "Pago marcado como pagado.";
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Detalle), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(CrearPagoEmpleadoDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Debe indicar el mes del pago.";
                return RedirectToAction("Index", "Produccion");
            }

            try
            {
                var empleadoId = _userManager.GetUserId(User) ?? string.Empty;
                var pago = await _pagosService.CrearPago(dto, empleadoId);
                TempData["Mensaje"] = $"Pago calculado correctamente. Total: {pago.total:N2}";
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Index", "Produccion");
        }
    }
}
