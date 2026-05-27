using ClubeDaLeituraWeb.WebApp.Compartilhado.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloCaixa.Dominio;

public interface IRepositorioCaixa : IRepositorio<Caixa>
{
    Caixa? SelecionarPorEtiqueta(string etiqueta);
}
