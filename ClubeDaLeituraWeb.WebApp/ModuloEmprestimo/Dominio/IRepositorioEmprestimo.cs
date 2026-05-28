using ClubeDaLeituraWeb.WebApp.Compartilhado.Dominio;
using ClubeDaLeituraWeb.WebApp.Compartilhado.Infra;

namespace ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Dominio;

public interface IRepositorioEmprestimo
    : IRepositorio<Emprestimo>
{
    List<Emprestimo> SelecionarAbertos();

    List<Emprestimo> SelecionarConcluidos();

    Emprestimo? SelecionarEmprestimoAtivoAmigo(string amigoId);
}
