using ClubeDaLeituraWeb.WebApp.Compartilhado.Dominio;
using ClubeDaLeituraWeb.WebApp.Compartilhado.Infra;

namespace ClubeDaLeituraWeb.WebApp.ModuloRevista.Dominio;

public interface IRepositorioRevista : IRepositorio<Revista>
{
    void Excluir(Revista revista);
    Revista? SelecionarPorTituloEdicao(
        string titulo,
        int numeroEdicao);
}
