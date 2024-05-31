using Alan_Veiculos.Models;
using Alan_Veiculos.Properties.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using MySqlConnector;

namespace Alan_Veiculos.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Listar_Usuario()
        {
            var usuarios = await _context.Usuarios.FromSqlRaw("CALL ListarUsuario()").ToListAsync();
            return View(usuarios);
        }

        public IActionResult Inserir_Usuario()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Inserir_Usuario(UsuariosViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "CALL InserirUsuario({0}, {1}, {2}, {3}, {4})",
                    usuario.Funcionario_Id, usuario.Nome, usuario.Login, usuario.Senha, usuario.Email
                );
                return RedirectToAction(nameof(Listar_Usuario));
            }
            return View(usuario);
        }

        public async Task<IActionResult> Alterar_Usuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Alterar_Usuario(int id, UsuariosViewModel usuario)
        {
            if (usuario == null)
            {
                return BadRequest("Dados do usuário inválidos.");
            }

            if (id != usuario.Id)
            {
                return BadRequest("ID do usuário na URL não corresponde ao ID do usuário.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync(
                        "CALL EditarUsuario(@Id, @Funcionario_Id, @Nome, @Login, @Senha, @Email)",
                        new MySqlParameter("@Id", usuario.Id),
                        new MySqlParameter("@Funcionario_Id", usuario.Funcionario_Id),
                        new MySqlParameter("@Nome", usuario.Nome),
                        new MySqlParameter("@Login", usuario.Login),
                        new MySqlParameter("@Senha", usuario.Senha),
                        new MySqlParameter("@Email", usuario.Email)
                    );
                    return RedirectToAction(nameof(Listar_Usuario));
                }
                catch (Exception ex)
                {
                    // Log the exception
                    return StatusCode(500, "Ocorreu um erro ao tentar atualizar o usuário. Por favor, tente novamente mais tarde.");
                }
            }
            return View(usuario);
        }

        public async Task<IActionResult> Excluir_Usuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost, ActionName("Excluir_Usuario")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir_UsuarioConfirmado(int id)
        {
            await _context.Database.ExecuteSqlRawAsync("CALL ExcluirUsuario({0})", id);
            return RedirectToAction(nameof(Listar_Usuario));
        }
    }
}
