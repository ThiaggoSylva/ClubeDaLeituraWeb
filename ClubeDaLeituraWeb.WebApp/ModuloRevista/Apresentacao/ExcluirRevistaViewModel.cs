public class ExcluirRevistaViewModel
{
    public string Id { get; set; }

    public string Titulo { get; set; }

    public int NumeroEdicao { get; set; }

    public ExcluirRevistaViewModel(
        string id,
        string titulo,
        int numeroEdicao)
    {
        Id = id;
        Titulo = titulo;
        NumeroEdicao = numeroEdicao;
    }
}
