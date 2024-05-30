using Alan_Veiculos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Alan_Veiculos.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly ApplicationDbContext _context;

        public UsuariosController(ILogger<HomeController> logger)
        {
            _logger = logger;
           // _context = Context;
        }

        public IActionResult Listar_Usuarios()
        {
            return View();
        }

        public IActionResult Inserir_Usuarios()
        {
            return View();
        }

        public IActionResult Alterar_Usuarios()
        {
            return View();
        }

        public IActionResult Excluir_Usuarios()
        {
            return View();
        }

        //public IActionResult ListarFuncionari()
        //{
        //    var funcionarios = _context.Funcionarios.ToList();
        //    return View(funcionarios);
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
