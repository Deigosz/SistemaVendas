using System;
using System.Collections.Generic;
using System.Threading; // Adicionando o namespace para Thread.Sleep
using System.Globalization;
using System.Drawing;
using SistemaVendas.Services;
using Console = Colorful.Console;

namespace ConsoleMenuSistema
{
    public class Menu
    {
        private List<Produto> listaProdutos;

        public Menu(string nomguia)
        {
            listaProdutos = new List<Produto>();
            for (; ; )
            {
                Console.Clear();
                Console.Title = nomguia;
                MostrarLogo();
                MontarOpcoes("1", "Cadastrar Produto");
                MontarOpcoes("2", "Listar Produtos");
                MontarOpcoes("3", "Item Venda");
                MontarOpcoes("0", "Sair");

                Console.Write("\nSelecione uma Opção: ");
                string Opcao = Console.ReadLine();

                if (Opcao == "1")
                {
                    string NomeProduto;
                    double Preco;
                    int QtdEstoque;

                    Console.Write("Digite o nome do Produto: ");
                    NomeProduto = Console.ReadLine();

                    Console.Write("Digite o preço do Produto: ");
                    Preco = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                    Console.Write("Digite a quantidade em Estoque: ");
                    QtdEstoque = Convert.ToInt32(Console.ReadLine());

                    Produto produto = new Produto(NomeProduto, Preco, QtdEstoque);
                    listaProdutos.Add(produto);

                    Console.WriteLine("Produto cadastrado com sucesso!");
                    Console.WriteLine();
                }
                else if (Opcao == "2")
                {
                    Console.WriteLine("\nLista de Produtos: ");
                    Console.WriteLine();
                    foreach (var produto in listaProdutos)
                    {
                        Console.WriteLine($"Id: {produto.Id}, Nome: {produto.Nome},\nPreço: {produto.Preco:c}, Estoque: {produto.QtdEstoque}\n",Color.GreenYellow);
                    }

                    Console.WriteLine();
                    Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
                    Console.WriteLine();
                    Console.ReadKey();
                }
                else if (Opcao == "3")
                { 
                    if(listaProdutos.Count == 0)
                    {
                        Console.WriteLine("Não há produtos cadastrados.");
                        Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
                        Console.ReadKey();
                        continue;
                    }

                    
                    Console.WriteLine("Lista de Produtos Disponíveis para Venda: \n");
                    foreach (var produto in listaProdutos)
                    {
                        Console.WriteLine($"Id: {produto.Id}, Nome: {produto.Nome},Preço: {produto.Preco:c}, Estoque: {produto.QtdEstoque}\n", Color.GreenYellow);
                    }
                    Console.WriteLine();

                    Console.Write("\nSelecione o número do produto que deseja adicionar à venda (ou digite 0 para cancelar): ");
                    int IdProdutoItemVenda = Convert.ToInt32(Console.ReadLine());

                    if (IdProdutoItemVenda == 0)
                    {
                        Console.WriteLine("Operação cancelada.");
                        Thread.Sleep(1500);
                        continue;
                    }

                    Console.Write("\nInfome a quantidade à ser vendida do produto: ");
                    int QuantidadeASerVendida = Convert.ToInt32(Console.ReadLine());

                    Produto produtoSelecionado = listaProdutos[IdProdutoItemVenda - 1];
                    ItemVenda novoItemVenda = new ItemVenda(produtoSelecionado, QuantidadeASerVendida);

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