using Aula04Out24.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Aula04Out24.Controllers {
    public class HomeController : Controller {
        private readonly FazendaUrbanaEntities1 _context = new FazendaUrbanaEntities1();

        public ActionResult Index() {
            return View();
        }
        public ActionResult ControleRH() {
            return View();
        }
        public ActionResult ControleProducao() {
            return View();
        }
        public ActionResult ControleVendas() {
            return View();
        }
        public ActionResult CadastroUsuario() {
            return View();
        }
        public ActionResult CadastroColaboradores() {
            return View();
        }
        public ActionResult CadastroNutrientes() {
            return View();
        }
        public ActionResult CadastroFornecedor() {
            return View();
        }
        public ActionResult Login() {
            return View();
        }
        public ActionResult EditProduto(int id) {
            var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);
            if (produto == null) {
                return HttpNotFound();
            }
            return View(produto);
        }
        public ActionResult CadastroProduto() {
            return View();
        }
        public ActionResult ListaProdutos() {
            var produtos = _context.Produtos.ToList();
            return View(produtos);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduto(Produtos produto) {
            if (ModelState.IsValid) {
                var produtoBanco = _context.Produtos.FirstOrDefault(p => p.Id == produto.Id);

                if (produtoBanco != null) {
                    produtoBanco.Nome = produto.Nome;
                    produtoBanco.Preco = produto.Preco;
                    produtoBanco.Validade = produto.Validade;
                    produtoBanco.QtdProduto = produto.QtdProduto;
                    _context.SaveChanges();
                    TempData["ToastrMessage"] = "Produto atualizado com sucesso!";
                    TempData["ToastrType"] = "success";
                    return RedirectToAction("ListaProdutos");
                }
                else {
                    TempData["ToastrMessage"] = "Produto não encontrado!";
                    TempData["ToastrType"] = "error";
                    return HttpNotFound();
                }
            }
            LogModelErrors();
            TempData["ToastrMessage"] = "Por favor, corrija os erros no formulário!";
            TempData["ToastrType"] = "error";
            return View(produto);
        }


        [HttpPost]
        public ActionResult Login(Cadastro login) {
            if (ModelState.IsValid) {
                // remover a formatação do cpf para fazer nosso login
                string cpfSemFormatacao = string.Concat(login.CPF.Where(char.IsDigit));

                var usuario = _context.Cadastro
                    .SingleOrDefault(u => u.CPF.Replace(".", "").Replace("-", "") == cpfSemFormatacao && u.Senha == login.Senha);

                if (usuario != null) {
                    TempData["ToastrMessage"] = "Login realizado com sucesso!";
                    TempData["ToastrType"] = "success";
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
        [HttpPost]
        public ActionResult CadastroProduto(Produtos produto) {
            if (ModelState.IsValid) {
                _context.Produtos.Add(produto);
                _context.SaveChanges();
                TempData["ToastrMessage"] = "Informações enviadas com sucesso!";
                TempData["ToastrType"] = "success";
                return RedirectToAction("ListaProdutos");
            }
            else {
                LogModelErrors();
                TempData["ToastrMessage"] = "Insira todos os campos para prosseguir!";
                TempData["ToastrType"] = "error";
            }
            return View(produto);
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
        [HttpPost]
        public ActionResult CadastroFornecedor(Fornecedores fornecedor) {
            if (ModelState.IsValid) {
                _context.Fornecedores.Add(fornecedor);
                _context.SaveChanges();
                TempData["ToastrMessage"] = "Informações enviadas com sucesso!";
                TempData["ToastrType"] = "success";
                return RedirectToAction("CadastroFornecedor");
            }
            else {
                LogModelErrors();
                TempData["ToastrMessage"] = "Insira todos os campos para prosseguir!";
                TempData["ToastrType"] = "error";
            }
            return View(fornecedor);
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

        public ActionResult PainelControle() {
            if (TempData["ToastrMessage"] != null) {
                ViewBag.ToastrMessage = TempData["ToastrMessage"];
                ViewBag.ToastrType = TempData["ToastrType"];
            }
            return View();
        }
        
        [HttpPost]
        public ActionResult CadastroColaboradores(Colaboradores colaboradores) {
            if (ModelState.IsValid) {
                _context.Colaboradores.Add(colaboradores);
                _context.SaveChanges();
                TempData["ToastrMessage"] = "Informações enviadas com sucesso!";
                TempData["ToastrType"] = "success";
                return RedirectToAction("ControleRH");
            }
            else {
                LogModelErrors();
                TempData["ToastrMessage"] = "Insira todos os campos para prosseguir!";
                TempData["ToastrType"] = "error";
            }

            return View(colaboradores);
        }

        // validar algum tipo de erro futuro no toastr
        private void LogModelErrors() {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors)) {
                Console.WriteLine($"Erro: {error.ErrorMessage}");
            }
        }
    }
}

