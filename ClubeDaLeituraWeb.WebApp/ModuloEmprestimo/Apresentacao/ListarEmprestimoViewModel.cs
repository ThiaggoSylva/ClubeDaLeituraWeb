using ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Apresentacao.Models;

public record ListarEmprestimoViewModel(
    string Id,
    string Amigo,
    string Revista,
    DateTime DataEmprestimo,
    DateTime DataDevolucao,
    StatusEmprestimo Status);


