using System;
using System.Collections.Generic;

namespace SistemaVendas.Services
{
    public class Venda
    {
        private static int proximoId = 1;
        private static List<Venda> vendasRegistradas = new List<Venda>();

        public int Id { get; private set; }
        public DateTime Data { get; set; }
        public double Total { get; private set; }
        public List<ItemVenda> Itens { get; private set; }

        public Venda(DateTime data)
        {
            this.Id = proximoId++;
            this.Data = data;
            this.Itens = new List<ItemVenda>();
        }

        public void AdicionarItem(ItemVenda item, int quantidade)
        {
            if (item.Produto.QtdEstoque >= quantidade)
            {
                item.Produto.QtdEstoque -= quantidade;
                Itens.Add(item);
                Total += item.Produto.Preco * quantidade;
                // Adiciona a venda à lista de vendas registradas
                vendasRegistradas.Add(this);
            }
            else
            {
                throw new Exception("Quantidade insuficiente em estoque para este produto.");
            }
        }

        public static List<Venda> ObterTodasAsVendas()
        {
            return vendasRegistradas;
        }
    }
}
