using ClubeDaLeituraWeb.WebApp.Compartilhado.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Dominio;

public interface IRepositorioEmprestimo
    : IRepositorio<Emprestimo>
{
    List<Emprestimo> SelecionarEmprestimosAbertos();

    List<Emprestimo> SelecionarEmprestimosFechados();

    bool AmigoPossuiEmprestimoAberto(
        string amigoId);

    List<Emprestimo> SelecionarEmprestimosPorAmigo(
        string amigoId);
}
