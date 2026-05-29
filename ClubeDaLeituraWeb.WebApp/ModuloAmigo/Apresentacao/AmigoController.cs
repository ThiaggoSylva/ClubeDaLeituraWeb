using Microsoft.AspNetCore.Mvc;

using ClubeDaLeituraWeb.WebApp.ModuloAmigo.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloAmigo.Infra;

namespace ClubeDaLeituraWeb.WebApp.ModuloAmigo.Apresentacao;

[Route("amigos")]
public class AmigoController : Controller
{
    private readonly IRepositorioAmigo repositorioAmigo;

    public AmigoController(IRepositorioAmigo repositorioAmigo)
    {
        this.repositorioAmigo = repositorioAmigo;
    }

    [HttpGet]
    [Route("listar")]
    public IActionResult Listar()
{
    List<Amigo> amigos = repositorioAmigo.SelecionarTodos();

    List<ListarAmigosViewModel> vm = amigos.Select(a =>
        new ListarAmigosViewModel(
            a.Id,
            a.Nome,
            a.NomeResponsavel,
            a.Telefone
        )
    ).ToList();

    return View(vm);
}

    [HttpGet]
    [Route("cadastrar")]
    public IActionResult Cadastrar()
    {
        CadastrarAmigoViewModel cadastrarVm =
            new CadastrarAmigoViewModel(
                "",
                "",
                ""
            );

        return View(cadastrarVm);
    }

    [HttpPost]
    [Route("cadastrar")]
    public IActionResult Cadastrar(CadastrarAmigoViewModel cadastrarVm)
    {
        Amigo amigo = new Amigo(
            cadastrarVm.Nome,
            cadastrarVm.NomeResponsavel,
            cadastrarVm.Telefone
        );

        List<string> erros = amigo.Validar();

        Amigo? amigoExistente =
            repositorioAmigo.SelecionarPorNomeTelefone(
                amigo.Nome,
                amigo.Telefone
            );

        if (amigoExistente != null)
            erros.Add("Já existe um amigo com este nome e telefone.");

        foreach (string erro in erros)
            ModelState.AddModelError(string.Empty, erro);

        if (!ModelState.IsValid)
            return View(cadastrarVm);

        repositorioAmigo.Cadastrar(amigo);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    [Route("editar/{id}")]
    public IActionResult Editar(string id)
    {
        Amigo? amigoSelecionado =
            repositorioAmigo.SelecionarPorId(id);

        if (amigoSelecionado == null)
            return RedirectToAction(nameof(Listar));

        EditarAmigoViewModel editarVm =
            new EditarAmigoViewModel(
                amigoSelecionado.Id,
                amigoSelecionado.Nome,
                amigoSelecionado.NomeResponsavel,
                amigoSelecionado.Telefone
            );

        return View(editarVm);
    }

    [HttpPost]
    [Route("editar/{id}")]
    public IActionResult Editar(
        string id,
        EditarAmigoViewModel editarVm)
    {
        Amigo amigoEditado = new Amigo(
            editarVm.Nome,
            editarVm.NomeResponsavel,
            editarVm.Telefone
        );

        amigoEditado.Id = id;

        List<string> erros = amigoEditado.Validar();

        Amigo? amigoExistente =
            repositorioAmigo.SelecionarPorNomeTelefone(
                amigoEditado.Nome,
                amigoEditado.Telefone
            );

        if (amigoExistente != null &&
            amigoExistente.Id != amigoEditado.Id)
        {
            erros.Add("Já existe um amigo com este nome e telefone.");
        }

        foreach (string erro in erros)
            ModelState.AddModelError(string.Empty, erro);

        if (!ModelState.IsValid)
            return View(editarVm);

        repositorioAmigo.Editar(id, amigoEditado);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    [Route("excluir/{id}")]
    public IActionResult Excluir(string id)
    {
        Amigo? amigoSelecionado =
            repositorioAmigo.SelecionarPorId(id);

        if (amigoSelecionado == null)
            return RedirectToAction(nameof(Listar));

        ExcluirAmigoViewModel excluirVm =
            new ExcluirAmigoViewModel(
                amigoSelecionado.Id,
                amigoSelecionado.Nome,
                amigoSelecionado.NomeResponsavel,
                amigoSelecionado.Telefone
            );

        return View(excluirVm);
    }

    [HttpPost]
    [Route("excluir/{id}")]
    public IActionResult ExcluirConfirmado(string id)
    {
        Amigo? amigoSelecionado =
            repositorioAmigo.SelecionarPorId(id);

        if (amigoSelecionado == null)
            return RedirectToAction(nameof(Listar));

        repositorioAmigo.Excluir(id);

        return RedirectToAction(nameof(Listar));
    }
}
