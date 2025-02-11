using Alan_Veiculos.Models;
using Alan_Veiculos.Properties.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System;
using System.Threading.Tasks;

namespace Alan_Veiculos.Controllers
{
    public class VeiculosController : Controller
    {
        private readonly AppDbContext _context;

        public VeiculosController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Listar_Veiculo()
        {
            var veiculos = await _context.Veiculos.FromSqlRaw("CALL ListarVeiculo()").ToListAsync();
            return View(veiculos);
        }

        public IActionResult Inserir_Veiculo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Inserir_Veiculo(VeiculosViewModel veiculo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                         await _context.Database.ExecuteSqlRawAsync(
                         "CALL InserirVeiculo(@Nome, @Modelo, @Placa, @Ano, @Valor)",
                         new MySqlParameter("@Nome", veiculo.Nome),
                         new MySqlParameter("@Modelo", veiculo.Modelo),
                         new MySqlParameter("@Placa", veiculo.Placa),
                         new MySqlParameter("@Ano", veiculo.Ano),
                         new MySqlParameter
                         {
                             ParameterName = "@Valor",
                             Value = veiculo.Valor,
                             MySqlDbType = MySqlDbType.Decimal
                         }
                     );
                    return RedirectToAction(nameof(Listar_Veiculo));
                }
                catch (Exception ex)
                {
                    // Log the exception
                    return StatusCode(500, "Ocorreu um erro ao tentar inserir o veículo. Por favor, tente novamente mais tarde.");
                }
            }
            return View(veiculo);
        }

        public async Task<IActionResult> Alterar_Veiculo(int id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo == null)
            {
                return NotFound("Veículo não encontrado.");
            }
            return View(veiculo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Alterar_Veiculo(int id, VeiculosViewModel veiculo)
        {
            if (veiculo == null)
            {
                return BadRequest("Dados do veículo inválidos.");
            }

            if (id != veiculo.Id)
            {
                return BadRequest("ID do veículo na URL não corresponde ao ID do veículo.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync(
                        "CALL EditarVeiculo(@Id, @Nome, @Modelo, @Placa, @Ano, @Valor)",
                        new MySqlParameter("@Id", veiculo.Id),
                        new MySqlParameter("@Nome", veiculo.Nome),
                        new MySqlParameter("@Modelo", veiculo.Modelo),
                        new MySqlParameter("@Placa", veiculo.Placa),
                        new MySqlParameter("@Ano", veiculo.Ano),
                        new MySqlParameter("@Valor", veiculo.Valor)
                    );
                    return RedirectToAction(nameof(Listar_Veiculo));
                }
                catch (Exception ex)
                {
                    // Log the exception
                    return StatusCode(500, "Ocorreu um erro ao tentar atualizar o veículo. Por favor, tente novamente mais tarde.");
                }
            }
            return View(veiculo);
        }

        public async Task<IActionResult> Excluir_Veiculo(int id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo == null)
            {
                return NotFound();
            }

            return View(veiculo);
        }

        [HttpPost, ActionName("Excluir_Veiculo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir_VeiculoConfirmado(int id)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("CALL ExcluirVeiculo(@Id)", new MySqlParameter("@Id", id));
                return RedirectToAction(nameof(Listar_Veiculo));
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Ocorreu um erro ao tentar excluir o veículo. Por favor, tente novamente mais tarde.");
            }
        }
    }
}
