using System.Collections.Generic;
using System.Linq;
using Shared.Models;



namespace Client.Services
{
    public class CarrinhoService
    {
        private List<Produto> item = new List<Produto>();

        public void AdicionarProduto(Produto produto)
        {
            item.Add(produto);
        }

        public void RemoverProduto(Produto produto)
        {
            item.Remove(produto);
        }

        public List<Produto> ObterProdutos()
        {
            return item;
        }

        public decimal ObterTotal()
        {
            return (decimal)item.Sum(p => p.Preco);
        }
    }
}
