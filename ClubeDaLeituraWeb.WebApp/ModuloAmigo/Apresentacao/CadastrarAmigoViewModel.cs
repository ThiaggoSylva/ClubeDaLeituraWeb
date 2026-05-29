using System.ComponentModel.DataAnnotations;
using ClubeDaLeituraWeb.WebApp.ModuloAmigo.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloAmigo.Apresentacao;

public record CadastrarAmigoViewModel
(
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MinLength(3, ErrorMessage = "O nome deve ter no mínimo 3 caracteres.")]
    [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O responsável é obrigatório.")]
    [MinLength(3, ErrorMessage = "O responsável deve ter no mínimo 3 caracteres.")]
    [MaxLength(100, ErrorMessage = "O responsável deve ter no máximo 100 caracteres.")]
    string NomeResponsavel,

    [Required(ErrorMessage = "O telefone é obrigatório.")]
    [RegularExpression(@"^\d{10,11}$",
        ErrorMessage = "O telefone deve possuir entre 10 e 11 dígitos.")]
    string Telefone
)
{
    public Amigo ToAmigo()
    {
        return new Amigo(
            Nome,
            NomeResponsavel,
            Telefone
        );
    }
}
