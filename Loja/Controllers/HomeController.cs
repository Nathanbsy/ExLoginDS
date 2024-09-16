using Loja.Models;
using Loja.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Loja.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ILoginRepositorio _loginRepositorio;
        private Usuario _loginUsuario;

        //Criando construtor
        public HomeController(ILogger<HomeController> logger, ILoginRepositorio loginRepositorio, Usuario loginUsuario)
        {
            _logger = logger;
            _loginRepositorio = loginRepositorio;
            _loginUsuario = loginUsuario;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Usuario user)
        {
            Usuario loginUser = _loginRepositorio.Login(user.usuario, user.senha);
            if (loginUser.usuario != null && loginUser.senha != null)
            {
                //_loginRepositiorio.Login();
                return new RedirectResult(Url.Action(nameof(Index)));
            } else
            {
                ViewData["msg"] = "Usu�rio / Senha inv�lidos";
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
