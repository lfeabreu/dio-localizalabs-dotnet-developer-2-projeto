using System;

namespace DIO.Series
{
    class Program
    {

        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                opcaoUsuario = ObterOpcaoUsuario();
            }
        }

        private static void ListarSeries()
        {
            Console.WriteLine("===== Listar séries =====");

            var lista = repositorio.Lista();

            if(lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada.");
                Console.WriteLine("===== Fim da lista =====");
                return;
            }

            foreach (var serie in lista)
            {
                Console.WriteLine("#ID {0}: - {1}", serie.retornaId(), serie.retornaTitulo());
            }
            Console.WriteLine("===== Fim da lista =====");
        }

        private static void opcoesInsereAtualizaSerie(out int genero, out string titulo, out int ano, out string descricao)
        {
            Console.WriteLine("Gêneros:");
            foreach( int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
            }
            Console.WriteLine("Digite o gênero entre as opções acima: ");
            genero = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o título da série: ");
            titulo = Console.ReadLine();

            Console.WriteLine("Digite o ano de início da série: ");
            ano = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o descrição da série: ");
            descricao = Console.ReadLine();
        }

        private static void InserirSerie()
        {
            Console.WriteLine("===== Inserir nova série =====");

            int entradaGenero;
            string entradaTitulo;
            int entradaAno;
            string entradaDescricao;
            opcoesInsereAtualizaSerie(out entradaGenero, out entradaTitulo, out entradaAno, out entradaDescricao);

            Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);
            
            repositorio.Insere(novaSerie);
            Console.WriteLine("===== Série inserida =====");
        }

        private static void AtualizarSerie()
        {
            Console.WriteLine("===== Atualizar série =====");
            Console.WriteLine("Digite o id da série: ");
            int idSerie = int.Parse(Console.ReadLine());

            int entradaGenero;
            string entradaTitulo;
            int entradaAno;
            string entradaDescricao;
            opcoesInsereAtualizaSerie(out entradaGenero, out entradaTitulo, out entradaAno, out entradaDescricao);

            Serie atualizaSerie = new Serie(id: idSerie,
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);
            
            repositorio.Atualiza(idSerie, atualizaSerie);
            Console.WriteLine("===== Série atualizada =====");
        }

        private static void ExcluirSerie()
        {
            Console.WriteLine("===== Excluir série =====");
            Console.WriteLine("Digite o id da série: ");
            int idSerie = int.Parse(Console.ReadLine());

            repositorio.Exclui(idSerie);
            Console.WriteLine("===== Série excluída =====");
        }

        private static void VisualizarSerie()
        {
            Console.WriteLine("===== Visualizar série =====");
            Console.WriteLine("Digite o id da série: ");
            int idSerie = int.Parse(Console.ReadLine());

            var serie = repositorio.RetornaPorId(idSerie);

            Console.WriteLine(serie);
            Console.WriteLine("===== Fim da visualização =====");
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("===== /|\\ DIO Séries /|\\ =====");
            Console.WriteLine("Informe a opção desejada:");

            Console.WriteLine("1- Listar séries");
            Console.WriteLine("2- Inserir nova série");
            Console.WriteLine("3- Atualizar série");
            Console.WriteLine("4- Excluir série");
            Console.WriteLine("5- Visualizar série");
            Console.WriteLine("----------------");
            Console.WriteLine("C- Limpar tela");
            Console.WriteLine("X- Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
    }
}
