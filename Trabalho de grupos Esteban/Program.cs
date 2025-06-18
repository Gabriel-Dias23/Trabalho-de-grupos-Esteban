

/*
3.Sistema de Cadastro de Notas de Alunos

Descrição: Registra as notas dos alunos em diferentes disciplinas.

Conceitos Aplicados:

Vetor: Armazena o nome dos alunos.
Matriz: Armazena as notas dos alunos em várias disciplinas.
Ordenação: Lista os alunos em ordem alfabética ou por média de notas.
Busca Binária: Localiza rapidamente um aluno pelo nome.
*/


using System;

class SistemaNotas
{
    /*
    classe main para iniciar o projeto - Carlos 
    */
    static void Main()
    {
        int numAlunos = 3;
        string[] disciplinas = { "Português", "Matemática", "História" };
        int numDisciplinas = disciplinas.Length;

        string[] alunos = new string[numAlunos];
        double[,] notas = new double[numAlunos, numDisciplinas];

        // Cadastro de alunos e notas - Carlos 
        for (int i = 0; i < numAlunos; i++)
        {
            Console.Write($"Nome do aluno {i + 1}: ");
            alunos[i] = Console.ReadLine();

            for (int j = 0; j < numDisciplinas; j++)
            {
                Console.Write($"Nota em {disciplinas[j]}: ");
                notas[i, j] = double.Parse(Console.ReadLine());
            }

            Console.WriteLine();
        }

        // menu interativo - Caio
        int opcao;
        do
        {
            Console.WriteLine("\nMENU:");
            Console.WriteLine("1 - Listar alunos em ordem alfabética");
            Console.WriteLine("2 - Listar alunos por média (maior para menor)");
            Console.WriteLine("3 - Buscar aluno por nome");
            Console.WriteLine("4 - Mostrar notas por aluno");
            Console.WriteLine("5 - Mostrar notas por disciplina");
            Console.WriteLine("6 - Sair");
            Console.Write("Escolha uma opção: ");
            opcao = int.Parse(Console.ReadLine());
            Console.Clear();

            switch (opcao)
            {
                case 1:
                    OrdenarPorNome(alunos, notas);
                    MostrarAlunos(alunos, notas);
                    break;

                case 2:
                    OrdenarPorMedia(alunos, notas);
                    MostrarAlunos(alunos, notas);
                    break;

                case 3:
                    OrdenarPorNome(alunos, notas); // Necessário para busca binária - Caio 
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
                    /*
                    Gabriel 
                    */
                case 4:
                    for (int i = 0; i < alunos.Length; i++)
                    {
                        Console.WriteLine($"\nAluno: {alunos[i]}");
                        for (int j = 0; j < disciplinas.Length; j++)
                            Console.WriteLine($"{disciplinas[j]}: {notas[i, j]:F2}");
                    }
                    break;

                case 5:
                    for (int j = 0; j < disciplinas.Length; j++)
                    {
                        Console.WriteLine($"\nDisciplina: {disciplinas[j]}");
                        for (int i = 0; i < alunos.Length; i++)
                            Console.WriteLine($"{alunos[i]}: {notas[i, j]:F2}");
                    }
                    break;

                case 6:
                    Console.WriteLine("Encerrando o programa...");
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

        } while (opcao != 6);
    }

    /*
    Implementações das opções do menu e suas funcionalidades - Bernado 
    */
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
    /*
    Parte de ordenação por nomes com busca binária - João 
    */
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

    /*
    M
    */
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
