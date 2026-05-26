using ClubeDaLeituraWeb.WebApp.Compartilhado.Infra;
using ClubeDaLeituraWeb.WebApp.Compartilhado.Infra.Arquivos;
using ClubeDaLeituraWeb.WebApp.ModuloRevista.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloRevista.Infra;

public class RepositorioRevistaEmArquivo
    : RepositorioBaseEmArquivo<Revista>,
      IRepositorioRevista
{
    public RepositorioRevistaEmArquivo(ContextoJson contexto)
        : base(contexto)
    {
    }

    protected override List<Revista> CarregarRegistros()
    {
        return contexto.Revistas;
    }

    public new bool Editar(string id, Revista revistaAtualizada)
    {
        Revista? revistaSelecionada = SelecionarPorId(id);

        if (revistaSelecionada == null)
            return false;

        revistaSelecionada.Atualizar(revistaAtualizada);

        contexto.Salvar();

        return true;
    }

    public new bool Excluir(Revista revista)
    {
        Revista? revistaSelecionada =
            registros.FirstOrDefault(x => x.Id == revista.Id);

        if (revistaSelecionada == null)
            return false;

        registros.Remove(revistaSelecionada);

        contexto.Salvar();

        return true;
    }

    public Revista? SelecionarPorTituloEdicao(
        string titulo,
        int numeroEdicao)
    {
        return registros.FirstOrDefault(x =>
            x.Titulo.Equals(
                titulo,
                StringComparison.OrdinalIgnoreCase)
            &&
            x.NumeroEdicao == numeroEdicao);
    }

    public new List<Revista> Filtrar(Predicate<Revista> filtro)
    {
        return registros.FindAll(filtro);
    }
}
