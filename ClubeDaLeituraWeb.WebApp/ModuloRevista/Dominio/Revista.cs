using ClubeDaLeituraWeb.WebApp.Compartilhado.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloRevista.Dominio;

public class Revista : EntidadeBase<Revista>
{
    public string Titulo { get; set; }

    public int NumeroEdicao { get; set; }

    public int AnoPublicacao { get; set; }

    public string CaixaId { get; set; }
    public string Status { get; set; } = "Disponível";

    public Revista(
        string titulo,
        int numeroEdicao,
        int anoPublicacao,
        string caixaId)
    {
        Titulo = titulo;
        NumeroEdicao = numeroEdicao;
        AnoPublicacao = anoPublicacao;
        CaixaId = caixaId;

        Status = "Disponível";
    }

    public Revista()
    {
    }

    public override void Atualizar(Revista registroEditado)
    {
        Titulo = registroEditado.Titulo;
        NumeroEdicao = registroEditado.NumeroEdicao;
        AnoPublicacao = registroEditado.AnoPublicacao;
        CaixaId = registroEditado.CaixaId;
    }

    public override List<string> Validar()
    {
        List<string> erros = new();

        if (string.IsNullOrWhiteSpace(Titulo))
            erros.Add("O título é obrigatório.");

        if (Titulo.Length < 2 || Titulo.Length > 100)
            erros.Add("O título deve conter entre 2 e 100 caracteres.");

        if (NumeroEdicao <= 0)
            erros.Add("O número da edição deve ser positivo.");

        if (CaixaId == null)
            erros.Add("A caixa é obrigatória.");

        return erros;
    }

}
