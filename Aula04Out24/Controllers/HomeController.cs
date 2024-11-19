// a porra da area de login ta com problema, ja fiz de tudo e essa merda nao resolve
using Aula04Out24.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Aula04Out24.Controllers {
    public class HomeController : Controller {
        private readonly FazendaUrbanaEntities _context = new FazendaUrbanaEntities();

        public ActionResult Index() {
            return View();
        }

        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Cadastro login) {
            if (ModelState.IsValid) {
                // Remove a formatação do CPF
                string cpfSemFormatacao = string.Concat(login.CPF.Where(char.IsDigit));

                var usuario = _context.Cadastro
                    .SingleOrDefault(u => u.CPF.Replace(".", "").Replace("-", "") == cpfSemFormatacao && u.Senha == login.Senha);

                if (usuario != null) {
                    TempData["ToastrMessage"] = "Login realizado com sucesso!";
                    TempData["ToastrType"] = "success";

                    // Armazena o usuário na sessão
                    Session["UsuarioLogado"] = usuario;
                    return RedirectToAction("PainelControle");
                }
                else {
                    TempData["ToastrMessage"] = "CPF ou senha inválidos!";
                    TempData["ToastrType"] = "error";
                }
            }
            else {
                LogModelErrors();
                TempData["ToastrMessage"] = "Insira todos os campos corretamente!";
                TempData["ToastrType"] = "error";
            }

            return View(login);
        }

        [HttpGet]
        public ActionResult CadastroUsuario() {
            return View();
        }

        [HttpPost]
        public ActionResult CadastroUsuario(Cadastro cadastro) {
            if (ModelState.IsValid) {
                _context.Cadastro.Add(cadastro);
                _context.SaveChanges();
                TempData["ToastrMessage"] = "Informações enviadas com sucesso!";
                TempData["ToastrType"] = "success";
                return RedirectToAction("CadastroUsuario");
            }
            else {
                LogModelErrors();
                TempData["ToastrMessage"] = "Insira todos os campos para prosseguir!";
                TempData["ToastrType"] = "error";
            }

            return View(cadastro);
        }

        [HttpGet]
        public ActionResult CadastroSementes() {
            return View();
        }

        [HttpPost]
        public ActionResult CadastroSementes(Sementes sementes) {
            if (ModelState.IsValid) {
                _context.Sementes.Add(sementes);
                _context.SaveChanges();
                TempData["ToastrMessage"] = "Informações enviadas com sucesso!";
                TempData["ToastrType"] = "success";
                return RedirectToAction("PainelControle");
            }
            else {
                LogModelErrors();
                TempData["ToastrMessage"] = "Insira todos os campos para prosseguir!";
                TempData["ToastrType"] = "error";
            }

            return View(sementes);
        }

        [HttpGet]
        public ActionResult CadastroNutrientes() {
            return View();
        }

        [HttpPost]
        public ActionResult CadastroNutrientes(Nutrientes cadastroNutriente) {
            if (ModelState.IsValid) {
                _context.Nutrientes.Add(cadastroNutriente);
                _context.SaveChanges();
                TempData["ToastrMessage"] = "Informações enviadas com sucesso!";
                TempData["ToastrType"] = "success";
                return RedirectToAction("PainelControle");
            }
            else {
                LogModelErrors();
                TempData["ToastrMessage"] = "Insira todos os campos para prosseguir!";
                TempData["ToastrType"] = "error";
            }

            return View(cadastroNutriente);
        }

        public ActionResult ControleProducao() {
            return View();
        }

        public ActionResult ControleVendas() {
            return View();
        }

        public ActionResult PainelControle() {
            return View();
        }

        // Método utilitário para logar erros de validação
        private void LogModelErrors() {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors)) {
                Console.WriteLine($"Erro: {error.ErrorMessage}");
            }
        }

        // Dispose o contexto ao final do ciclo de vida
        protected override void Dispose(bool disposing) {
            if (disposing) {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
