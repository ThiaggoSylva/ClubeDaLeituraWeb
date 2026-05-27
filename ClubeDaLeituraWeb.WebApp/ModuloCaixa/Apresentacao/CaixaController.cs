using ClubeDaLeituraWeb.WebApp.ModuloCaixa.Dominio;
using Microsoft.AspNetCore.Mvc;
using ClubeDaLeituraWeb.WebApp.ModuloRevista.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloCaixa.Apresentacao;

public class CaixaController : Controller
{
    private readonly IRepositorioCaixa repositorioCaixa;
    private readonly IRepositorioRevista repositorioRevista;

    public CaixaController(
    IRepositorioCaixa repositorioCaixa,
    IRepositorioRevista repositorioRevista)
    {
    this.repositorioCaixa = repositorioCaixa;
    this.repositorioRevista = repositorioRevista;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<Caixa> caixas = repositorioCaixa.SelecionarTodos();

        List<ListarCaixasViewModel> listarVms = new List<ListarCaixasViewModel>();

        foreach (Caixa c in caixas)
        {
            ListarCaixasViewModel viewModel = new ListarCaixasViewModel(
                c.Id,
                c.Etiqueta,
                c.Cor,
                c.DiasDeEmprestimo
            );

            listarVms.Add(viewModel);
        }

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarCaixaViewModel cadastrarVm = new CadastrarCaixaViewModel(
            string.Empty,
            string.Empty,
            7
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarCaixaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        Caixa novaCaixa = new Caixa(
            cadastrarVm.Etiqueta,
            cadastrarVm.Cor,
            cadastrarVm.DiasDeEmprestimo
        );

        Caixa? caixaExistente =
    repositorioCaixa.SelecionarPorEtiqueta(cadastrarVm.Etiqueta);

    if (caixaExistente != null)
    {
    ModelState.AddModelError(
        "Etiqueta",
        "Já existe uma caixa com esta etiqueta.");

    return View(cadastrarVm);
    }

        repositorioCaixa.Cadastrar(novaCaixa);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(string id)
    {
        Caixa? caixa = repositorioCaixa.SelecionarPorId(id);

        if (caixa == null)
            return RedirectToAction(nameof(Listar));

        EditarCaixaViewModel editarVm = new EditarCaixaViewModel(
            id,
            caixa.Etiqueta,
            caixa.Cor,
            caixa.DiasDeEmprestimo
        );

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarCaixaViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        Caixa caixaAtualizada = new Caixa(
            editarVm.Etiqueta,
            editarVm.Cor,
            editarVm.DiasDeEmprestimo
        );

        repositorioCaixa.Editar(editarVm.Id, caixaAtualizada);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(string id)
    {
        Caixa? caixa = repositorioCaixa.SelecionarPorId(id);

        if (caixa == null)
            return RedirectToAction(nameof(Listar));

        ExcluirCaixaViewModel excluirVm = new ExcluirCaixaViewModel(
            id,
            caixa.Etiqueta,
            caixa.Cor,
            caixa.DiasDeEmprestimo
        );

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirCaixaViewModel excluirVm)
    {
        List<Revista> revistas =
            repositorioRevista.SelecionarTodos();

        bool possuiRevistas =
            revistas.Any(x => x.CaixaId == excluirVm.Id);

        if (possuiRevistas)
        {
            TempData["MensagemErro"] =
                "Não é possível excluir uma caixa com revistas vinculadas.";

            return RedirectToAction(nameof(Listar));
        }

        repositorioCaixa.Excluir(excluirVm.Id);

        return RedirectToAction(nameof(Listar));
    }
}
