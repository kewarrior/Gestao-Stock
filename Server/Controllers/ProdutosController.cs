using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {

        [HttpPost("Inserir")]
        public IActionResult Adicionar(Produto produto)
        {
            if (produto == null)
                return BadRequest("Produto não foi inserido");

            Produto? produtoAnterior = Banco.Produtos.OrderByDescending(p => p.Id).FirstOrDefault();
            if (produtoAnterior != null)
            {
                produto.Id = produtoAnterior.Id + 1;
            }
            else
            {
                produto.Id = 1;
            }
            Banco.Produtos.Add(produto);
            return Ok("Produto Inserido!");
        }




        [HttpPost ("Inserir-ou-Alterar")]
        public IActionResult AdicionarOutAlterar(Produto produto)
        {
            if (produto == null) return BadRequest("Produto não foi inserido");
            
            Produto? produtoJaExiste = Banco.Produtos.Where(p=>p.Id.Equals(produto.Id)).FirstOrDefault();

            if(produtoJaExiste == null)
            {
                Produto? produtoAnterior = Banco.Produtos.OrderByDescending(p => p.Id).FirstOrDefault();
                if (produtoAnterior != null)
                {
                    produto.Id = produtoAnterior.Id + 1;
                }
                else
                {
                    produto.Id = 1;
                }
                Banco.Produtos.Add(produto);
            }
            else
            {
                produtoJaExiste.Nome = produto.Nome;
                produtoJaExiste.Preco = produto.Preco;
                produtoJaExiste.Quantidade = produto.Quantidade;
                produtoJaExiste.Imagem = produto.Imagem;

            }
            return Ok("Produto Inserido ou Atualizado com sucesso!");
        }
        
        


        [HttpGet("Consultar")]
        public IActionResult Listar(string? nome)
        {
            List<Produto> retorno = Banco.Produtos.ToList();

            if (nome != null)
            {
                retorno = Banco.Produtos.Where(p => p.Nome.Contains(nome)).ToList();
            }

            if (retorno.Count > 0)
            {
                return Ok(retorno);
            }
            else
            {
                return NotFound("Produto não encontrado!");
            }
        }

        [HttpGet("Consultar/{id:int}")]
        public IActionResult Consultar(int id)
        {
            Produto? p = Banco.Produtos.Where(p => p.Id == id).FirstOrDefault();
            if (p != null)
            {
                return Ok(p);
            }
            else
            {
                return NotFound("Produto não encontrado");
            }
        }

        [HttpPut("Atualizar")]
        public IActionResult Atualizar(Produto produto)
        {
            if (produto == null)
                return BadRequest("Produto não encontrado");

            Produto? p = Banco.Produtos.Where(p => p.Id == produto.Id).FirstOrDefault();

            if(p == null)
                return BadRequest("Produto não encontrado");

            p.Nome = produto.Nome;
            p.Preco = produto.Preco;
            p.Quantidade = produto.Quantidade;
            p.Imagem = produto.Imagem;
            return Ok("Produto atualizado com Sucesso.");
        }


        [HttpDelete("Apagar/{id:int}")]
        public IActionResult Apagar(int id)
        {
            Produto? p = Banco.Produtos.Where(p => p.Id == id).FirstOrDefault();
            if (p == null)
                BadRequest("Produto não encontrado");
            
            Banco.Produtos.Remove(p);
            return Ok("Produto apagado com sucesso.");

        }

        [HttpPost("AdicionarCarrinho")]
        public IActionResult AdicionarCarrinho(Produto produto)
        {
            if (produto == null)
            {
                return BadRequest("Produto inválido.");
            }
            var produtoExistente = Banco.Carros.FirstOrDefault(p => p.Nome == produto.Nome);

            if (produtoExistente != null)
            {
                if (produtoExistente.Quantidade < produto.Quantidade)
                {
                    produtoExistente.Quantidade++;
                }
                else
                {
                    return BadRequest("Atingiu o máximo de quantidade de stock deste produto.");
                }

            }
            else
            {

                Banco.Carros.Add(new Carro
                {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    Preco = produto.Preco,
                    Quantidade = 1,
                    Imagem = produto.Imagem
                });
            }
            return Ok("Produto adicionado ao carrinho com sucesso!");
        }


        [HttpGet("ConsultarCarrinho")]
        public IActionResult ListarCarrinho()
        {
            List<Carro> retorno = Banco.Carros.ToList();

            if (retorno.Any()) // Verifica se há itens no carrinho
            {
                return Ok(retorno);
            }
            else
            {
                return NotFound("Carrinho vazio!");
            }
        }


        [HttpDelete("ApagarCarrinho/{id:int}")]
        public IActionResult ApagarCarrinho(int id)
        {
            Carro? p = Banco.Carros.Where(p => p.Id == id).FirstOrDefault();
            if (p == null)
            {
                return BadRequest("Produto não encontrado no carrinho");
            }
            else
            {
                if (p.Quantidade > 1)
                {
                    p.Quantidade--; 
                    return Ok("Quantidade do produto descrementada com sucesso.");

                }
                else
                {
                    Banco.Carros.Remove(p);
                    return Ok("Produto removido do carrinho com sucesso.");
                }
            }
        }
    }
}
