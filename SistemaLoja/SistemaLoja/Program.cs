

using System;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using SistemaLoja.Lab12_ConexaoSQLServer;

namespace SistemaLoja
{
    // ===============================================
    // MODELOS DE DADOS
    // ===============================================

    // ===============================================
    // CLASSE DE CONEXÃO
    // ===============================================

    // ===============================================
    // REPOSITÓRIO DE PRODUTOS
    // ===============================================

    // ===============================================
    // REPOSITÓRIO DE PEDIDOS
    // ===============================================

    // ===============================================
    // CLASSE PRINCIPAL
    // ===============================================
    
    class Program
    {
        static void Main(string[] args)
        {
            // IMPORTANTE: Antes de executar, crie o banco de dados!
            // Execute o script SQL fornecido no arquivo setup.sql
            
            Console.WriteLine("=== LAB 12 - CONEXÃO SQL SERVER ===\n");
            
            var produtoRepo = new ProdutoRepository();
            var pedidoRepo = new PedidoRepository();
            
            bool continuar = true;
            
            while (continuar)
            {
                MostrarMenu();
                string opcao = Console.ReadLine();
                
                try
                {
                    switch (opcao)
                    {
                        case "1":
                            produtoRepo.ListarTodosProdutos();
                            break;
                            
                        case "2":
                            InserirNovoProduto(produtoRepo);
                            break;
                            
                        case "3":
                            AtualizarProdutoExistente(produtoRepo);
                            break;
                            
                        case "4":
                            DeletarProdutoExistente(produtoRepo);
                            break;
                            
                        case "5":
                            ListarPorCategoria(produtoRepo);
                            break;
                            
                        case "6":
                            CriarNovoPedido(pedidoRepo);
                            break;
                            
                        case "7":
                            ListarPedidosDeCliente(pedidoRepo);
                            break;
                            
                        case "8":
                            DetalhesDoPedido(pedidoRepo);
                            break;
                            
                        case "0":
                            continuar = false;
                            break;
                            
                        default:
                            Console.WriteLine("Opção inválida!");
                            break;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"\n❌ Erro SQL: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n❌ Erro: {ex.Message}");
                }
                
                if (continuar)
                {
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            
            Console.WriteLine("\nPrograma finalizado!");
        }

        static void MostrarMenu()
        {
            Console.WriteLine("\n╔════════════════════════════════════╗");
            Console.WriteLine("║       MENU PRINCIPAL               ║");
            Console.WriteLine("╠════════════════════════════════════╣");
            Console.WriteLine("║  PRODUTOS                          ║");
            Console.WriteLine("║  1 - Listar todos os produtos      ║");
            Console.WriteLine("║  2 - Inserir novo produto          ║");
            Console.WriteLine("║  3 - Atualizar produto             ║");
            Console.WriteLine("║  4 - Deletar produto               ║");
            Console.WriteLine("║  5 - Listar por categoria          ║");
            Console.WriteLine("║                                    ║");
            Console.WriteLine("║  PEDIDOS                           ║");
            Console.WriteLine("║  6 - Criar novo pedido             ║");
            Console.WriteLine("║  7 - Listar pedidos de cliente     ║");
            Console.WriteLine("║  8 - Detalhes de um pedido         ║");
            Console.WriteLine("║                                    ║");
            Console.WriteLine("║  0 - Sair                          ║");
            Console.WriteLine("╚════════════════════════════════════╝");
            Console.Write("\nEscolha uma opção: ");
        }

        // TODO: Implemente os métodos auxiliares abaixo
        
        static void InserirNovoProduto(ProdutoRepository repo)
        {
            Console.WriteLine("\n=== INSERIR NOVO PRODUTO ===");
            
            // TODO: Solicite os dados do produto ao usuário
            Console.Write("Nome: ");
            string nome = Console.ReadLine();
            
            // TODO: Complete com Preco, Estoque, CategoriaId
            
            var produto = new Produto
            {
                // TODO: Preencha as propriedades
            };
            
            // repo.InserirProduto(produto);
        }

        static void AtualizarProdutoExistente(ProdutoRepository repo)
        {
            // TODO: Implemente a atualização
            Console.WriteLine("\n=== ATUALIZAR PRODUTO ===");
            
            Console.Write("ID do produto: ");
            int id = int.Parse(Console.ReadLine());
            
            // TODO: Busque o produto e permita alterar os dados
        }

        static void DeletarProdutoExistente(ProdutoRepository repo)
        {
            // TODO: Implemente a exclusão
            Console.WriteLine("\n=== DELETAR PRODUTO ===");
            
            Console.Write("ID do produto: ");
            int id = int.Parse(Console.ReadLine());
            
            // TODO: Confirme antes de deletar
        }

        static void ListarPorCategoria(ProdutoRepository repo)
        {
            // TODO: Implemente
            Console.WriteLine("\n=== PRODUTOS POR CATEGORIA ===");
        }

        static void CriarNovoPedido(PedidoRepository repo)
        {
            // TODO: Implemente criação de pedido com itens
            Console.WriteLine("\n=== CRIAR NOVO PEDIDO ===");
        }

        static void ListarPedidosDeCliente(PedidoRepository repo)
        {
            Console.WriteLine("\n=== PEDIDOS DO CLIENTE ===");
            Console.Write("Informe o ID do cliente: ");
            if (int.TryParse(Console.ReadLine(), out int clienteId))
            {
                repo.ListarPedidosCliente(clienteId);
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }

        static void DetalhesDoPedido(PedidoRepository repo)
        {
            Console.WriteLine("\n=== DETALHES DO PEDIDO ===");
            Console.Write("Informe o ID do pedido: ");
            if (int.TryParse(Console.ReadLine(), out int pedidoId))
            {
                repo.ObterDetalhesPedido(pedidoId);
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }
    }
}