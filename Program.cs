using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Projeto03_GestorDeEstoque_POO
{
    class Program
    {
        static List<IEstoque> produtos = new List<IEstoque>();
        enum Menu{Listar = 1, Adicionar, Remover, Entrada, Saida, Sair}
        static void Main(string[] args)
        {
            Carregar();
            bool escolheuSair = false;
            while(escolheuSair == false)
            {
                Console.WriteLine("Sistema de Estoque");
                Console.WriteLine("1-Listar\n2-Adicionar\n3-Remover\n4-Registrar Entrada\n5-Registrar Saída\n6-Sair");

                string opcaoStr = Console.ReadLine();
                int opcaoInt = int.Parse(opcaoStr);

                if (opcaoInt > 0 && opcaoInt < 7)
                {                    
                    Menu escolha = (Menu)opcaoInt;

                        switch(escolha)
                    {
                        case Menu.Listar:
                            Listagem();
                            break;

                        case Menu.Adicionar:
                            Cadastro();
                            break;

                        case Menu.Remover:
                            Remover();
                            break;

                        case Menu.Entrada:
                            Entrada();
                            break;

                        case Menu.Saida:
                            Saida();
                            break;

                        case Menu.Sair:
                            escolheuSair = true;
                            break;
                    }

                }
                else
                {
                    escolheuSair = true;
                }
                Console.Clear();
            }            
        }
        static void Listagem()
        {
            Console.WriteLine("Lista de Produtos");
            int i = 0;            
            foreach(IEstoque produto in produtos)
            {
                Console.WriteLine("ID:" + i);
                produto.Exibir();
                i++;
            }
            Console.ReadLine();
        }
        static void Remover()
        {
            Listagem();
            Console.WriteLine("Digite o ID do elemento que você quer remover: ");
            int id = int.Parse(Console.ReadLine());

            if (id >= 0 && id < produtos.Count)
            {
                produtos.RemoveAt(id);
                Salvar();
            }
        }
        static void Entrada()
        {
            Listagem();
            Console.WriteLine("Digite o ID do elemento que você quer dar entrada: ");
            int id = int.Parse(Console.ReadLine());

            if (id >= 0 && id < produtos.Count)
            {
                produtos[id].AdicionarEntrada();
                Salvar();
            }
        }
        static void Saida()
        {
            Listagem();
            Console.WriteLine("Digite o ID do elemento que você quer dar baíxa: ");
            int id = int.Parse(Console.ReadLine());

            if (id >= 0 && id < produtos.Count)
            {
                produtos[id].AdicionarSaida();
                Salvar();
            }
        }
        static void Cadastro()
        {
            Console.WriteLine("Cadastro de Produto");
            Console.WriteLine("1-Produto Fisíco\n2-Ebook\n3-Curso");
            string opcStr = Console.ReadLine();
            // Convertendo str para int
            int escolhaInt = int.Parse(opcStr);
            switch(escolhaInt)
            {
                case 1:
                    CadastrarPFisico();
                    break;

                case 2:
                    CadastrarEbook();
                    break;

                case 3:
                    CadastrarCurso();
                    break;
            }
        }
        static void CadastrarPFisico()
        {
            Console.WriteLine("Cadastrando Produto Fisíco:");
            Console.WriteLine("Nome: ");
            string nome = Console.ReadLine();
            
            Console.WriteLine("Preço: ");
            float preco = float.Parse(Console.ReadLine());

            Console.WriteLine("Frete: ");
            float frete = float.Parse(Console.ReadLine());

            ProdutoFisico pf = new ProdutoFisico(nome, preco, frete);
            produtos.Add(pf);
            Salvar();
        }
        static void CadastrarEbook()
        {
            Console.WriteLine("Cadastrando Ebook:");
            Console.WriteLine("Nome: ");
            string nome = Console.ReadLine();
            
            Console.WriteLine("Preço: ");
            float preco = float.Parse(Console.ReadLine());

            Console.WriteLine("Autor: ");
            string autor = (Console.ReadLine());

            Ebook eb = new Ebook(nome, preco, autor);
            produtos.Add(eb);
            Salvar();
        }
        static void CadastrarCurso()
        {
            Console.WriteLine("Cadastrando Curso:");
            Console.WriteLine("Nome: ");
            string nome = Console.ReadLine();
            
            Console.WriteLine("Preço: ");
            float preco = float.Parse(Console.ReadLine());

            Console.WriteLine("Autor: ");
            string autor = (Console.ReadLine());

            Curso cs = new Curso(nome, preco, autor);
            produtos.Add(cs);
            Salvar();
        }
        static void Salvar()
        {
            // Essa função fica alocada no final de uma determinada ação
            FileStream stream = new FileStream("produtos.dat", FileMode.OpenOrCreate);
            BinaryFormatter enconder = new BinaryFormatter();

            enconder.Serialize(stream, produtos);

            stream.Close();
        }
        static void Carregar()
        {
            // Essa função fica alocada no começo de uma determinada ação
            FileStream stream = new FileStream("produtos.dat", FileMode.OpenOrCreate);
            BinaryFormatter enconder = new BinaryFormatter();
            
            /*
            Pode acontecer da lista está vazia e o programa dar erro, por isso o correto é usar
            o try(caso der um erro)
            */
            try
            {
                produtos = (List<IEstoque>)enconder.Deserialize(stream);

                if(produtos == null)
                {
                    produtos = new List<IEstoque>();
                }

            }
            catch(Exception e)
            {
                produtos = new List<IEstoque>();
            }
            stream.Close();
        }
    }
}
