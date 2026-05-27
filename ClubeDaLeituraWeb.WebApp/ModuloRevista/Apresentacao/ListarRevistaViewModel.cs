namespace ClubeDaLeituraWeb.WebApp.ModuloRevista.Apresentacao;

public class ListarRevistasViewModel
{
    public string Id { get; set; }

    public string Titulo { get; set; }

    public int NumeroEdicao { get; set; }

    public DateOnly AnoPublicacao { get; set; }

    public string CaixaId { get; set; }

    public ListarRevistasViewModel(
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
