using Alan_Veiculos.Models;
using Alan_Veiculos.Properties.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using MySqlConnector;

namespace Alan_Veiculos.Controllers
{
    public class ClientesController : Controller
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Listar_Cliente()
        {
            var clientes = await _context.Clientes.FromSqlRaw("CALL ListarCliente()").ToListAsync();
            return View(clientes);
        }

        public IActionResult Inserir_Cliente()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Inserir_Cliente(ClientesViewModel cliente)
        {
            if (ModelState.IsValid)
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "CALL InserirCliente({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8})",
                    cliente.Cpf, cliente.Nome, cliente.Cep, cliente.Logradouro, cliente.Bairro,
                    cliente.Localidade, cliente.Uf, cliente.Telefone, cliente.Email
                );
                return RedirectToAction(nameof(Listar_Cliente));
            }
            return View(cliente);
        }

        public async Task<IActionResult> Alterar_Cliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound("Cliente não encontrado.");
            }
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Alterar_Cliente(int id, ClientesViewModel cliente)
        {
            if (cliente == null)
            {
                return BadRequest("Dados do cliente inválidos.");
            }

            if (id != cliente.Id)
            {
                return BadRequest("ID do cliente na URL não corresponde ao ID do cliente.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync(
                        "CALL EditarCliente(@Id, @Cpf, @Nome, @Cep, @Logradouro, @Bairro, @Localidade, @Uf, @Telefone, @Email)",
                        new MySqlParameter("@Id", cliente.Id),
                        new MySqlParameter("@Cpf", cliente.Cpf),
                        new MySqlParameter("@Nome", cliente.Nome),
                        new MySqlParameter("@Cep", cliente.Cep),
                        new MySqlParameter("@Logradouro", cliente.Logradouro),
                        new MySqlParameter("@Bairro", cliente.Bairro),
                        new MySqlParameter("@Localidade", cliente.Localidade),
                        new MySqlParameter("@Uf", cliente.Uf),
                        new MySqlParameter("@Telefone", cliente.Telefone),
                        new MySqlParameter("@Email", cliente.Email)
                    );
                    return RedirectToAction(nameof(Listar_Cliente));
                }
                catch (Exception ex)
                {
                    // Log the exception
                    return StatusCode(500, "Ocorreu um erro ao tentar atualizar o cliente. Por favor, tente novamente mais tarde.");
                }
            }
            return View(cliente);
        }
        public async Task<IActionResult> Excluir_Cliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [HttpPost, ActionName("Excluir_Cliente")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir_ClienteConfirmado(int id)
        {
            await _context.Database.ExecuteSqlRawAsync("CALL ExcluirCliente({0})", id);
            return RedirectToAction(nameof(Listar_Cliente));
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
