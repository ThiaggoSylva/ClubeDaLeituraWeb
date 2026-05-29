using ClubeDaLeituraWeb.WebApp.ModuloRevista.Dominio;

public record EditarRevistaViewModel(
    string id,
    string titulo,
    int numeroEdicao,
    int anoPublicacao,
    string caixaId

)
{
    internal Revista ToRevista()
    {
         return new Revista(
            titulo,
            numeroEdicao,
            anoPublicacao,
            caixaId
        );
    }
}

