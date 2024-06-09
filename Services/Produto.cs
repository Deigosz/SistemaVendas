using System;

namespace SistemaVendas.Services
{
    public class Produto
    {
        private static int proximoId = 1;

        public int Id { get; private set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
        public int QtdEstoque { get; set; }

        public Produto(string nome, double preco, int qtdEstoque)
        {
            this.Id = proximoId++;
            this.Nome = nome;
            this.Preco = preco;
            this.QtdEstoque = qtdEstoque;
        }
    }
}
