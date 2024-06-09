namespace SistemaVendas.Services
{
    public class Produto
    {
        private static int proximoId = 1;

        public int Id { get; private set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
        public int QtdEstoque { get; set; }

        public Produto(string Nome, double Preco, int QtdEstoque)
        {
            this.Id = proximoId++;
            this.Nome = Nome;
            this.Preco = Preco;
            this.QtdEstoque = QtdEstoque;
        }
    }
}