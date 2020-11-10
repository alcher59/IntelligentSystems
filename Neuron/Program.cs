using System;
using System.Collections.Generic;
using System.Linq;

namespace Neuron
{
    class Program
    {
        static List<double> x1 = new double[] { 1, 9.4, 2.5, 8, 0.5, 7.9, 7, 2.8, 1.2, 7.8 }.ToList();  //темп
        static List<double> x2 = new double[] { 1, 6.4, 2.1, 7.7, 2.2, 8.4, 7, 0.8, 3, 6.1 }.ToList();  //вибрация
        static List<double> x3 = new double[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }.ToList(); //порог
        static List<double> y = new double[] { 1, -1, 1, -1, 1, -1, -1, 1, 1, -1 }.ToList();
        static double k1 = 0.75, k2 = 0.5, k3 = -0.6;
        static double c = 0.2;

        static double net = 0, s = 0;
        static double dk1 = 0, dk2 = 0, dk3 = 0;
        static int errCount = 1;

        public static void Teach()
        {
            int i = 0;
            while (errCount != 0)
            {
                Console.WriteLine($"-------------Эпоха {i + 1}-------------");
                errCount = 0;
                for (int j = 0; j < x1.Count; j++)
                {
                    s = k1 * x1[j] + k2 * x2[j] + k3 * x3[j];
                    if (s < 0) net = -1; else net = 1;
                    if (net != y[j])
                    {
                        errCount++;

                        dk1 = c * (y[j] - net) * x1[j];
                        dk2 = c * (y[j] - net) * x2[j];
                        dk3 = c * (y[j] - net) * x3[j];

                        k1 = k1 + dk1;
                        k2 = k2 + dk2;
                        k3 = k3 + dk3;
                    }
                    Console.WriteLine($"\n            (Шаг {j + 1})           ");
                    Console.WriteLine("s = " + Math.Round(s, 4) + "    y = " + y[j] + "    net = " + net);
                    Console.WriteLine("k1: " + Math.Round(k1, 4) + "    k2: " + Math.Round(k2, 4) + "   k3: " + Math.Round(k3, 4));
                }
                i++;
                Console.WriteLine("\nКол-во ошибок: " + errCount + "\n");
            } 
        }

        static void Main(string[] args)
        {
            Teach();
        }
    }
}
