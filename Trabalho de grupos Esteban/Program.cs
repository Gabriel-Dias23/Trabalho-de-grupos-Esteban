using System;

class SistemaNotas
{
    static void Main()
    {
        int numAlunos = 2;
        string[] disciplinas = { "Português", "Matemática", "História" };
        int numDisciplinas = disciplinas.Length;

        string[] nomes = new string[numAlunos];
        double[,] notas = new double[numAlunos, numDisciplinas];

        // Cadastro de alunos e notas
        for (int i = 0; i < numAlunos; i++)
        {
            Console.WriteLine($"Digite o nome do Aluno {i + 1}: ");
            nomes[i] = Console.ReadLine();

            for (int j = 0; j < numDisciplinas; j++)
            {
                Console.Write($"Nota de {disciplinas[j]}: ");
                double nota;

                while (!double.TryParse(Console.ReadLine(), out nota) || nota < 0 || nota > 10)
                {
                    Console.WriteLine("Nota inválida. Digite novamente (0 a 10): ");
                }

                notas[i, j] = nota;
            }

            Console.WriteLine();
        }

        // Cálculo das médias
        double[] medias = new double[numAlunos];
        for (int i = 0; i < numAlunos; i++)
        {
            double soma = 0;
            for (int j = 0; j < numDisciplinas; j++)
            {
                soma += notas[i, j];
            }
            medias[i] = soma / numDisciplinas;
        }

        // Menu
        int opcao;
        do
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine(" 1 - Lista de alunos em ordem alfabética");
            Console.WriteLine(" 2 - Lista de alunos por média");
            Console.WriteLine(" 3 - Buscar aluno por nome");
            Console.WriteLine(" 4 - Mostrar notas por matéria (formato que você quer)");
            Console.WriteLine(" 5 - Sair");
            Console.Write("Escolha: ");
            opcao = int.Parse(Console.ReadLine());
            Console.Clear();

            switch (opcao)
            {
                case 1:
                    ListarPorNome(nomes, medias);
                    break;
                case 2:
                    ListarPorMedia(nomes, medias);
                    break;
                case 3:
                    BuscarAluno(nomes, medias);
                    break;
                case 4:
                    MostrarNotasPorMateria(nomes, notas, disciplinas);
                    break;
                case 5:
                    Console.WriteLine("Saindo...");
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        } while (opcao != 5);

        // MÉTODOS AUXILIARES

        static void ListarPorNome(string[] nomes, double[] medias)
        {
            string[] nomesOrdenados = (string[])nomes.Clone();
            double[] mediasOrdenadas = (double[])medias.Clone();
            Array.Sort(nomesOrdenados, mediasOrdenadas);
            Console.WriteLine("\nAlunos em ordem alfabética:");
            for (int i = 0; i < nomesOrdenados.Length; i++)
            {
                Console.WriteLine($"{nomesOrdenados[i]} - Média: {mediasOrdenadas[i]:F2}");
            }
        }

        static void ListarPorMedia(string[] nomes, double[] medias)
        {
            double[] mediasOrdenadas = (double[])medias.Clone();
            string[] nomesOrdenados = (string[])nomes.Clone();
            Array.Sort(mediasOrdenadas, nomesOrdenados);
            Console.WriteLine("\nAlunos ordenados por média:");
            for (int i = 0; i < nomesOrdenados.Length; i++)
            {
                Console.WriteLine($"{nomesOrdenados[i]} - Média: {mediasOrdenadas[i]:F2}");
            }
        }

        static void BuscarAluno(string[] nomes, double[] medias)
        {
            string[] nomesOrdenados = (string[])nomes.Clone();
            double[] mediasOrdenadas = (double[])medias.Clone();
            Array.Sort(nomesOrdenados, mediasOrdenadas, StringComparer.CurrentCultureIgnoreCase);

            Console.Write("Digite o nome do aluno para buscar: ");
            string nomeBusca = Console.ReadLine();

            int pos = Array.BinarySearch(nomesOrdenados, nomeBusca, StringComparer.CurrentCultureIgnoreCase);

            if (pos >= 0)
            {
                Console.WriteLine($"Aluno encontrado: {nomesOrdenados[pos]} - Média: {mediasOrdenadas[pos]:F2}");
            }
            else
            {
                Console.WriteLine("Aluno não encontrado.");
            }
        }
        static void MostrarNotasPorAluno(string[] nomes, double[,] notas, string[] disciplinas)
        {
            Console.WriteLine("\nNotas dos Alunos por Disciplina:\n");

            for (int i = 0; i < nomes.Length; i++)
            {
                Console.WriteLine($"Aluno: {nomes[i]}");
                for (int j = 0; j < disciplinas.Length; j++)
                {
                    Console.WriteLine($"  {disciplinas[j]}: {notas[i, j]:F2}");
                }
                Console.WriteLine();
            }

            static void MostrarNotasPorMateria(string[] nomes, double[,] notas, string[] disciplinas)
            {
                Console.WriteLine("\nNotas por Matéria:\n");

                for (int j = 0; j < disciplinas.Length; j++)
                {
                    Console.WriteLine($"Matéria: {disciplinas[j]}");
                    for (int i = 0; i < nomes.Length; i++)
                    {
                        Console.WriteLine($"{nomes[i]} - {notas[i, j]:F2}");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
