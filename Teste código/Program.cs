using System;

class SistemaNotas
{
    static void Main()
    {
        string[] disciplinas = null;
        string[] alunos = null;
        double[,] notas = null;
        int numAlunos = 0;

        int opcao;

        do
        {
            Console.WriteLine("\nMENU:");
            Console.WriteLine("1 - Cadastrar disciplinas");
            Console.WriteLine("2 - Cadastrar alunos e notas");
            Console.WriteLine("3 - Listar alunos em ordem alfabética");
            Console.WriteLine("4 - Listar alunos por média (maior para menor)");
            Console.WriteLine("5 - Buscar aluno por nome");
            Console.WriteLine("6 - Mostrar notas por aluno");
            Console.WriteLine("7 - Mostrar notas por disciplina");
            Console.WriteLine("8 - Sair");
            Console.Write("Escolha uma opção: ");

            while (!int.TryParse(Console.ReadLine(), out opcao) || opcao < 1 || opcao > 8)
            {
                Console.Write("Opção inválida. Digite um número entre 1 e 8: ");
            }

            Console.Clear();

            switch (opcao)
            {
                case 1:
                    Console.Write("Digite a quantidade de disciplinas: ");
                    int numDisciplinas = int.Parse(Console.ReadLine());
                    disciplinas = new string[numDisciplinas];

                    for (int i = 0; i < numDisciplinas; i++)
                    {
                        Console.Write($"Nome da disciplina {i + 1}: ");
                        disciplinas[i] = Console.ReadLine();
                    }
                    Console.WriteLine("Disciplinas cadastradas com sucesso!");
                    break;

                case 2:
                    if (disciplinas == null)
                    {
                        Console.WriteLine("Você precisa cadastrar as disciplinas antes.");
                        break;
                    }

                    Console.Write("Digite a quantidade de alunos: ");
                    numAlunos = int.Parse(Console.ReadLine());
                    alunos = new string[numAlunos];
                    notas = new double[numAlunos, disciplinas.Length];

                    for (int i = 0; i < numAlunos; i++)
                    {
                        Console.Write($"\nNome do aluno {i + 1}: ");
                        alunos[i] = Console.ReadLine();

                        for (int j = 0; j < disciplinas.Length; j++)
                        {
                            double nota;
                            Console.Write($"Nota em {disciplinas[j]} (0 a 10): ");

                            while (!double.TryParse(Console.ReadLine(), out nota) || nota < 0 || nota > 10)
                            {
                                Console.Write("Nota inválida! Digite novamente (0 a 10): ");
                            }

                            notas[i, j] = nota;
                        }
                    }
                    Console.WriteLine("Cadastro de alunos e notas realizado com sucesso!");
                    break;

                case 3:
                    if (alunos == null)
                    {
                        Console.WriteLine("Cadastre alunos primeiro.");
                        break;
                    }
                    OrdenarPorNome(alunos, notas);
                    MostrarAlunos(alunos, notas);
                    break;

                case 4:
                    if (alunos == null)
                    {
                        Console.WriteLine("Cadastre alunos primeiro.");
                        break;
                    }
                    OrdenarPorMedia(alunos, notas);
                    MostrarAlunos(alunos, notas);
                    break;

                case 5:
                    if (alunos == null)
                    {
                        Console.WriteLine("Cadastre alunos primeiro.");
                        break;
                    }
                    OrdenarPorNome(alunos, notas);
                    Console.Write("Digite o nome do aluno para buscar: ");
                    string nomeBusca = Console.ReadLine();
                    int pos = BuscaBinaria(alunos, nomeBusca);
                    if (pos >= 0)
                    {
                        Console.WriteLine($"Aluno encontrado: {alunos[pos]}");
                        for (int j = 0; j < disciplinas.Length; j++)
                            Console.WriteLine($"{disciplinas[j]}: {notas[pos, j]:F2}");
                    }
                    else
                    {
                        Console.WriteLine("Aluno não encontrado.");
                    }
                    break;

                case 6:
                    if (alunos == null)
                    {
                        Console.WriteLine("Cadastre alunos primeiro.");
                        break;
                    }
                    for (int i = 0; i < alunos.Length; i++)
                    {
                        Console.WriteLine($"\nAluno: {alunos[i]}");
                        for (int j = 0; j < disciplinas.Length; j++)
                            Console.WriteLine($"{disciplinas[j]}: {notas[i, j]:F2}");
                    }
                    break;

                case 7:
                    if (alunos == null)
                    {
                        Console.WriteLine("Cadastre alunos primeiro.");
                        break;
                    }
                    for (int j = 0; j < disciplinas.Length; j++)
                    {
                        Console.WriteLine($"\nDisciplina: {disciplinas[j]}");
                        for (int i = 0; i < alunos.Length; i++)
                            Console.WriteLine($"{alunos[i]}: {notas[i, j]:F2}");
                    }
                    break;

                case 8:
                    Console.WriteLine("Encerrando o programa...");
                    break;
            }

        } while (opcao != 8);
    }

    static double CalcularMedia(double[,] notas, int linha)
    {
        double soma = 0;
        for (int j = 0; j < notas.GetLength(1); j++)
            soma += notas[linha, j];
        return soma / notas.GetLength(1);
    }

    static void MostrarAlunos(string[] alunos, double[,] notas)
    {
        for (int i = 0; i < alunos.Length; i++)
        {
            double media = CalcularMedia(notas, i);
            Console.WriteLine($"{alunos[i]} - Média: {media:F2}");
        }
    }

    static void OrdenarPorNome(string[] alunos, double[,] notas)
    {
        for (int i = 0; i < alunos.Length - 1; i++)
        {
            for (int j = i + 1; j < alunos.Length; j++)
            {
                if (string.Compare(alunos[i], alunos[j]) > 0)
                {
                    Trocar(ref alunos[i], ref alunos[j]);
                    TrocarNotas(notas, i, j);
                }
            }
        }
    }

    static void OrdenarPorMedia(string[] alunos, double[,] notas)
    {
        for (int i = 0; i < alunos.Length - 1; i++)
        {
            for (int j = i + 1; j < alunos.Length; j++)
            {
                double mediaI = CalcularMedia(notas, i);
                double mediaJ = CalcularMedia(notas, j);
                if (mediaJ > mediaI)
                {
                    Trocar(ref alunos[i], ref alunos[j]);
                    TrocarNotas(notas, i, j);
                }
            }
        }
    }

    static int BuscaBinaria(string[] alunos, string nome)
    {
        int esquerda = 0, direita = alunos.Length - 1;
        while (esquerda <= direita)
        {
            int meio = (esquerda + direita) / 2;
            int comparacao = string.Compare(alunos[meio], nome, StringComparison.OrdinalIgnoreCase);
            if (comparacao == 0)
                return meio;
            else if (comparacao < 0)
                esquerda = meio + 1;
            else
                direita = meio - 1;
        }
        return -1;
    }

    static void Trocar(ref string a, ref string b)
    {
        string temp = a;
        a = b;
        b = temp;
    }

    static void TrocarNotas(double[,] notas, int linha1, int linha2)
    {
        int colunas = notas.GetLength(1);
        for (int j = 0; j < colunas; j++)
        {
            double temp = notas[linha1, j];
            notas[linha1, j] = notas[linha2, j];
            notas[linha2, j] = temp;
        }
    }
}
