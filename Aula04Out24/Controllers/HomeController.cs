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
        public ActionResult Login() {
            return View();
        }
        public ActionResult PainelControle() {
            if (TempData["ToastrMessage"] != null) {
                ViewBag.ToastrMessage = TempData["ToastrMessage"];
                ViewBag.ToastrType = TempData["ToastrType"];
            }
            ViewBag.NomeUsuario = UserLogin();
            return View();
        }
        public ActionResult ControleProducao() {
            ViewBag.NomeUsuario = UserLogin();
            return View();
        }
        public ActionResult ControleVendas() {
            return View();
        }
        public ActionResult CadastroUsuario() {
            ViewBag.NomeUsuario = UserLogin();
            return View();
        }
        public ActionResult CadastroColaboradores() {
            ViewBag.NomeUsuario = UserLogin();
            return View();
        }
        public ActionResult CadastroNutrientes() {
            ViewBag.NomeUsuario = UserLogin();
            return View();
        }
        public ActionResult CadastroFornecedor() {
            ViewBag.NomeUsuario = UserLogin();
            return View();
        }
        public ActionResult CadastroProduto() {
            ViewBag.NomeUsuario = UserLogin();
            return View();
        }
        public ActionResult ControleRH() {
            ViewBag.NomeUsuario = UserLogin();
            return View();
        }

        //entidade de validaçao de login
        [HttpPost]
        public ActionResult Login(Cadastro login) {
            if (ModelState.IsValid) {
                // concatenação para nossa string do cpf
                string cpfSemFormatacao = string.Concat(login.CPF.Where(char.IsDigit));
                var usuario = _context.Cadastro
                    .SingleOrDefault(u => u.CPF.Replace(".", "").Replace("-", "") == cpfSemFormatacao && u.Senha == login.Senha);

                if (usuario != null) {
                    //armazenamento do adm na sessão principal
                    Session["NomeUsuario"] = usuario.NomeFull;

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

        //entidade de cadastro de usuario
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

        //entidade de cadastro de produto
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

        //entidade de cadastro de nutrientes
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

        //entidade de cadastro de colaborador
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

        //entidade de crud para fornecedor

        [HttpPost]
        public ActionResult CadastroFornecedor(Fornecedores fornecedor) {
            if (ModelState.IsValid) {
                _context.Fornecedores.Add(fornecedor);
                _context.SaveChanges();
                TempData["ToastrMessage"] = "Informações enviadas com sucesso!";
                TempData["ToastrType"] = "success";
                return RedirectToAction("ListaFornecedores");
            }
            else {
                LogModelErrors();
                TempData["ToastrMessage"] = "Insira todos os campos para prosseguir!";
                TempData["ToastrType"] = "error";
            }
            return View(fornecedor);
        }
        public ActionResult EditFornecedor (int id) {
            ViewBag.NomeUsuario = UserLogin();
            var fornecedores = _context.Fornecedores.FirstOrDefault(p => p.Id == id); 
            if (fornecedores == null){
                return HttpNotFound();
            }
            return View (fornecedores);
        }
        public ActionResult ListaFornecedores() {
            ViewBag.NomeUsuario = UserLogin();
            var fornecedores = _context.Fornecedores.ToList();
            return View(fornecedores);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFornecedor(Fornecedores fornecedores) {
            ViewBag.NomeUsuario = UserLogin();
            if (ModelState.IsValid) {
                var fornecedoresBanco = _context.Fornecedores.FirstOrDefault(p => p.Id == fornecedores.Id);
                if (fornecedoresBanco != null) {
                    fornecedoresBanco.Nome = fornecedores.Nome;
                    fornecedoresBanco.TipoFornecimento = fornecedores.TipoFornecimento;
                    fornecedoresBanco.CNPJ = fornecedores.CNPJ;
                    _context.SaveChanges();
                    TempData["ToastrMessage"] = "Fornecedor atualizado com sucesso!";
                    TempData["ToastrType"] = "success";
                    return RedirectToAction("ListaFornecedores");
                }
                else {
                    TempData["ToastrMessage"] = "Fornecedor não encontrado!";
                    TempData["ToastrType"] = "error";
                    return HttpNotFound();
                }
            }
            LogModelErrors();
            TempData["ToastrMessage"] = "Por favor, corrija os erros no formulário!";
            TempData["ToastrType"] = "error";
            return View(fornecedores);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirFornecedor(int id) {
            try {
                var fornecedores = _context.Fornecedores.Find(id);
                if (fornecedores != null) {
                    _context.Fornecedores.Remove(fornecedores);
                    _context.SaveChanges();
                    TempData["ToastrMessage"] = "Fornecedor excluído com sucesso!";
                    TempData["ToastrType"] = "success";
                }
                else {
                    TempData["ToastrMessage"] = "Fornecedor não encontrado!";
                    TempData["ToastrType"] = "error";
                }
            }
            catch (Exception ex) {
                TempData["Erro"] = $"Erro ao excluir o Fornecedor: {ex.Message}";
                TempData["ToastrType"] = "error";
            }
            return RedirectToAction("ListaFornecedores");
        }

        //entidade de crud para produto
        public ActionResult EditProduto(int id) {
            var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);
            if (produto == null) {
                return HttpNotFound();
            }
            return View(produto);
        }
        public ActionResult ListaProdutos() {
            ViewBag.NomeUsuario = UserLogin();
            var produtos = _context.Produtos.ToList();
            return View(produtos);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduto(Produtos produto) {
            ViewBag.NomeUsuario = UserLogin();
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
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirProduto(int id) {
            try {
                var produto = _context.Produtos.Find(id);
                if (produto != null) {
                    _context.Produtos.Remove(produto);
                    _context.SaveChanges();
                    TempData["ToastrMessage"] = "Produto excluído com sucesso!";
                    TempData["ToastrType"] = "success";  
                }
                else {
                    TempData["ToastrMessage"] = "Produto não encontrado!";
                    TempData["ToastrType"] = "error";
                }
            }
            catch (Exception ex) {
                TempData["Erro"] = $"Erro ao excluir o produto: {ex.Message}";
                TempData["ToastrType"] = "error"; 
            }
            return RedirectToAction("ListaProdutos");
        }

        // método de verificação para ver se o usuario esta logado
        private string UserLogin() {
            var nomeUsuario = Session["NomeUsuario"] != null ? Session["NomeUsuario"].ToString() : "Usuário não logado";
            return nomeUsuario;
        }

        // validar algum tipo de erro futuro no toastr
        private void LogModelErrors() {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors)) {
                Console.WriteLine($"Erro: {error.ErrorMessage}");
            }
        }
    }
}

