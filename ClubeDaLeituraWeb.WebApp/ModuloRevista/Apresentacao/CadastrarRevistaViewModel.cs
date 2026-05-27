using System.ComponentModel.DataAnnotations;

namespace ClubeDaLeituraWeb.WebApp.ModuloRevista.Apresentacao;

public class CadastrarRevistaViewModel
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Titulo { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int NumeroEdicao { get; set; }

    [Required]
    public DateOnly AnoPublicacao { get; set; }

    [Required]
    public string CaixaId { get; set; } = string.Empty;
}
