using ClubeDaLeituraWeb.WebApp.ModuloAmigo.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloRevista.Dominio;
using System.ComponentModel.DataAnnotations;

namespace ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Apresentacao.Models;

public record CadastrarEmprestimoViewModel(

    [param: Required(
        ErrorMessage = "Selecione um amigo.")]
    string AmigoId,

    [param: Required(
        ErrorMessage = "Selecione uma revista.")]
    string RevistaId
)
{
    public List<Amigo> Amigos
    { get; set; } = [];

    public List<Revista> Revistas
    { get; set; } = [];
}
