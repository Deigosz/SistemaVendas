/*using System;
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
            this.Data = DateTime.Now;
        }



    }
}
*/