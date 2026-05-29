using ClubeDaLeituraWeb.WebApp.Compartilhado.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloAmigo.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloRevista.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Dominio;

public class Emprestimo : EntidadeBase<Emprestimo>
{
    public Amigo Amigo { get; set; } = null!;

    public Revista Revista { get; set; } = null!;

    public DateTime DataEmprestimo { get; set; }

    public DateTime DataDevolucao { get; set; }

    public DateTime? DataDevolucaoRealizada { get; set; }
    public StatusEmprestimo Status
    {
    get
        {
        if (DataDevolucaoRealizada != null)
            return StatusEmprestimo.Concluido;

        if (DateTime.Now > DataDevolucao)
            return StatusEmprestimo.Atrasado;

        return StatusEmprestimo.Aberto;
        }
    }


    public Emprestimo()
    {
    }

    public override void Atualizar(Emprestimo registroEditado)
    {
        Amigo = registroEditado.Amigo;

        Revista = registroEditado.Revista;

        DataEmprestimo = registroEditado.DataEmprestimo;

        DataDevolucao = registroEditado.DataDevolucao;

        DataDevolucaoRealizada =
            registroEditado.DataDevolucaoRealizada;
    }

    public override List<string> Validar()
    {
        var erros = new List<string>();

        if (Amigo == null)
            erros.Add("O campo Amigo é obrigatório.");

        if (Revista == null)
            erros.Add("O campo Revista é obrigatório.");

        if (DataEmprestimo == default)
            erros.Add("A Data de Empréstimo é obrigatória.");

        if (DataDevolucao == default)
            erros.Add("A Data de Devolução é obrigatória.");

        return erros;
    }
}

