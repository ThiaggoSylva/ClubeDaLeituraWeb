using ClubeDaLeituraWeb.WebApp.ModuloRevista.Apresentacao;

public class EditarRevistaViewModel : CadastrarRevistaViewModel
{
    public string Id { get; set; }

    public EditarRevistaViewModel(
        string id,
        string titulo,
        int numeroEdicao,
        DateOnly anoPublicacao,
        string caixaId)
    {
        Id = id;
        Titulo = titulo;
        NumeroEdicao = numeroEdicao;
        AnoPublicacao = anoPublicacao;
        CaixaId = caixaId;
    }
}
