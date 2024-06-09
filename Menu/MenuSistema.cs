using System;
using System.Collections.Generic;
using System.Threading;
using System.Globalization;
using System.Drawing;
using SistemaVendas.Services;
using Console = Colorful.Console;

namespace ConsoleMenuSistema
{
    public class Menu
    {
        private List<Produto> listaProdutos;
        private List<Venda> vendasRegistradas;
        private Venda venda;

        public Menu(string nomguia)
        {
            listaProdutos = new List<Produto>();
            vendasRegistradas = new List<Venda>();
            venda = new Venda(DateTime.Now);
            for (; ; )
            {
                Console.Clear();
                Console.Title = nomguia;
                MostrarLogo();
                MontarOpcoes("1", "Cadastrar Produto");
                MontarOpcoes("2", "Listar Produtos");
                MontarOpcoes("3", "Pedidos");
                MontarOpcoes("4", "Vendas");
                MontarOpcoes("0", "Sair");

                Console.Write("\nSelecione uma Opção: ");
                string Opcao = Console.ReadLine();

                if (Opcao == "1")
                {
                    Console.Write("Digite o nome do Produto: ");
                    string nomeProduto = Console.ReadLine();

                    Console.Write("Digite o preço do Produto: ");
                    double precoProduto;
                    while (!double.TryParse(Console.ReadLine(), NumberStyles.Currency, CultureInfo.InvariantCulture, out precoProduto))
                    {
                        Console.WriteLine("Preço inválido. Por favor, digite um número válido.");
                        Console.Write("Digite o preço do Produto: ");
                    }

                    Console.Write("Digite a quantidade em Estoque: ");
                    int qtdEstoque;
                    while (!int.TryParse(Console.ReadLine(), out qtdEstoque) || qtdEstoque < 0)
                    {
                        Console.WriteLine("Quantidade inválida. Por favor, digite um número inteiro não negativo.");
                        Console.Write("Digite a quantidade em Estoque: ");
                    }

                    Produto produto = new Produto(nomeProduto, precoProduto, qtdEstoque);
                    listaProdutos.Add(produto);

                    Console.WriteLine("Produto cadastrado com sucesso!");
                    Console.WriteLine();
                }

                else if (Opcao == "2")
                {
                    Console.WriteLine("\nLista de Produtos: ");
                    Console.WriteLine();
                    if (listaProdutos.Count == 0)
                    {
                        Console.WriteLine("Não há produtos cadastrados.");
                    }
                    else
                    {
                        foreach (var produto in listaProdutos)
                        {
                            Console.WriteLine($"Id: {produto.Id}, Nome: {produto.Nome}, Preço: {produto.Preco:c}, Estoque: {produto.QtdEstoque}\n", Color.GreenYellow);
                        }
                    }
                    Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
                    Console.WriteLine();
                    Console.ReadKey();
                }
                else if (Opcao == "3")
                {
                    if (listaProdutos.Count == 0)
                    {
                        Console.WriteLine("Não há produtos cadastrados.");
                        Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
                        Console.ReadKey();
                        continue;
                    }

                    Console.WriteLine("Lista de Produtos Disponíveis para Venda: \n");
                    foreach (var produto in listaProdutos)
                    {
                        Console.WriteLine($"Id: {produto.Id}, Nome: {produto.Nome}, Preço: {produto.Preco:c}, Estoque: {produto.QtdEstoque}\n", Color.GreenYellow);
                    }
                    Console.WriteLine();

                    Console.Write("\nSelecione o número do produto que deseja adicionar à venda (ou digite 0 para cancelar): ");
                    int idProdutoItemVenda;
                    while (!int.TryParse(Console.ReadLine(), out idProdutoItemVenda) || idProdutoItemVenda < 0 || idProdutoItemVenda > listaProdutos.Count)
                    {
                        Console.WriteLine("ID inválido. Por favor, selecione um ID válido ou 0 para cancelar.");
                        Console.Write("\nSelecione o número do produto que deseja adicionar à venda (ou digite 0 para cancelar): ");
                    }

                    if (idProdutoItemVenda == 0)
                    {
                        Console.WriteLine("Operação cancelada.");
                        Thread.Sleep(1500);
                        continue;
                    }

                    Console.Write("\nInforme a quantidade a ser vendida do produto: ");
                    int quantidadeASerVendida;
                    while (!int.TryParse(Console.ReadLine(), out quantidadeASerVendida) || quantidadeASerVendida <= 0)
                    {
                        Console.WriteLine("Quantidade inválida. Por favor, digite um número inteiro positivo.");
                        Console.Write("\nInforme a quantidade a ser vendida do produto: ");
                    }

                    Produto produtoSelecionado = listaProdutos[idProdutoItemVenda - 1];
                    ItemVenda novoItemVenda = new ItemVenda(produtoSelecionado, quantidadeASerVendida);

                    venda.AdicionarItem(novoItemVenda, quantidadeASerVendida);

                    Console.WriteLine("Deseja finalizar a compra e realizar o pagamento? (S/N)");
                    string opcaoFinalizar = Console.ReadLine().ToUpper();

                    if (opcaoFinalizar == "S")
                    {
                        if (venda.Itens.Count == 0)
                        {
                            Console.WriteLine("Não há itens na venda. Operação Cancelada!");
                            Thread.Sleep(1500);
                            continue;
                        }

                        double totalVenda = venda.Total;
                        Console.WriteLine($"Total da Venda: {totalVenda:c}");

                        Console.WriteLine("Selecione o método de pagamento:");
                        Console.WriteLine("1. Espécie");
                        Console.WriteLine("2. Cheque");
                        Console.WriteLine("3. Cartão");
                        Console.Write("Opção: ");
                        string opcaoPagamento = Console.ReadLine();

                        Pagamento pagamento = null;

                        switch (opcaoPagamento)
                        {
                            case "1":
                                Console.Write("Digite a quantia em espécie: ");
                                double quantia;
                                while (!double.TryParse(Console.ReadLine(), NumberStyles.Currency, CultureInfo.InvariantCulture, out quantia) || quantia < totalVenda)
                                {
                                    Console.WriteLine("Quantia inválida. Por favor, digite um valor válido.");
                                    Console.Write("Digite a quantia em espécie: ");
                                }
                                pagamento = new PagamentoEspecie { Quantia = quantia, Total = totalVenda };
                                break;
                            case "2":
                                Console.Write("Digite o número do cheque: ");
                                long numeroCheque;
                                while (!long.TryParse(Console.ReadLine(), out numeroCheque))
                                {
                                    Console.WriteLine("Número de cheque inválido. Por favor, digite um número válido.");
                                    Console.Write("Digite o número do cheque: ");
                                }

                                Console.Write("Digite a data de depósito do cheque (formato dd/mm/yyyy): ");
                                DateTime dataDeposito;
                                while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataDeposito))
                                {
                                    Console.WriteLine("Data inválida. Por favor, digite a data no formato correto (dd/mm/yyyy).");
                                    Console.Write("Digite a data de depósito do cheque (formato dd/mm/yyyy): ");
                                }
                                pagamento = new PagamentoCheque
                                {
                                    Numero = numeroCheque,
                                    DataDeposito = dataDeposito,
                                    Total = totalVenda
                                };
                                break;
                            case "3":
                                Console.Write("Digite os dados da transação do cartão: ");
                                string dadosTransacao = Console.ReadLine();

                                Console.Write("Digite o resultado da transação (1 para sucesso, 0 para falha): ");
                                int resultadoTransacao;
                                while (!int.TryParse(Console.ReadLine(), out resultadoTransacao) || (resultadoTransacao != 0 && resultadoTransacao != 1))
                                {
                                    Console.WriteLine("Resultado da transação inválido. Por favor, digite 0 para falha ou 1 para sucesso.");
                                    Console.Write("Digite o resultado da transação (1 para sucesso, 0 para falha): ");
                                }

                                pagamento = new PagamentoCartao
                                {
                                    DadosTransacao = dadosTransacao,
                                    ResultadoTransacao = resultadoTransacao,
                                    Total = totalVenda
                                };
                                break;
                            default:
                                Console.WriteLine("Opção de pagamento inválida. Operação cancelada.");
                                Thread.Sleep(1500);
                                continue;
                        }

                        if (pagamento != null)
                        {
                            venda.RegistrarPagamento(pagamento);
                            Console.WriteLine("Pagamento realizado com sucesso!");
                            venda.Paga = true;
                        }
                    }

                    Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
                    Console.WriteLine();
                    Console.ReadKey();
                }
                else if (Opcao == "4")
                {
                    List<Venda> vendas = Venda.ObterTodasAsVendas();
                    if (vendas.Count == 0)
                    {
                        Console.WriteLine("Não há vendas registradas.");
                    }
                    else
                    {
                        Console.WriteLine("\nLista de Vendas: ");
                        foreach (var venda in vendas)
                        {
                            Console.WriteLine($"Id: {venda.Id}, Data: {venda.Data}, Total: {venda.Total:c}");
                        }
                    }

                    Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
                    Console.WriteLine();
                    Console.ReadKey();
                }

                else if (Opcao == "0")
                {
                    Console.WriteLine("Saindo...");
                    Thread.Sleep(1500);
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("A opção digitada é inválida!!");
                    Thread.Sleep(1500);
                }
            }
        }

        public static void MostrarLogo()
        {
            Console.WriteLine("  ____            ____        _           ", Color.BlueViolet);
            Console.WriteLine(" / ___| _   _ ___/ ___|  __ _| | ___  ___ ", Color.BlueViolet);
            Console.WriteLine(" \\___ \\| | | / __\\___ \\ / _` | |/ _ \\/ __|", Color.BlueViolet);
            Console.WriteLine("  ___) | |_| \\__ \\___) | (_| | |  __/\\__ \\", Color.BlueViolet);
            Console.WriteLine(" |____/ \\__, |___/____/ \\__,_|_|\\___||___/", Color.BlueViolet);
            Console.WriteLine("        |___/                             ", Color.BlueViolet);
            Console.WriteLine("\n");
        }

        public static void MontarOpcoes(string prefix, string message)
        {
            Console.Write("[");
            Console.Write(prefix, Color.BlueViolet);
            Console.WriteLine($"] {message}");
        }
    }
}