namespace SistemaVendas.Services
{
    public class ItemVenda
    {
        private static int proximoId = 1;

        public int Id { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }

        public ItemVenda(Produto produto, int Quantidade) 
        {
            this.Id = proximoId++;
            this.Produto = produto;
            this.Quantidade = Quantidade;
        }
    }
}