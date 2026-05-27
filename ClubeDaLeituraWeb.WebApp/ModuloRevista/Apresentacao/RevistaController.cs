using ClubeDaLeituraWeb.WebApp.ModuloRevista.Dominio;
using Microsoft.AspNetCore.Mvc;

namespace ClubeDaLeituraWeb.WebApp.ModuloRevista.Apresentacao;

public class RevistaController : Controller
{
    private readonly IRepositorioRevista repositorioRevista;

    public RevistaController(
        IRepositorioRevista repositorioRevista)
    {
        this.repositorioRevista = repositorioRevista;
    }

    public IActionResult Listar()
    {
        List<Revista> revistas =
            repositorioRevista.SelecionarTodos();

        List<ListarRevistasViewModel> listarVms = [];

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
        return View(new CadastrarRevistaViewModel());
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
        Revista? revista =
            repositorioRevista.SelecionarPorId(id);

        if (revista == null)
            return RedirectToAction(nameof(Listar));

        EditarRevistaViewModel vm = new(
            revista.Id,
            revista.Titulo,
            revista.NumeroEdicao,
            revista.AnoPublicacao,
            revista.CaixaId);

        return View(vm);
    }

   [HttpPost]
public IActionResult Editar(EditarRevistaViewModel editarVm)
{
    if (!ModelState.IsValid)
        return View(editarVm);

    Revista revistaAtualizada = new Revista(
        editarVm.Titulo,
        editarVm.NumeroEdicao,
        editarVm.AnoPublicacao,
        editarVm.CaixaId
    );

    repositorioRevista.Editar(
        editarVm.Id,
        revistaAtualizada
    );

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
            revista.Id,
            revista.Titulo,
            revista.NumeroEdicao);

        return View(vm);
    }

    [HttpPost]
    public IActionResult Excluir(
        ExcluirRevistaViewModel vm)
    {
        Revista? revista =
            repositorioRevista.SelecionarPorId(vm.Id);

        if (revista != null)
            repositorioRevista.Excluir(vm.Id);

        return RedirectToAction(nameof(Listar));
    }
}