using ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Dominio;
using System.Text.Json;

namespace ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Infraestrutura
{
    public class EmprestimoRepositorio
    {
        private readonly string _caminho = "emprestimos.json";

        public List<Emprestimo> ObterTodos()
        {
            if (!File.Exists(_caminho)) return new List<Emprestimo>();
            var json = File.ReadAllText(_caminho);
            return JsonSerializer.Deserialize<List<Emprestimo>>(json) ?? new List<Emprestimo>();
        }

        public void Salvar(List<Emprestimo> emprestimos)
        {
            var json = JsonSerializer.Serialize(emprestimos);
            File.WriteAllText(_caminho, json);
        }
    }
}