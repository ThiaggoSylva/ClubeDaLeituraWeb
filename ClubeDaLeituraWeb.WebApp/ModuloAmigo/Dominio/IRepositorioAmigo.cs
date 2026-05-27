using ClubeDaLeituraWeb.WebApp.Compartilhado.Dominio;
using ClubeDaLeituraWeb.WebApp.Compartilhado.Infra;

namespace ClubeDaLeituraWeb.WebApp.ModuloAmigo.Dominio;

public interface IRepositorioAmigo : IRepositorio<Amigo>
{
    Amigo? SelecionarPorNomeTelefone(
        string nome,
        string telefone);
}
