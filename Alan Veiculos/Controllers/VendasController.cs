using Alan_Veiculos.Models;
using Alan_Veiculos.Properties.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using MySqlConnector;

namespace Alan_Veiculos.Controllers
{
    public class VendasController : Controller
    {
        private readonly AppDbContext _context;

        public VendasController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Listar_Venda()
        {
            var vendas = await _context.Vendas.FromSqlRaw("CALL ListarVenda()").ToListAsync();
            return View(vendas);
        }

        public IActionResult Inserir_Venda()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Inserir_Venda(VendasViewModel venda)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    venda.Data_Hora = DateTime.Now;  // Atribuindo data e hora atuais

                    await _context.Database.ExecuteSqlRawAsync(
                        "CALL InserirVenda({0}, {1}, {2}, {3}, {4}, {5}, {6})",
                        venda.Funcionario_Id, venda.Veiculo_Id, venda.Cliente_Id, venda.Valor, venda.Data_Hora, venda.Comissao, venda.Garantia
                    );
                    return RedirectToAction(nameof(Listar_Venda));
                }
                catch (Exception ex)
                {
                    // Log the exception
                    return StatusCode(500, "Ocorreu um erro ao tentar inserir a venda. Por favor, tente novamente mais tarde.");
                }
            }
            return View(venda);
        }

        public async Task<IActionResult> Alterar_Venda(int id)
        {
            var venda = await _context.Vendas.FindAsync(id);
            if (venda == null)
            {
                return NotFound("Venda não encontrada.");
            }
            return View(venda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Alterar_Venda(int id, VendasViewModel venda)
        {
            if (venda == null)
            {
                return BadRequest("Dados da venda inválidos.");
            }

            if (id != venda.Id)
            {
                return BadRequest("ID da venda na URL não corresponde ao ID da venda.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync(
                        "CALL EditarVenda(@Id, @Funcionario_Id, @Veiculo_Id, @Cliente_Id, @Valor, @Comissao, @Garantia)",
                        new MySqlParameter("@Id", venda.Id),
                        new MySqlParameter("@Funcionario_Id", venda.Funcionario_Id),
                        new MySqlParameter("@Veiculo_Id", venda.Veiculo_Id),
                        new MySqlParameter("@Cliente_Id", venda.Cliente_Id),
                        new MySqlParameter("@Valor", venda.Valor),
                        new MySqlParameter("@Comissao", venda.Comissao),
                        new MySqlParameter("@Garantia", venda.Garantia)
                    );
                    return RedirectToAction(nameof(Listar_Venda));
                }
                catch (Exception ex)
                {
                    // Log the exception
                    return StatusCode(500, "Ocorreu um erro ao tentar atualizar a venda. Por favor, tente novamente mais tarde.");
                }
            }
            return View(venda);
        }

        public async Task<IActionResult> Excluir_Venda(int id)
        {
            var venda = await _context.Vendas.FindAsync(id);
            if (venda == null)
            {
                return NotFound();
            }

            return View(venda);
        }

        [HttpPost, ActionName("Excluir_Venda")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir_VendaConfirmado(int id)
        {
            await _context.Database.ExecuteSqlRawAsync("CALL ExcluirVenda({0})", id);
            return RedirectToAction(nameof(Listar_Venda));
        }
    }
}
