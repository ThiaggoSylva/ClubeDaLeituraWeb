using ClubeDaLeituraWeb.WebApp.Compartilhado.Dominio;
using ClubeDaLeituraWeb.WebApp.Compartilhado.Infra;

namespace ClubeDaLeituraWeb.WebApp.ModuloRevista.Dominio;

public interface IRepositorioRevista : IRepositorio<Revista>
{
    Revista? SelecionarPorTituloEdicao(
        string titulo,
        int numeroEdicao);
}
