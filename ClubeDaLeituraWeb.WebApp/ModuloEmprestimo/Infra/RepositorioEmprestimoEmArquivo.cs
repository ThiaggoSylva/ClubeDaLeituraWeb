using ClubeDaLeituraWeb.WebApp.Compartilhado.Infra;
using ClubeDaLeituraWeb.WebApp.Compartilhado.Infra.Arquivos;
using ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Infra;

public class RepositorioEmprestimoEmArquivo
    : RepositorioBaseEmArquivo<Emprestimo>, IRepositorioEmprestimo
{
    public RepositorioEmprestimoEmArquivo(ContextoJson contexto)
        : base(contexto)
    {
    }

    protected override List<Emprestimo> CarregarRegistros()
    {
        return contexto.Emprestimos;
    }

    public List<Emprestimo> SelecionarAbertos()
    {
        return registros
            .Where(x => x.Status == StatusEmprestimo.Aberto ||
                        x.Status == StatusEmprestimo.Atrasado)
            .ToList();
    }

    public List<Emprestimo> SelecionarConcluidos()
    {
        return registros
            .Where(x => x.Status == StatusEmprestimo.Concluido)
            .ToList();
    }

    public Emprestimo? SelecionarEmprestimoAtivoAmigo(string amigoId)
    {
        return registros.FirstOrDefault(x =>
            x.AmigoId == amigoId &&
            x.Status != StatusEmprestimo.Concluido);
    }
}
