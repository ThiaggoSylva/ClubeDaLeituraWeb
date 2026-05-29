using ClubeDaLeituraWeb.WebApp.ModuloAmigo.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Apresentacao.Models;
using ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloRevista.Dominio;
using Microsoft.AspNetCore.Mvc;
using ClubeDaLeituraWeb.WebApp.ModuloCaixa.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Apresentacao;

public class EmprestimoController : Controller
{
    private readonly IRepositorioEmprestimo repositorioEmprestimo;

    private readonly IRepositorioAmigo repositorioAmigo;

    private readonly IRepositorioRevista repositorioRevista;
    private readonly IRepositorioCaixa repositorioCaixa;

    public EmprestimoController(
        IRepositorioEmprestimo repositorioEmprestimo,
        IRepositorioAmigo repositorioAmigo,
        IRepositorioRevista repositorioRevista,
        IRepositorioCaixa repositorioCaixa)
    {
        this.repositorioEmprestimo = repositorioEmprestimo;
        this.repositorioAmigo = repositorioAmigo;
        this.repositorioRevista = repositorioRevista;
        this.repositorioCaixa = repositorioCaixa;
    }

    public IActionResult ListarAbertos()
    {
        List<Emprestimo> emprestimos =
            repositorioEmprestimo
            .SelecionarEmprestimosAbertos();

        List<ListarEmprestimoViewModel> registros = [];

        foreach (Emprestimo e in emprestimos)
        {
            ListarEmprestimoViewModel vm =
                new(
                    e.Id,
                    e.Amigo.Nome,
                    e.Revista.Titulo,
                    e.DataEmprestimo,
                    e.DataDevolucao,
                    e.Status
                );

            registros.Add(vm);
        }

        return View(registros);
    }

    public IActionResult ListarFechados()
    {
        List<Emprestimo> emprestimos =
            repositorioEmprestimo
            .SelecionarEmprestimosFechados();

        List<ListarEmprestimoViewModel> registros = [];

        foreach (Emprestimo e in emprestimos)
        {
            ListarEmprestimoViewModel vm =
                new(
                    e.Id,
                    e.Amigo.Nome,
                    e.Revista.Titulo,
                    e.DataEmprestimo,
                    e.DataDevolucao,
                    e.Status
                );

            registros.Add(vm);
        }

        return View(registros);
    }

    [HttpGet]
    public IActionResult Cadastrar()
    {
        CadastrarEmprestimoViewModel vm =
            new(string.Empty, string.Empty);

        vm.Amigos =
            repositorioAmigo
            .SelecionarTodos();

        vm.Revistas =
            repositorioRevista
            .SelecionarTodos()
            .Where(x => x.Status == "Disponível")
            .ToList();

        return View(vm);
    }

    [HttpPost]
    public IActionResult Cadastrar(
        CadastrarEmprestimoViewModel vm)
    {
        vm.Amigos =
            repositorioAmigo
            .SelecionarTodos();

        vm.Revistas =
            repositorioRevista
            .SelecionarTodos()
            .Where(x => x.Status == "Disponível")
            .ToList();

        if (!ModelState.IsValid)
            return View(vm);

        Amigo? amigo =
        repositorioAmigo.SelecionarPorId(vm.AmigoId);

        if (amigo == null)
        {
            ModelState.AddModelError(
                string.Empty,
                "Amigo não encontrado.");

            return View(vm);
        }

        Revista? revista =
            repositorioRevista
            .SelecionarPorId(vm.RevistaId);

        if (revista == null)
        {
            ModelState.AddModelError(
                string.Empty,
                "Revista não encontrada.");

            return View(vm);
        }

        if (revista.Status != "Disponível")
        {
            ModelState.AddModelError(
                string.Empty,
                "A revista não está disponível.");

            return View(vm);
        }

        bool amigoPossuiEmprestimo =
            repositorioEmprestimo
            .AmigoPossuiEmprestimoAberto(amigo.Id);

        if (amigoPossuiEmprestimo)
        {
            ModelState.AddModelError(
                string.Empty,
                "O amigo já possui um empréstimo aberto.");

            return View(vm);
        }


        Caixa? caixa =
        repositorioCaixa.SelecionarPorId(revista.CaixaId);



        Emprestimo emprestimo = new()
        {
            Amigo = amigo,
            Revista = revista,
            DataEmprestimo = DateTime.Now,
            DataDevolucao = DateTime.Now.AddDays(
                caixa!.DiasDeEmprestimo)
        };

        revista.Status = "Emprestada";

        repositorioRevista.Editar(
            revista.Id,
            revista);

        repositorioEmprestimo.Cadastrar(
            emprestimo);

        return RedirectToAction(
            nameof(ListarAbertos));
    }

    [HttpGet]
    public IActionResult RegistrarDevolucao(string id)
    {
        Emprestimo? emprestimo =
            repositorioEmprestimo
            .SelecionarPorId(id);

        if (emprestimo == null)
            return RedirectToAction(
                nameof(ListarAbertos));

        RegistrarDevolucaoViewModel vm =
            new(
                emprestimo.Id,
                emprestimo.Revista.Titulo,
                emprestimo.Amigo.Nome
            );

        return View(vm);
    }

    [HttpPost]
    public IActionResult RegistrarDevolucao(
        RegistrarDevolucaoViewModel vm)
    {
        Emprestimo? emprestimo =
            repositorioEmprestimo
            .SelecionarPorId(vm.Id);

        if (emprestimo == null)
            return RedirectToAction(
                nameof(ListarAbertos));

        emprestimo.DataDevolucaoRealizada =
            DateTime.Now;

        emprestimo.Revista.Status =
            "Disponível";

        repositorioRevista.Editar(
            emprestimo.Revista.Id,
            emprestimo.Revista);

        repositorioEmprestimo.Editar(
            emprestimo.Id,
            emprestimo);

        return RedirectToAction(
            nameof(ListarAbertos));
    }
}

