using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaBasadaEnProblemas
{
    internal class Program
    {
        class Clínica
        {
            //Actividad 5: Gestión de turnos en una clínica
            static void EdadesPacientes()
            {
                int[] edades = new int[20];
                int ninos = 0, jovenes = 0, adultos = 0, mayores = 0;
                int sumaTotal = 0, sumaNinos = 0, sumaJovenes = 0, sumaAdultos = 0, sumaMayores = 0;

                Console.WriteLine("Ingrese la edad del paciente (no se permiten valores negativos): ");

                for (int i = 0; i < edades.Length; i++)
                {
                    int edad;
                    bool edadValida = false;

                    // Bucle de validación
                    do
                    {
                        Console.Write($"Paciente {i + 1}: ");
                        edad = Convert.ToInt32(Console.ReadLine());

                        if (edad < 0)
                        {
                            Console.WriteLine("❌ Error: la edad no puede ser negativa. Intente nuevamente.");
                        }
                        else
                        {
                            edadValida = true;
                            edades[i] = edad;
                        }

                    } while (!edadValida);

                    // Suma total de edades
                    sumaTotal += edades[i];

                    // Clasificación por rango de edad
                    if (edades[i] <= 12)
                    {
                        ninos++;
                        sumaNinos += edades[i];
                    }
                    else if (edades[i] <= 25)
                    {
                        jovenes++;
                        sumaJovenes += edades[i];
                    }
                    else if (edades[i] <= 60)
                    {
                        adultos++;
                        sumaAdultos += edades[i];
                    }
                    else
                    {
                        mayores++;
                        sumaMayores += edades[i];
                    }
                }

                //Resultados
                Console.WriteLine($"\n----Resultados---");
                Console.WriteLine($"Niños: {ninos}");
                Console.WriteLine($"Jóvenes: {jovenes}");
                Console.WriteLine($"Adultos: {adultos}");
                Console.WriteLine($"Mayores: {mayores}");

                if (mayores > 5)
                {
                    Console.WriteLine("⚠️ Alerta: Más de 5 personas mayores registradas... ¡¡¡ALTO RIESGO!!!");
                }

                Console.WriteLine($"\nPromedio de edad total: {(double)sumaTotal / edades.Length:F2}");
                if (ninos > 0)
                    Console.WriteLine($"Promedio de edad niños: {(double)sumaNinos / ninos:F2}");
                if (jovenes > 0)
                    Console.WriteLine($"Promedio de edad jóvenes: {(double)sumaJovenes / jovenes:F2}");
                if (adultos > 0)
                    Console.WriteLine($"Promedio de edad adultos: {(double)sumaAdultos / adultos:F2}");
                if (mayores > 0)
                    Console.WriteLine($"Promedio de edad mayores: {(double)sumaMayores / mayores:F2}");
            }
            private const int TOTAL_VOTOS = 100;
            private const int TOTAL_CANDIDATOS = 5;

            static void Main(string[] args)
            {
                Console.WriteLine("Seleccione la actividad a ejecutar:");
                Console.WriteLine("1. Actividad 5: Gestión de turnos en una clínica");
                Console.WriteLine("2. Reto 6: Simulador de Votaciones");
                Console.Write("Ingrese 1 o 2: ");
                string opcion = Console.ReadLine();
                if (opcion == "1")
                {
                    EdadesPacientes();
                }
                else if (opcion == "2")
                {
                    EjecutarSistemaDeVotacion();
                }
                else
                {
                    Console.WriteLine("Opción no válida. Saliendo del programa.");
                }
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

}
