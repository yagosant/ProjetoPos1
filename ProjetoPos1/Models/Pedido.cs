namespace ProjetoPos1.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public string ClienteNome { get; set; }
        public List<string> Produtos { get; set; }
        public DateTime DataPedido { get; set; }
        public StatusPedido Status { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
