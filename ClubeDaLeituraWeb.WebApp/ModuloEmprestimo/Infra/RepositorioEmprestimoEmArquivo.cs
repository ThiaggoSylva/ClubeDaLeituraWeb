using ClubeDaLeituraWeb.WebApp.Compartilhado.Infra.Arquivos;
using ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Infra;

public class RepositorioEmprestimoEmArquivo
    : RepositorioBaseEmArquivo<Emprestimo>,
      IRepositorioEmprestimo
{

    public RepositorioEmprestimoEmArquivo(ContextoJson contexto)
        : base(contexto)
    {
    }



    protected override List<Emprestimo>
    CarregarRegistros()
    {
    if (contexto.Emprestimos == null)
        contexto.Emprestimos = [];

    return contexto.Emprestimos;
    }

    public List<Emprestimo> SelecionarEmprestimosAbertos()
    {
        return registros
            .Where(x => x.Status != StatusEmprestimo.Concluido)
            .ToList();
    }

    public List<Emprestimo> SelecionarEmprestimosFechados()
    {
        return registros
            .Where(x => x.Status == StatusEmprestimo.Concluido)
            .ToList();
    }

    public bool AmigoPossuiEmprestimoAberto(string amigoId)
    {
        return registros.Any(x =>
            x.Amigo.Id == amigoId.ToString() &&
            x.Status != StatusEmprestimo.Concluido);
    }

    public List<Emprestimo> SelecionarEmprestimosPorAmigo(string amigoId)
    {
        return registros
            .Where(x => x.Amigo.Id == amigoId.ToString())
            .ToList();
    }
}

