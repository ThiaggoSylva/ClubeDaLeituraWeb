using ClubeDaLeituraWeb.WebApp.ModuloCaixa.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloRevista.Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace ClubeDaLeituraWeb.WebApp.ModuloRevista.Apresentacao;

public class RevistaController : Controller
{
    private readonly IRepositorioRevista repositorioRevista;
    private readonly IRepositorioCaixa repositorioCaixa;

    public RevistaController(
        IRepositorioRevista repositorioRevista,
        IRepositorioCaixa repositorioCaixa)
    {
        this.repositorioRevista = repositorioRevista;
        this.repositorioCaixa = repositorioCaixa;
    }

    public IActionResult Listar()
    {
        List<Revista> revistas =
            repositorioRevista.SelecionarTodos();

        List<ListarRevistasViewModel> listarVms =
            new List<ListarRevistasViewModel>();

        foreach (Revista r in revistas)
        {
            listarVms.Add(new ListarRevistasViewModel(
                r.Id,
                r.Titulo,
                r.NumeroEdicao,
                r.AnoPublicacao,
                r.CaixaId
            ));
        }

        return View(listarVms);
    }

    [HttpGet]
    public IActionResult Cadastrar()
    {
        var caixas = repositorioCaixa.SelecionarTodos();

        ViewBag.Caixas = caixas
            .Select(c => new SelectListItem
            {
                Value = c.Id,
                Text = c.Etiqueta
            })
            .ToList();

        return View();
    }

    [HttpPost]
    public IActionResult Cadastrar(
        CadastrarRevistaViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        Revista revista = new(
            vm.Titulo,
            vm.NumeroEdicao,
            vm.AnoPublicacao,
            vm.CaixaId);

        Revista? revistaExistente =
            repositorioRevista
            .SelecionarPorTituloEdicao(
                revista.Titulo,
                revista.NumeroEdicao);

        if (revistaExistente != null)
        {
            ModelState.AddModelError(
                "",
                "Já existe uma revista com este título e edição.");

            return View(vm);
        }

        repositorioRevista.Cadastrar(revista);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public IActionResult Editar(string id)
    {
        Revista? revistaSelecionada = repositorioRevista.SelecionarPorId(id);

    if (revistaSelecionada == null)
        return RedirectToAction(nameof(Listar));

    EditarRevistaViewModel vm = new EditarRevistaViewModel(
        revistaSelecionada.Id,
        revistaSelecionada.Titulo,
        revistaSelecionada.NumeroEdicao,
        revistaSelecionada.AnoPublicacao,
        revistaSelecionada.CaixaId
    );

    List<SelectListItem> caixas = repositorioCaixa
        .SelecionarTodos()
        .Select(c => new SelectListItem
        {
            Value = c.Id,
            Text = c.Etiqueta
        })
        .ToList();

    ViewBag.Caixas = caixas;

    return View(vm);
    }

    [HttpPost]
public IActionResult Editar(EditarRevistaViewModel vm)
{
    if (!ModelState.IsValid)
    {
        ViewBag.Caixas = repositorioCaixa
            .SelecionarTodos()
            .Select(c => new SelectListItem
            {
                Value = c.Id,
                Text = c.Etiqueta
            })
            .ToList();

        return View(vm);
    }

    Revista revistaAtualizada = vm.ToRevista();

    repositorioRevista.Editar(vm.id, revistaAtualizada);

    return RedirectToAction(nameof(Listar));
}
    [HttpGet]
    public IActionResult Excluir(string id)
    {
        Revista? revista =
            repositorioRevista.SelecionarPorId(id);

        if (revista == null)
            return RedirectToAction(nameof(Listar));

        ExcluirRevistaViewModel vm = new(
            id,
            revista.Titulo,
            revista.NumeroEdicao,
            revista.AnoPublicacao,
            revista.CaixaId);

        return View(vm);
    }

    [HttpPost]
    public IActionResult Excluir(
        ExcluirRevistaViewModel vm)
    {
        Revista? revista =
            repositorioRevista.SelecionarPorId(vm.id);

        if (revista != null)
            repositorioRevista.Excluir(vm.id);

        return RedirectToAction(nameof(Listar));
    }
}
