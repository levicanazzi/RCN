using RCN.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace RCN.Entities
{
    public class Produtos
    {
        public int ID { get; set; }
        [StringLength(255, ErrorMessage = "O Campo Identificador excedeu o limite de caracteres.")]
        public string Nome { get; set; }
        public CategoriaProdutos Categoria { get; set; }

        IProdutosRepository _produtosRepository;

        public Produtos()
        {
        }

        public Produtos(IProdutosRepository produtosRepository)
        {
            _produtosRepository = produtosRepository;
        }

        //Validando Categorias do Produto 
        public bool ValidarCategoriaProduto(int categoriaProdutos)
        {
            if (categoriaProdutos == 1 || categoriaProdutos == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void AdicionarProdutos(Produtos produtos)
        {
            _produtosRepository.Add(new Produtos()
            {
                ID = produtos.ID,
                Nome = produtos.Nome,
                Categoria = produtos.Categoria

            });

            _produtosRepository.Save();
        }

        public void FindProdutosById(int idProduto)
        {
            var produto = _produtosRepository.GetAll(x => x.ID == idProduto);

            if (produto == null)
            {
                Console.WriteLine("Não foi encontrado nenhum resultado na pesquisa.");
            }
            else
            {
                Console.WriteLine(produto);
            }
        }

        public bool FindIdProdutos(int idProduto)
        {

            var produto = _produtosRepository.FirstOrDefault(x => x.ID == idProduto);

            if (produto == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void UpProdutos(Produtos produto)
        {
            var upProduto = new Produtos()
            {
                Nome = produto.Nome,
                Categoria = produto.Categoria
            };

            _produtosRepository.Save();

        }

        public void DeleteProdutos(int idProduto)
        {
            if (idProduto == null)
            {
                throw new Exception("Nenhum produto selecionado!");
            }
            else if (idProduto != null)
            {
                var produto = _produtosRepository.FirstOrDefault(x => x.ID == idProduto);

                _produtosRepository.Remove(produto);
                _produtosRepository.Save();

            }
        }

        public override string ToString()
        {
            return $"ID do produto: {ID} - Nome do Produto: {Nome} - Categoria do Produto: {Categoria}.";
        }
    }
}
