using Alan_Veiculos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Alan_Veiculos.Controllers
{
    public class VeiculosController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public VeiculosController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Listar_Veiculo()
        {
            return View();
        }

        public IActionResult Inserir_Veiculo()
        {
            return View();
        }

        public IActionResult Alterar_Veiculo()
        {
            return View();
        }

        public IActionResult Excluir_Veiculo()
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
