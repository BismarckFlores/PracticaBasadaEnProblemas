using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaBasadaEnProblemas
{
    internal class Program
    {
        private const int TOTAL_VOTOS = 100;
        private const int TOTAL_CANDIDATOS = 5;

        static void Main(string[] args)
        {
            
        }

        private static void EjecutarSistemaDeVotacion()
        {
            int[] votos = GenerarVotos();

            Console.WriteLine("--- Reto 6: Simulador de Votaciones ---");
            Console.WriteLine($"Total de votos generados: {votos.Length}");

            ValidarVotos(votos, out int[] votosValidos);
            Console.WriteLine($"Votos válidos para el conteo: {votosValidos.Length}");
            Console.WriteLine($"Votos inválidos descartados: {TOTAL_VOTOS - votosValidos.Length}\n");

            Dictionary<int, int> conteoVotos = ContarVotos(votosValidos);

            Console.WriteLine("\n--- Conteo de Votos por Candidato ---");
            foreach (var candidato in conteoVotos)
            {
                Console.WriteLine($"Candidato {candidato.Key}: {candidato.Value} votos");
            }
            Console.WriteLine("----------------------------------\n");

            DeterminarGanador(conteoVotos);
            Console.WriteLine();

            CalcularPorcentajes(conteoVotos, votosValidos.Length);
            Console.ReadKey();
        }

        private static int[] GenerarVotos()
        {
            Random random = new Random();
            int[] votos = new int[TOTAL_VOTOS];
            for (int i = 0; i < TOTAL_VOTOS; i++)
            {
                // Genera votos entre 1 y 5 (candidatos válidos)
                // Más ocasionalmente 0 o 6 (inválidos) para la prueba de validación
                votos[i] = random.Next(0, TOTAL_CANDIDATOS + 2); // Rango de 0 a 6
                if (votos[i] == 0) votos[i] = 6; // Votos inválidos: 6 o 0
            }
            return votos;
        }

        private static void ValidarVotos(int[] votos, out int[] votosValidos)
        {
            votosValidos = votos.Where(v => v >= 1 && v <= TOTAL_CANDIDATOS).ToArray();
        }

        private static Dictionary<int, int> ContarVotos(int[] votosValidos)
        {
            Dictionary<int, int> conteo = new Dictionary<int, int>();
            for (int i = 1; i <= TOTAL_CANDIDATOS; i++)
            {
                conteo[i] = votosValidos.Count(v => v == i);
            }
            return conteo;
        }

        private static void DeterminarGanador(Dictionary<int, int> conteoVotos)
        {
            if (conteoVotos.Count == 0)
            {
                Console.WriteLine("** No hay votos válidos para determinar un ganador. **");
                return;
            }

            int maxVotos = conteoVotos.Values.Max();

            var ganadores = conteoVotos.Where(c => c.Value == maxVotos).Select(c => c.Key).ToList();

            if (ganadores.Count == 1)
            {
                Console.WriteLine($"*** ¡Candidato {ganadores.First()} es el GANADOR con {maxVotos} votos! ***");
            }
            else
            {
                string empatados = string.Join(" y ", ganadores);
                Console.WriteLine($"*** ¡EMPATE! Los candidatos {empatados} empataron con {maxVotos} votos cada uno. ***");
            }
        }

        private static void CalcularPorcentajes(Dictionary<int, int> conteoVotos, int totalVotosValidos)
        {
            Console.WriteLine("--- Porcentaje de Votos ---");
            if (totalVotosValidos == 0)
            {
                Console.WriteLine("No se puede calcular el porcentaje: 0 votos válidos.");
                return;
            }

            foreach (var par in conteoVotos)
            {
                double porcentaje = (double)par.Value / totalVotosValidos * 100;
                Console.WriteLine($"Candidato {par.Key}: {porcentaje:F2}% ({par.Value} votos)");
            }
            Console.WriteLine("---------------------------\n");
        }
    }
}
