namespace ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Dominio
{
    public class Emprestimo
    {
        public int Id { get; set; }
        public string Amigo { get; set; } = "";
        public string Revista { get; set; } = "";
        public DateTime DataEmprestimo { get; set; } = DateTime.Now;
        public DateTime DataDevolucao { get; set; }
        public string Status { get; set; } = "Aberto";

        public bool EstaAtrasado()
        {
            return Status == "Aberto" && DateTime.Now > DataDevolucao;
        }
    }
}
