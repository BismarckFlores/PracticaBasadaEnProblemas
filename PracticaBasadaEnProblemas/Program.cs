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

                Console.WriteLine("Ingrese la edad del paciente (o -1 para terminar): ");

                for (int i = 0; i < edades.Length; i++)
                {
                    Console.Write($"Paciente {i + 1}: ");
                    edades[i] = Convert.ToInt32(Console.ReadLine());
                    sumaTotal += edades[i];

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
                    Console.WriteLine("Alerta: Más de 5 personas mayores registradas... ¡¡¡ALTO RIESGO!!!");
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
        }
    }
}
