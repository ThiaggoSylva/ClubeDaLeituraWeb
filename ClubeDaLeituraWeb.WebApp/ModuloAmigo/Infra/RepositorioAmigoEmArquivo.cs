using ClubeDaLeituraWeb.WebApp.Compartilhado.Infra;
using ClubeDaLeituraWeb.WebApp.Compartilhado.Infra.Arquivos;
using ClubeDaLeituraWeb.WebApp.ModuloAmigo.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloAmigo.Infra;

public class RepositorioAmigoEmArquivo
    : RepositorioBaseEmArquivo<Amigo>,
    IRepositorioAmigo
{
    public RepositorioAmigoEmArquivo(ContextoJson contexto)
        : base(contexto)
    {
    }

    protected override List<Amigo> CarregarRegistros()
    {
        return contexto.Amigos;
    }

    public bool Editar(string id, Amigo amigoAtualizado)
    {
        Amigo? amigoSelecionado =
            SelecionarPorId(id);

        if (amigoSelecionado == null)
            return false;

        amigoSelecionado.Atualizar(amigoAtualizado);

        contexto.Salvar();

        return true;
    }

    public new bool Excluir(Amigo amigo)
    {
        Amigo? amigoSelecionado =
            registros.FirstOrDefault(x => x.Id == amigo.Id);

        if (amigoSelecionado == null)
            return false;

        registros.Remove(amigoSelecionado);

        contexto.Salvar();

        return true;
    }

    public Amigo? SelecionarPorNomeTelefone(
        string nome,
        string telefone)
    {
        return registros.FirstOrDefault(x =>
            x.Nome.Equals(nome,
            StringComparison.OrdinalIgnoreCase)
            &&
            x.Telefone == telefone);
    }
}
