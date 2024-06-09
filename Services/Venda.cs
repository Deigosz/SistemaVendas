using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVendas.Services
{
    public class Venda
    {
        private static int proximoId = 1;

        public int Id { get; private set; }
        public DateTime Data { get; private set; }
        private double Total { get; }
        public List<ItemVenda> Itens { get; private set; }

        public Venda(DateTime)
        {
            this.Id = proximoId++;
            this.Data = DateTime.Now;
            this.Itens = new List<ItemVenda>();
        }

        public void AdicionarItem(Produto produto, int quantidade)
        {
            if (produto == null)
            {
                throw new ArgumentNullException(nameof(produto));
            }

            if (quantidade <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantidade), "A quantidade deve ser maior que zero.");
            }
            Itens.Add(new ItemVenda(produto, quantidade));
        }
    }
}
