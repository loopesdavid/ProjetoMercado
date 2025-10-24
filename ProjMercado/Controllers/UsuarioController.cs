using Microsoft.AspNetCore.Mvc;
using ProjMercado.Models;
using ProjMercado.Repository;

namespace ProjetoLoja2DSA.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(UsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        // [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Email, string Senha)
        {
            var usuario = _usuarioRepositorio.ObterUsuario(Email);

            if (usuario != null && usuario.Senha == Senha)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Email / senha Inválidos");

            // RETORNA A PAGINA INDEX
            return View();
        }

        // cadastro do usuario
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _usuarioRepositorio.AdicionarUsuario(usuario);
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}
