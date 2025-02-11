using Alan_Veiculos.Models;
using Alan_Veiculos.Properties.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using MySqlConnector;

namespace Alan_Veiculos.Controllers
{
    public class FuncionariosController : Controller
    {
        private readonly AppDbContext _context;

        public FuncionariosController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Listar_Funcionario()
        {
            var funcionarios = await _context.Funcionarios.FromSqlRaw("CALL ListarFuncionario()").ToListAsync();
            return View(funcionarios);
        }

        public IActionResult Inserir_Funcionario()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Inserir_Funcionario(FuncionariosViewModel funcionario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync(
                        "CALL InserirFuncionario(@Cpf, @Nome, @Cep, @Logradouro, @Bairro, @Localidade, @Uf, @Salario, @Comissao, @Telefone, @Email)",
                        new MySqlParameter("@Cpf", funcionario.Cpf),
                        new MySqlParameter("@Nome", funcionario.Nome),
                        new MySqlParameter("@Cep", funcionario.Cep),
                        new MySqlParameter("@Logradouro", funcionario.Logradouro),
                        new MySqlParameter("@Bairro", funcionario.Bairro),
                        new MySqlParameter("@Localidade", funcionario.Localidade),
                        new MySqlParameter("@Uf", funcionario.Uf),
                        new MySqlParameter
                        {
                            ParameterName = "@Salario",
                            Value = funcionario.Salario,
                            MySqlDbType = MySqlDbType.Decimal
                        },
                        new MySqlParameter
                        {
                            ParameterName = "@Comissao",
                            Value = funcionario.Comissão,
                            MySqlDbType = MySqlDbType.Decimal
                        },
                        new MySqlParameter("@Telefone", funcionario.Telefone),
                        new MySqlParameter("@Email", funcionario.Email)
                    );
                    return RedirectToAction(nameof(Listar_Funcionario));
                }
                catch (Exception ex)
                {
                    // Log the exception
                    return StatusCode(500, "Ocorreu um erro ao tentar inserir o funcionário.");
                }
            }
            return View(funcionario);
        }

        public async Task<IActionResult> Alterar_Funcionario(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound("Funcionário não encontrado.");
            }
            return View(funcionario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Alterar_Funcionario(int id, FuncionariosViewModel funcionario)
        {
            if (funcionario == null)
            {
                return BadRequest("Dados do funcionário inválidos.");
            }

            if (id != funcionario.Id)
            {
                return BadRequest("ID do funcionário na URL não corresponde ao ID do funcionário.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync(
                        "CALL EditarFuncionario(@Id, @Cpf, @Nome, @Cep, @Logradouro, @Bairro, @Localidade, @Uf, @Salario, @Comissão, @Telefone, @Email)",
                        new MySqlParameter("@Id", funcionario.Id),
                        new MySqlParameter("@Cpf", funcionario.Cpf),
                        new MySqlParameter("@Nome", funcionario.Nome),
                        new MySqlParameter("@Cep", funcionario.Cep),
                        new MySqlParameter("@Logradouro", funcionario.Logradouro),
                        new MySqlParameter("@Bairro", funcionario.Bairro),
                        new MySqlParameter("@Localidade", funcionario.Localidade),
                        new MySqlParameter("@Uf", funcionario.Uf),
                        new MySqlParameter
                       {
                           ParameterName = "@Salario",
                           Value = funcionario.Salario,
                           MySqlDbType = MySqlDbType.Decimal
                       },
                        new MySqlParameter
                        {
                            ParameterName = "@Comissao",
                            Value = funcionario.Comissão,
                            MySqlDbType = MySqlDbType.Decimal
                        },
                        new MySqlParameter("@Telefone", funcionario.Telefone),
                        new MySqlParameter("@Email", funcionario.Email)
                    );
                    return RedirectToAction(nameof(Listar_Funcionario));
                }
                catch (Exception ex)
                {
                    // Log the exception
                    return StatusCode(500, "Ocorreu um erro ao tentar atualizar o funcionário. Por favor, tente novamente mais tarde.");
                }
            }
            return View(funcionario);
        }

        public async Task<IActionResult> Excluir_Funcionario(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        [HttpPost, ActionName("Excluir_Funcionario")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir_FuncionarioConfirmado(int id)
        {
            await _context.Database.ExecuteSqlRawAsync("CALL ExcluirFuncionario({0})", id);
            return RedirectToAction(nameof(Listar_Funcionario));
        }
    }
}
