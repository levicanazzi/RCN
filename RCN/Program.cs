using RCN.Entities;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata;

namespace RCN
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var pedido = new Pedidos();
            var produtos = new Produtos();
            var itensDePedido = new ItensDePedido();
            var processo = 0;

            while (processo != 5)
            {
                try
                {
                    Console.WriteLine("CRUD usando LINQ e Entity Framework!");
                    Console.WriteLine();
                    Console.WriteLine("Pressione 1 caso deseje Adicionar.");
                    Console.WriteLine("Pressione 2 caso deseje Atualizar.");
                    Console.WriteLine("Pressione 3 caso deseje Procurar.");
                    Console.WriteLine("Pressione 4 caso deseje Deletar.");
                    Console.WriteLine("Pressione 5 para sair.");
                    Console.WriteLine();

                    processo = int.Parse(Console.ReadLine());

                    Console.WriteLine();

                    switch (processo)
                    {
                        //Caso Adicionar 
                        case 1:
                            Console.Write("ID do Produto: ");
                            int idProduto = int.Parse(Console.ReadLine());

                            Console.Write("Nome do Produto:");
                            string nomeProduto = Console.ReadLine();

                            Console.Write("Categoria do Produto sendo 1 - Perecível, 2 - Nao Perecível: ");
                            int categoriaProduto = int.Parse(Console.ReadLine());

                            //Valida Categoria da Classe Produtos
                            while (produtos.ValidarCategoriaProduto(categoriaProduto) == false)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Insira categoria válida!");
                                Console.Write("Categoria do Produto sendo 1 - Perecível, 2 - Nao Perecível: ");
                                categoriaProduto = int.Parse(Console.ReadLine());
                            }

                            CategoriaProdutos categoriaValida = (CategoriaProdutos)categoriaProduto;

                            //Criando Objeto da classe Produtos
                            var addProduto = new Produtos()
                            {
                                ID = idProduto,
                                Nome = nomeProduto,
                                Categoria = categoriaValida
                            };

                            Console.Write("ID do Pedido: ");
                            int idPedido = int.Parse(Console.ReadLine());

                            Console.Write("Identificador(Ex: P_A000_C): ");
                            string identificadorPedido = Console.ReadLine();

                            //Valida Identificador da Classe Pedidos
                            while (pedido.ValidarIdentificador(identificadorPedido) == false)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Insira um Identificador Válido no formato P_[letra, seguida de 3 números]_C. Exemplo:'P_A000_C' ");
                                Console.Write("Identificador(Ex: P_A000_C): ");
                                identificadorPedido = Console.ReadLine();
                            }

                            Console.Write("Descrição: ");
                            string descricaoPedido = Console.ReadLine();

                            Console.Write("ID do Itens de Pedido: ");
                            int idItens = int.Parse(Console.ReadLine());

                            Console.Write("Quantidade de Itens: ");
                            int quantidadeItens = int.Parse(Console.ReadLine());

                            Console.Write("Valor unitário dos Itens: ");
                            int valorItens = int.Parse(Console.ReadLine());

                            //Criando Objeto da classe ItensDePedido
                            var addItensDePedido = new ItensDePedido()
                            {
                                ID = idItens,
                                PedidoID = idPedido,
                                ProdutoID = idProduto,
                                Quantidade = quantidadeItens,
                                Valor = valorItens
                            };

                            //Utilizando método ValorTotal para ser inserido no Objeto da Classe Pedidos
                            decimal valorTotal = addItensDePedido.ValorTotal(quantidadeItens, valorItens);

                            //Criando Objeto da Classe Pedidos
                            var addPedidos = new Pedidos()
                            {
                                ID = idPedido,
                                Identificador = identificadorPedido,
                                Descricao = descricaoPedido,
                                ValorTotal = valorTotal
                            };

                            //Adicionando ao Banco de Dados
                            produtos.AdicionarProdutos(addProduto);
                            itensDePedido.AdicionarItensDePedido(addItensDePedido);
                            pedido.AdicionarPedidos(addPedidos);

                            break;

                        //Caso Atualizar
                        case 2:
                            Console.Write("ID do Produto a ser atualizado: ");
                            int idProdProcurado = int.Parse(Console.ReadLine());

                            //Utilizando método para Buscar um ID válido da Classe Produtos
                            while (produtos.FindIdProdutos(idProdProcurado) == false)
                            {
                                Console.WriteLine("Produto inexistente");
                                Console.Write("ID do Produto a ser atualizado: ");
                                idProdProcurado = int.Parse(Console.ReadLine());
                            }

                            Console.Write("Nome do Produto a ser atualizado:");
                            var upNomeProduto = Console.ReadLine();

                            Console.Write("Categoria do Produto a ser atualizada sendo 1 - Perecível, 2 - Nao Perecível: ");
                            int upCategoriaProduto = int.Parse(Console.ReadLine());

                            //Valida Categoria da Classe Produtos
                            while (produtos.ValidarCategoriaProduto(upCategoriaProduto) == false)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Insira categoria válida!");
                                Console.Write("Categoria do Produto sendo 1 - Perecível, 2 - Nao Perecível: ");
                                upCategoriaProduto = int.Parse(Console.ReadLine());
                            }

                            var upCategoriaProdutoValida = (CategoriaProdutos)upCategoriaProduto;

                            //Criando Objeto da Classe Produtos
                            var upProduto = new Produtos()
                            {
                                ID = idProdProcurado,
                                Nome = upNomeProduto,
                                Categoria = upCategoriaProdutoValida
                            };

                            Console.Write("ID do Pedido: ");
                            int idPedidoProcurado = int.Parse(Console.ReadLine());

                            //Utilizando metodo para Buscar um ID válido da Classe Pedidos
                            while (pedido.FindIdPedidos(idPedidoProcurado) == false)
                            {
                                Console.WriteLine("Pedido inexistente");
                                Console.Write("ID do Pedido a ser atualizado: ");
                                idPedidoProcurado = int.Parse(Console.ReadLine());
                            }

                            Console.Write("Identificador a ser atualizado(Ex: P_A000_C): ");
                            string upIdentificadorPedido = Console.ReadLine();

                            //Utilizando metodo para Validar o Identificador da Classe Pedidos
                            while (pedido.ValidarIdentificador(upIdentificadorPedido) == false)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Insira um Identificador Válido no formato P_[letra, seguida de 3 números]_C. Exemplo:'P_A000_C' ");
                                Console.Write("Identificador(Ex: P_A000_C): ");
                                upIdentificadorPedido = Console.ReadLine();
                            }

                            Console.Write("Descrição a ser atualizada: ");
                            string upDescricaoPedido = Console.ReadLine();

                            Console.Write("ID do Itens de Pedido: ");
                            int idItensProcurado = int.Parse(Console.ReadLine());

                            //Utilizando metodo para Buscar um ID válido da Classe ItensDePedido
                            while (itensDePedido.FindIdItensDePedido(idItensProcurado) == false)
                            {
                                Console.WriteLine("Itens de pedido inexistente");
                                Console.Write("ID do Itens de pedido a ser atualizado: ");
                                idItensProcurado = int.Parse(Console.ReadLine());
                            }

                            Console.Write("Quantidade de Itens a ser atualizados: ");
                            int upQuantidadeItens = int.Parse(Console.ReadLine());

                            Console.Write("Valor unitário a ser atualizado dos Itens: ");
                            int upValorItens = int.Parse(Console.ReadLine());

                            //Criando Objeto da Classe ItensDePedido
                            var upItensDePedido = new ItensDePedido()
                            {
                                ID = idItensProcurado,
                                PedidoID = idPedidoProcurado,
                                ProdutoID = idProdProcurado,
                                Quantidade = upQuantidadeItens,
                                Valor = upValorItens
                            };

                            //Utilizando método ValorTotal para ser inserido no Objeto da Classe Pedidos
                            decimal upValorTotal = itensDePedido.ValorTotal(upQuantidadeItens, upValorItens);

                            //Criando Objeto da Classe Pedidos
                            var upPedidos = new Pedidos()
                            {
                                ID = idPedidoProcurado,
                                Identificador = upIdentificadorPedido,
                                Descricao = upDescricaoPedido,
                                ValorTotal = upValorTotal
                            };

                            //Atualizando Banco através dos métodos Update
                            produtos.UpProdutos(upProduto);
                            itensDePedido.UpItensDePedido(upItensDePedido);
                            pedido.UpPedidos(upPedidos);

                            break;
                        
                        //Caso Procurar
                        case 3:
                            Console.Write("ID do Produto a ser Procurado: ");
                            int idProdBusca = int.Parse(Console.ReadLine());

                            //Utilizando método para Buscar um ID válido da Classe Produtos
                            while (produtos.FindIdProdutos(idProdBusca) == false)
                            {
                                Console.WriteLine("Produto inexistente");
                                Console.Write("ID do Produto a ser atualizado: ");
                                idProdBusca = int.Parse(Console.ReadLine());
                            }

                            Console.Write("ID do Pedido a ser Procurado: ");
                            int idPedidoBusca = int.Parse(Console.ReadLine());

                            //Utilizando metodo para Buscar um ID válido da Classe Pedidos
                            while (pedido.FindIdPedidos(idPedidoBusca) == false)
                            {
                                Console.WriteLine("Pedido inexistente");
                                Console.Write("ID do Pedido a ser atualizado: ");
                                idPedidoBusca = int.Parse(Console.ReadLine());
                            }

                            Console.Write("ID do Itens de Pedido a ser Procurado: ");
                            int idItemDePedidoBusca = int.Parse(Console.ReadLine());

                            //Utilizando metodo para Buscar um ID válido da Classe ItensDePedido
                            while (itensDePedido.FindIdItensDePedido(idItemDePedidoBusca) == false)
                            {
                                Console.WriteLine("Itens de pedido inexistente");
                                Console.Write("ID do Itens de pedido a ser atualizado: ");
                                idItemDePedidoBusca = int.Parse(Console.ReadLine());
                            }

                            //Buscando no Banco de Dados os dados de cada Classe
                            produtos.FindProdutosById(idProdBusca);
                            itensDePedido.FindItensDePedidoById(idItemDePedidoBusca);
                            pedido.FindPedidosById(idPedidoBusca);

                            break;

                        //Casso Deletar
                        case 4:
                            Console.WriteLine("Qual Pedido deseja deletar?");
                            int idPedidoDelete = int.Parse(Console.ReadLine());

                            Console.WriteLine("Qual Produto deseja deletar?");
                            int idProdutoDelete = int.Parse(Console.ReadLine());

                            Console.WriteLine("Qual Item de Produto deseja deletar?");
                            int idItensDeProdutoDelete = int.Parse(Console.ReadLine());

                            pedido.DeletePedidos(idPedidoDelete);
                            produtos.DeleteProdutos(idProdutoDelete);
                            itensDePedido.DeleteItensDePedido(idItensDeProdutoDelete);

                            Console.WriteLine("Deletados com sucesso: ");

                            break;

                        //Caso Encerrar Programa
                        case 5:
                            Console.WriteLine("Encerrando o Programa.");
                            break;
                        default:
                            Console.WriteLine("Tecla não correspondente!");
                            Console.WriteLine("Favor inserir uma opção válida.");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                Console.WriteLine();
            }
        }
    }
}
