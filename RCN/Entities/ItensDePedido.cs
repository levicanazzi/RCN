using RCN.Interfaces;
using RCN.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace RCN.Entities
{
    public class ItensDePedido
    {
        public int ID { get; set; }
        public int PedidoID { get; set; }
        public int ProdutoID { get; set; }

        public int Quantidade;

        IItensDePedidoRepository _itensDePedidoRepository;

        public ItensDePedido(IItensDePedidoRepository itensDePedidoRepository)
        {
            _itensDePedidoRepository = itensDePedidoRepository;
        }

        public ItensDePedido()
        {
        }

        //Validação para quantidade não ser negativa
        public int _quantidade
        {
            get { return Quantidade; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("A quantidade do produto de pedido não pode ser negativa.");
                }
                Quantidade = value;
            }
        }


        [Column(TypeName = "decimal(9,2)")]
        public decimal Valor { get; set; }

        //Validação para valor não ser negativo
        public decimal _valor
        {
            get { return Valor; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("O valor do produto de pedido não pode ser negativo.");
                }
                Valor = value;
            }
        }

        // Método para validar um item de pedido
        public static void AdicionarNovoItem(List<ItensDePedido> itens, ItensDePedido novoItem)
        {
            if (itens.Any(item => item.PedidoID == novoItem.PedidoID && item.ProdutoID == novoItem.ProdutoID && item.Valor == novoItem.Valor))
            {
                throw new InvalidOperationException("Não pode haver mais de um item de pedido com o mesmo produto e valor para um pedido.");
            }

            itens.Add(novoItem);
        }

        //Metodo para obter o valor total
        public decimal ValorTotal(int quantidade, decimal valor)
        {
            return quantidade * valor;
        }


        public void AdicionarItensDePedido(ItensDePedido itensDePedido)
        {
            _itensDePedidoRepository.Add(new ItensDePedido()
            {
                ID = itensDePedido.ID,
                PedidoID = itensDePedido.PedidoID,
                ProdutoID = itensDePedido.ProdutoID,
                Quantidade = itensDePedido.Quantidade,
                Valor = itensDePedido.Valor
            });

            _itensDePedidoRepository.Save();
        }

        public void FindItensDePedidoById(int idItensDePedido)
        {
            var itensDePedido = _itensDePedidoRepository.GetAll(x => x.ID == idItensDePedido);
            if (itensDePedido == null)
            {
                Console.WriteLine("Não foi encontrado nenhum resultado na pesquisa.");
            }
            else
            {
                Console.WriteLine(itensDePedido);
            }
        }

        public bool FindIdItensDePedido(int idItensDePedido)
        {

            var itemDePedido = _itensDePedidoRepository.FirstOrDefault(x => x.ID == idItensDePedido);

            if (itemDePedido == null)
            {
                return false;
            }
            else
            {                
               return true;
            }
        }
        public void UpItensDePedido(ItensDePedido itensDePedido)
        {
            var upItensDePedido = new ItensDePedido()
            {
                PedidoID = itensDePedido.PedidoID,
                ProdutoID = itensDePedido.ProdutoID,
                Quantidade = itensDePedido.Quantidade,
                Valor = itensDePedido.Valor
            };

            _itensDePedidoRepository.Save();

        }

        public void DeleteItensDePedido(int idItensDePedido)
        {
            if (idItensDePedido == null)
            {
                throw new Exception("Nenhum item de pedido selecionado!");
            }
            else if (idItensDePedido != null)
            {
                var produto = _itensDePedidoRepository.FirstOrDefault(x => x.ID == idItensDePedido);

                _itensDePedidoRepository.Remove(produto);
                _itensDePedidoRepository.Save();

            }
        }

        public override string ToString()
        {
            return $"ID do Item de Pedido: {ID} - ID do Pedido: {PedidoID} - ID do Produto: {ProdutoID} - Quantidado de itens de Pedido: {Quantidade} - Valor unitário dos Itens de Pedidos: {Valor}.";
        }
    }
}

