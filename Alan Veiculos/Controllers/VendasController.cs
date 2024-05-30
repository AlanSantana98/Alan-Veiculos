using Alan_Veiculos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Alan_Veiculos.Controllers
{
    public class VendasController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public VendasController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Listar_Venda()
        {
            return View();
        }

        public IActionResult Inserir_Venda()
        {
            return View();
        }

        public IActionResult Altera_Venda()
        {
            return View();
        }

        public IActionResult Excluir_Venda()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
