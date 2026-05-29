using ClubeDaLeituraWeb.WebApp.Compartilhado.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloAmigo.Dominio;

public class Amigo : EntidadeBase<Amigo>
{
    public string Nome { get; set; } = string.Empty;

    public string NomeResponsavel { get; set; } = string.Empty;

    public string Telefone { get; set; } = string.Empty;

    public Amigo()
    {
    }

    public Amigo(
        string nome,
        string nomeResponsavel,
        string telefone)
    {
        Id = Guid.NewGuid().ToString();

        Nome = nome;
        NomeResponsavel = nomeResponsavel;
        Telefone = telefone;
    }
    public override void Atualizar(Amigo registroEditado)
    {
        Nome = registroEditado.Nome;
        NomeResponsavel = registroEditado.NomeResponsavel;
        Telefone = registroEditado.Telefone;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo Nome é obrigatório.");

        if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O Nome deve conter entre 3 e 100 caracteres.");

        if (string.IsNullOrWhiteSpace(NomeResponsavel))
            erros.Add("O campo Nome do Responsável é obrigatório.");

        if (NomeResponsavel.Length < 3 || NomeResponsavel.Length > 100)
            erros.Add("O Nome do Responsável deve conter entre 3 e 100 caracteres.");

        if (string.IsNullOrWhiteSpace(Telefone))
            erros.Add("O campo Telefone é obrigatório.");

        string telefoneLimpo =
            Telefone.Replace("(", "")
                     .Replace(")", "")
                     .Replace("-", "")
                     .Replace(" ", "");

        bool telefoneValido =
            telefoneLimpo.Length >= 10
            &&
            telefoneLimpo.Length <= 11
            &&
            telefoneLimpo.All(char.IsDigit);

        if (!telefoneValido)
            erros.Add("O telefone deve possuir entre 10 e 11 dígitos.");

        return erros;
    }
}
