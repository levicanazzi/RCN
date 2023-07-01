using Newtonsoft.Json.Linq;
using RCN.Interfaces;
using RCN.Repository;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace RCN.Entities
{
    public class Pedidos
    {
        public int ID { get; set; }
        [StringLength(255, ErrorMessage = "O Campo Identificador excedeu o limite de caracteres.")]
        public string Identificador { get; set; }
        [StringLength(1000, ErrorMessage = "O Campo Descricao excedeu o limite de caracteres.")]
        public string? Descricao { get; set; }
        [Column(TypeName = "decimal(21,2)")]
        public decimal ValorTotal { get; set; }

        IPedidosRepository _pedidosRepository;

        //validação do campo Identificador para o padrão "P_[letra, seguida de 3 números]_C"
        public  bool ValidarIdentificador(string identificador)
        {
            if (Regex.IsMatch(identificador, @"^P_[A-Z]\d{3}_C$"))
            {
                 return true;
            }
            else
            {
                return false;                                
            }
        }

        public Pedidos()
        {
        }

        public Pedidos(IPedidosRepository pedidosRepository)
        {
            _pedidosRepository = pedidosRepository;
        }

        public void AdicionarPedidos(Pedidos pedidos)
        {
            _pedidosRepository.Add(new Pedidos()
            {
                ID = pedidos.ID,
                Descricao = pedidos.Descricao,
                Identificador = pedidos.Identificador,
                ValorTotal = pedidos.ValorTotal
            });

            _pedidosRepository.Save();
        }

        public void FindPedidosById(int idPedido)
        {
            var pedido = _pedidosRepository.GetAll(x => x.ID == idPedido);
            if (pedido == null)
            {
                Console.WriteLine("Não foi encontrado nenhum resultado na pesquisa.");
            }
            else
            {
                Console.WriteLine(pedido);
            }
        }
        public bool FindIdPedidos(int idPedidos)
        {

            var pedido = _pedidosRepository.FirstOrDefault(x => x.ID == idPedidos);

            if (pedido == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void UpPedidos(Pedidos pedido)
        {
            var upPedido = new Pedidos()
            {
                Descricao = pedido.Descricao,
                Identificador = pedido.Identificador,
                ValorTotal = pedido.ValorTotal
            };

            _pedidosRepository.Save();

        }

        public void DeletePedidos(int idPedido)
        {
            if (idPedido == null)
            {
                throw new Exception("Nenhum pedido selecionado!");
            }
            else if (idPedido != null)
            {
                var pedido = _pedidosRepository.FirstOrDefault(x => x.ID == idPedido);

                _pedidosRepository.Remove(pedido);
                _pedidosRepository.Save();

            }
        }

        public override string ToString()
        {
            return $"ID do pedido: {ID} - Identificador do Pedido: {Identificador} - Valor Total do Pedido {ValorTotal} - Descrição do Pedido: {Descricao}.";
        }
    }
}
