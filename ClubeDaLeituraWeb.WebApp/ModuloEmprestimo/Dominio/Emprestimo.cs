using ClubeDaLeituraWeb.WebApp.Compartilhado.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Dominio;

public class Emprestimo : EntidadeBase<Emprestimo>
{
    public string AmigoId { get; set; }
    public string RevistaId { get; set; }

    public DateTime DataEmprestimo { get; set; }
    public DateTime DataDevolucao { get; set; }

    public StatusEmprestimo Status { get; set; }

    public Emprestimo()
    {
        Id = Guid.NewGuid().ToString();

        DataEmprestimo = DateTime.Now;

        Status = StatusEmprestimo.Aberto;
        AmigoId = string.Empty;
        RevistaId = string.Empty;
    }

    public override List<string> Validar()
    {
        List<string> erros = new();

        if (string.IsNullOrWhiteSpace(AmigoId))
            erros.Add("O amigo é obrigatório.");

        if (string.IsNullOrWhiteSpace(RevistaId))
            erros.Add("A revista é obrigatória.");

        return erros;
    }

    public override void Atualizar(Emprestimo registroEditado)
    {
        AmigoId = registroEditado.AmigoId;
        RevistaId = registroEditado.RevistaId;
        DataEmprestimo = registroEditado.DataEmprestimo;
        DataDevolucao = registroEditado.DataDevolucao;
        Status = registroEditado.Status;
    }
}
