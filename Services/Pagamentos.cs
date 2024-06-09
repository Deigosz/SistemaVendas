using System;

namespace SistemaVendas.Services
{
    public abstract class Pagamento
    {
        public DateTime Data { get; set; }
        public double Total { get; set; }
    }

    public class PagamentoEspecie : Pagamento
    {
        public double Quantia { get; set; }
        public double Troco { get; set; }
    }

    public class PagamentoCheque : Pagamento
    {
        public long Numero { get; set; }
        public DateTime DataDeposito { get; set; }
        public int Situacao { get; set; }

        public PagamentoCheque() 
        {
            this.Situacao = 0;
        }
    }

    public class PagamentoCartao : Pagamento
    {
        public string DadosTransacao { get; set; }
        public int ResultadoTransacao { get; set; }

        public PagamentoCartao() 
        {
            this.DadosTransacao = "";
        }
    }
}
