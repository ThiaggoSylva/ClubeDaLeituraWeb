using System.ComponentModel.DataAnnotations;
using ClubeDaLeituraWeb.WebApp.ModuloCaixa.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloRevista.Apresentacao;

public record CadastrarRevistaViewModel
(
    [Required]
    [StringLength(100, MinimumLength = 2)]
     string Titulo,

    [Range(1, int.MaxValue)]
     int NumeroEdicao,

    [Required]
     int AnoPublicacao,

    [Required]
     string CaixaId
);
