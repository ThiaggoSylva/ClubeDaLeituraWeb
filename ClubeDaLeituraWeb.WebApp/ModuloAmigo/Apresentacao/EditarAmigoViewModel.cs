using System.ComponentModel.DataAnnotations;
using ClubeDaLeituraWeb.WebApp.ModuloAmigo.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloAmigo.Apresentacao;

public record EditarAmigoViewModel
(
    string Id,

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MinLength(3)]
    [MaxLength(100)]
    string Nome,

    [Required(ErrorMessage = "O responsável é obrigatório.")]
    [MinLength(3)]
    [MaxLength(100)]
    string NomeResponsavel,

    [Required(ErrorMessage = "O telefone é obrigatório.")]
    [RegularExpression(@"^\d{10,11}$")]
    string Telefone
)
{
    public Amigo ToAmigo()
    {
        Amigo amigo = new(
            Nome,
            NomeResponsavel,
            Telefone
        );

        amigo.Id = Id;

        return amigo;
    }
}
