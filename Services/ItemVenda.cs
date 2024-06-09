using System;

namespace SistemaVendas.Services
{
    public class ItemVenda
    {
        private static int proximoId = 1;

        public int Id { get; set; }
        public Produto Produto { get; private set; }
        public int Quantidade { get; private set; }

        public ItemVenda(Produto produto, int quantidade)
        {
            this.Id = proximoId++;
            this.Produto = produto;
            this.Quantidade = quantidade;
        }
    }
}
