using ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Infraestrutura;
using Microsoft.AspNetCore.Mvc;

namespace ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Apresentacao.Controllers
{
    public class EmprestimoController : Controller
    {
        private readonly EmprestimoRepositorio _repositorio = new();

        public IActionResult Index()
        {
            var emprestimos = _repositorio.ObterTodos();
            foreach (var e in emprestimos)
                if (e.EstaAtrasado()) e.Status = "Atrasado";
            return View(emprestimos);
        }

        public IActionResult Criar()
        {
            return View(new Emprestimo());
        }

        [HttpPost]
        public IActionResult Criar(Emprestimo emprestimo)
        {
            var lista = _repositorio.ObterTodos();
            if (lista.Any(e => e.Amigo == emprestimo.Amigo && e.Status == "Aberto"))
            {
                ModelState.AddModelError("", "Este amigo já possui um empréstimo em aberto!");
                return View(emprestimo);
            }
            emprestimo.Id = lista.Count + 1;
            emprestimo.DataEmprestimo = DateTime.Now;
            lista.Add(emprestimo);
            _repositorio.Salvar(lista);
            return RedirectToAction("Index");
        }

        public IActionResult Devolver(int id)
        {
            var lista = _repositorio.ObterTodos();
            var emprestimo = lista.FirstOrDefault(e => e.Id == id);
            if (emprestimo != null)
            {
                emprestimo.Status = "Concluído";
                _repositorio.Salvar(lista);
            }
            return RedirectToAction("Index");
        }
    }
}