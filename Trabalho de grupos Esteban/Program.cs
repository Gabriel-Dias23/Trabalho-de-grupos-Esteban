
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
using System.Collections.Generic;
using System.Data;
using System.Globalization;

class SistemaNotas
{
    static void Main()
    {
        int numAlunos = 2;
        int numDisciplinas = 2;

        string[] nomes = new string[numAlunos];
        double[,] notas = new double[numAlunos, numDisciplinas];

        for (int i = 0; i < numAlunos; i++)
        {
            Console.WriteLine($"Digite o nome do Aluno {i + 1}: ");
            nomes[i] = Console.ReadLine();

            for (int j = 0; j < numDisciplinas; j++)
            {
                Console.WriteLine($"Nota da disciplina {j + 1}: ");
                double nota;

                while (!double.TryParse(Console.ReadLine(), out nota) || nota < 0 || nota > 10)
                {
                    Console.WriteLine("Nota inválida. Digite novamente (0 a 10): ");
                }

                notas[i, j] = nota;


            }

            Console.WriteLine();



        }
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

        int opcao;
        do
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine(" 1- Lista de alunos em ordem alfabéica");
            Console.WriteLine(" 2- Lista de alunos por média(maior e menor)");
            Console.WriteLine(" 3- Buscar aluno por nome");
            Console.WriteLine(" 4 - sair");
            Console.WriteLine(" Escolha: ");
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
                    Console.WriteLine("Saindo...");
                    break;
                default:
                    Console.WriteLine("opção inválda. ");
                    break;
            }
            
        } while (opcao != 4);

        static void ListarPorNome(string[] nomes, double[] medias)
        {
            Array.Sort(nomes, medias);
            Console.WriteLine("\nLista de Alunos em ordem alfabética:");
            for (int i = 0; i < nomes.Length; i++)
            {
                Console.WriteLine($"{nomes[i]} - Média: {medias[i]:F2}");
            }
        }

        static void ListarPorMedia(string[] nomes, double[] medias)
        {
            Array.Sort(medias, nomes);
            Console.WriteLine("\nLista de Alunos por média:");
            for (int i = 0; i < nomes.Length; i++)
            {
                Console.WriteLine($"{nomes[i]} - Média: {medias[i]:F2}");
            }
        }
        static void BuscarAluno(string[] nomes, double[] medias)
        {
            string[] nomesOrdenados = (string[])nomes.Clone();
            double[] mediasOrdenadas = (double[])medias.Clone();

            Array.Sort(nomesOrdenados, mediasOrdenadas);

            Console.WriteLine("Digite o nome do aluno para buscar: ");
            string nomeBusca = Console.ReadLine();

            int pos = Array.BinarySearch(nomesOrdenados, nomeBusca);

            if(pos >= 0)
            {
                Console.WriteLine($"Aluno encontrado: {nomesOrdenados[pos]} - Média: {mediasOrdenadas[pos]:F2}");
            }
            else
            {
                Console.WriteLine("Aluno não encontrado.");
            }
        }

    }

}
