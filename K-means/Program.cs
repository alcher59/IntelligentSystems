using System;
using System.Collections.Generic;

namespace K_means
{
    class Program
    {
        static int[] x = { 1, 1, 4, 5, 1, 3, 2, 1, 2, 3, 1, 6, 2, 2, 1}; //вес
        static int[] y = { 3, 2, 3, 2, 1, 3, 3, 5, 2, 4, 2, 3, 3, 3, 6}; //объем
        static int[] z = { 2, 1, 5, 2, 1, 3, 1, 1, 2, 3, 5, 1, 2, 2, 1};  //цена
        static List<int> temp_cluster1;
        static List<int> temp_cluster2;
        static List<int> temp_cluster3;
        static List<int> temp_cluster4;


        static int count_k = 2;

        public static void K2()
        {
            List<int> cluster1 = new List<int>();
            List<int> cluster2 = new List<int>();

            int[] c1 = { 1, 2, 1 }; //центр k1
            int[] c2 = { 3, 2, 2 };  //центр k2

            double[] r1 = new double[15];
            double[] r2 = new double[15];

            bool end = true;
            do
            {
                temp_cluster1 = new List<int>();
                temp_cluster2 = new List<int>();
                Console.WriteLine("r1 | r2");
                for (int i = 0; i < 15; i++)
                {
                    r1[i] = Math.Sqrt(Math.Pow((x[i] - c1[0]), 2) + Math.Pow((y[i] - c1[1]), 2) + Math.Pow((z[i] - c1[2]), 2));
                    r2[i] = Math.Sqrt(Math.Pow((x[i] - c2[0]), 2) + Math.Pow((y[i] - c2[1]), 2) + Math.Pow((z[i] - c2[2]), 2));
                    Console.WriteLine(Math.Round(r1[i], 2) + " | " + Math.Round(r2[i], 2));
                    if (r1[i] <= r2[i]) temp_cluster1.Add(i); else temp_cluster2.Add(i);
                }

                if (temp_cluster1.Count == cluster1.Count)
                {
                    foreach (var i in temp_cluster1)
                    {
                        if (cluster1.Contains(i))
                        {
                            end = true;
                        }
                        else { end = false; }
                    }
                }
                else
                {
                    end = false;
                }
                if(end == false)
                {
                    cluster1.Clear();
                    cluster1.AddRange(temp_cluster1.ToArray());
                    cluster2.Clear();
                    cluster2.AddRange(temp_cluster2.ToArray());
                    if (cluster1.Count != 0 && cluster2.Count != 0)
                    {
                        ClearArray(c1);
                        ClearArray(c2);
                        for (int i = 0; i < cluster1.Count; i++)
                        {
                            c1[0] += x[cluster1[i]];
                            c1[1] += y[cluster1[i]];
                            c1[2] += z[cluster1[i]];
                        }
                        c1[0] = c1[0] / cluster1.Count;
                        c1[1] = c1[1] / cluster1.Count;
                        c1[2] = c1[2] / cluster1.Count;

                        for (int i = 0; i < cluster2.Count; i++)
                        {
                            c2[0] += x[cluster2[i]];
                            c2[1] += y[cluster2[i]];
                            c2[2] += z[cluster2[i]];
                        }
                        c2[0] = c2[0] / cluster2.Count;
                        c2[1] = c2[1] / cluster2.Count;
                        c2[2] = c2[2] / cluster2.Count;
                    }   
                }
                Console.WriteLine("----------------------------");
                Console.WriteLine("Cluster1: ");
                foreach (var c in cluster1)
                    Console.Write(c + 1 + ", ");
                Console.WriteLine("\n\nCluster2: ");
                foreach (var c in cluster2)
                    Console.Write(c + 1 + ", ");
                Console.WriteLine("\n----------------------------");
                //Console.WriteLine(end);
            } while (end != true);
            
        }

        public static void K3()
        {
            List<int> cluster1 = new List<int>();
            List<int> cluster2 = new List<int>();
            List<int> cluster3 = new List<int>();

            int[] c1 = { 1, 2, 1 }; //центр k1
            int[] c2 = { 3, 2, 2 };  //центр k2
            int[] c3 = { 1, 2, 4 };  //центр k3

            double[] r1 = new double[15];
            double[] r2 = new double[15];
            double[] r3 = new double[15];

            bool end = true;
            do
            {
                temp_cluster1 = new List<int>();
                temp_cluster2 = new List<int>();
                temp_cluster3 = new List<int>();
                Console.WriteLine("r1 | r2 | r3");
                for (int i = 0; i < 15; i++)
                {
                    r1[i] = Math.Sqrt(Math.Pow((x[i] - c1[0]), 2) + Math.Pow((y[i] - c1[1]), 2) + Math.Pow((z[i] - c1[2]), 2));
                    r2[i] = Math.Sqrt(Math.Pow((x[i] - c2[0]), 2) + Math.Pow((y[i] - c2[1]), 2) + Math.Pow((z[i] - c2[2]), 2));
                    r3[i] = Math.Sqrt(Math.Pow((x[i] - c3[0]), 2) + Math.Pow((y[i] - c3[1]), 2) + Math.Pow((z[i] - c3[2]), 2));
                    Console.WriteLine(Math.Round(r1[i], 2) + " | " + Math.Round(r2[i], 2) + " | " + Math.Round(r3[i], 2));
                    if (r1[i] <= r2[i] && r1[i] <= r3[i]) temp_cluster1.Add(i); 
                    else if (r2[i] <= r3[i] ) temp_cluster2.Add(i); else temp_cluster3.Add(i);

                }

                if (temp_cluster1.Count == cluster1.Count && temp_cluster2.Count == cluster2.Count)
                {
                    foreach (var i in temp_cluster1)
                    {
                        if (cluster1.Contains(i))
                        {
                            end = true;
                        }
                        else { end = false; }
                    }
                    foreach (var i in temp_cluster2)
                    {
                        if (cluster2.Contains(i))
                        {
                            end = true;
                        }
                        else { end = false; }
                    }
                }
                else
                {
                    end = false;
                }
                if (end == false)
                {
                    cluster1.Clear();
                    cluster1.AddRange(temp_cluster1.ToArray());
                    cluster2.Clear();
                    cluster2.AddRange(temp_cluster2.ToArray());
                    cluster3.Clear();
                    cluster3.AddRange(temp_cluster3.ToArray());
                    if (cluster1.Count != 0 && cluster2.Count != 0 && cluster3.Count != 0)
                    {
                        ClearArray(c1);
                        ClearArray(c2);
                        ClearArray(c3);
                        for (int i = 0; i < cluster1.Count; i++)
                        {
                            c1[0] += x[cluster1[i]];
                            c1[1] += y[cluster1[i]];
                            c1[2] += z[cluster1[i]];
                        }
                        c1[0] = c1[0] / cluster1.Count;
                        c1[1] = c1[1] / cluster1.Count;
                        c1[2] = c1[2] / cluster1.Count;

                        for (int i = 0; i < cluster2.Count; i++)
                        {
                            c2[0] += x[cluster2[i]];
                            c2[1] += y[cluster2[i]];
                            c2[2] += z[cluster2[i]];
                        }
                        c2[0] = c2[0] / cluster2.Count;
                        c2[1] = c2[1] / cluster2.Count;
                        c2[2] = c2[2] / cluster2.Count;

                        for (int i = 0; i < cluster3.Count; i++)
                        {
                            c3[0] += x[cluster3[i]];
                            c3[1] += y[cluster3[i]];
                            c3[2] += z[cluster3[i]];
                        }
                        c3[0] = c3[0] / cluster3.Count;
                        c3[1] = c3[1] / cluster3.Count;
                        c3[2] = c3[2] / cluster3.Count;
                    }
                }
                Console.WriteLine("----------------------------");
                Console.WriteLine("Cluster1: ");
                foreach (var c in cluster1)
                    Console.Write(c + 1 + ", ");
                Console.WriteLine("\n\nCluster2: ");
                foreach (var c in cluster2)
                    Console.Write(c + 1 + ", ");
                Console.WriteLine("\n\nCluster3: ");
                foreach (var c in cluster3)
                    Console.Write(c + 1 + ", ");
                Console.WriteLine("\n----------------------------");
                //Console.WriteLine(end);
            } while (end != true);


        }

        public static void K4()
        {
            List<int> cluster1 = new List<int>();
            List<int> cluster2 = new List<int>();
            List<int> cluster3 = new List<int>();
            List<int> cluster4 = new List<int>();

            int[] c1 = { 1, 2, 1 }; //центр k1
            int[] c2 = { 3, 2, 2 };  //центр k2
            int[] c3 = { 1, 3, 1 };  //центр k3
            int[] c4 = { 1, 2, 2 };  //центр k4

            double[] r1 = new double[15];
            double[] r2 = new double[15];
            double[] r3 = new double[15];
            double[] r4 = new double[15];

            bool end = true;
            do
            {
                temp_cluster1 = new List<int>();
                temp_cluster2 = new List<int>();
                temp_cluster3 = new List<int>();
                temp_cluster4 = new List<int>();
                Console.WriteLine("r1 | r2 | r3 | r4");
                for (int i = 0; i < 15; i++)
                {
                    r1[i] = Math.Sqrt(Math.Pow((x[i] - c1[0]), 2) + Math.Pow((y[i] - c1[1]), 2) + Math.Pow((z[i] - c1[2]), 2));
                    r2[i] = Math.Sqrt(Math.Pow((x[i] - c2[0]), 2) + Math.Pow((y[i] - c2[1]), 2) + Math.Pow((z[i] - c2[2]), 2));
                    r3[i] = Math.Sqrt(Math.Pow((x[i] - c3[0]), 2) + Math.Pow((y[i] - c3[1]), 2) + Math.Pow((z[i] - c3[2]), 2));
                    r4[i] = Math.Sqrt(Math.Pow((x[i] - c4[0]), 2) + Math.Pow((y[i] - c4[1]), 2) + Math.Pow((z[i] - c4[2]), 2));
                    Console.WriteLine(Math.Round(r1[i], 2) + " | " + Math.Round(r2[i], 2) + " | " + Math.Round(r3[i], 2) + " | " + Math.Round(r4[i], 2));
                    if (r1[i] <= r2[i] && r1[i] <= r3[i] && r1[i] <= r4[i]) temp_cluster1.Add(i);  
                    else if (r2[i] <= r3[i] && r2[i] <= r4[i]) temp_cluster2.Add(i);
                    else if (r3[i] <= r4[i]) temp_cluster3.Add(i); else temp_cluster4.Add(i);

                }

                if (temp_cluster1.Count == cluster1.Count && temp_cluster2.Count == cluster2.Count && temp_cluster3.Count == cluster3.Count)
                {
                    foreach (var i in temp_cluster1)
                    {
                        if (cluster1.Contains(i))
                        {
                            end = true;
                        }
                        else { end = false; }
                    }
                    foreach (var i in temp_cluster2)
                    {
                        if (cluster2.Contains(i))
                        {
                            end = true;
                        }
                        else { end = false; }
                    }
                    foreach (var i in temp_cluster3)
                    {
                        if (cluster3.Contains(i))
                        {
                            end = true;
                        }
                        else { end = false; }
                    }
                }
                else
                {
                    end = false;
                }
                if (end == false)
                {
                    cluster1.Clear();
                    cluster1.AddRange(temp_cluster1.ToArray());
                    cluster2.Clear();
                    cluster2.AddRange(temp_cluster2.ToArray());
                    cluster3.Clear();
                    cluster3.AddRange(temp_cluster3.ToArray());
                    cluster4.Clear();
                    cluster4.AddRange(temp_cluster4.ToArray());
                    if(cluster1.Count != 0 && cluster2.Count != 0 && cluster3.Count != 0 && cluster4.Count != 0) 
                    {
                        ClearArray(c1);
                        ClearArray(c2);
                        ClearArray(c3);
                        ClearArray(c4);
                        for (int i = 0; i < cluster1.Count; i++)
                        {
                            c1[0] += x[cluster1[i]];
                            c1[1] += y[cluster1[i]];
                            c1[2] += z[cluster1[i]];
                        }
                        c1[0] = c1[0] / cluster1.Count;
                        c1[1] = c1[1] / cluster1.Count;
                        c1[2] = c1[2] / cluster1.Count;

                        for (int i = 0; i < cluster2.Count; i++)
                        {
                            c2[0] += x[cluster2[i]];
                            c2[1] += y[cluster2[i]];
                            c2[2] += z[cluster2[i]];
                        }
                        c2[0] = c2[0] / cluster2.Count;
                        c2[1] = c2[1] / cluster2.Count;
                        c2[2] = c2[2] / cluster2.Count;

                        for (int i = 0; i < cluster3.Count; i++)
                        {
                            c3[0] += x[cluster3[i]];
                            c3[1] += y[cluster3[i]];
                            c3[2] += z[cluster3[i]];
                        }
                        c3[0] = c3[0] / cluster3.Count;
                        c3[1] = c3[1] / cluster3.Count;
                        c3[2] = c3[2] / cluster3.Count;

                        for (int i = 0; i < cluster4.Count; i++)
                        {
                            c4[0] += x[cluster4[i]];
                            c4[1] += y[cluster4[i]];
                            c4[2] += z[cluster4[i]];
                        }
                        c4[0] = c4[0] / cluster4.Count;
                        c4[1] = c4[1] / cluster4.Count;
                        c4[2] = c4[2] / cluster4.Count;
                    }
                }
                Console.WriteLine("----------------------------");
                Console.WriteLine("Cluster1: ");
                foreach (var c in cluster1)
                    Console.Write(c + 1 + ", ");
                Console.WriteLine("\n\nCluster2: ");
                foreach (var c in cluster2)
                    Console.Write(c + 1 + ", ");
                Console.WriteLine("\n\nCluster3: ");
                foreach (var c in cluster3)
                    Console.Write(c + 1 + ", ");
                Console.WriteLine("\n\nCluster4: ");
                foreach (var c in cluster4)
                    Console.Write(c + 1 + ", ");
                Console.WriteLine("\n----------------------------");
            } while (end != true);

        }

        public static void ClearArray(int[] array)
        {
            for(int i = 0; i < array.Length; i++)
            {
                array[i] = 0;
            }
        }
        //public static List<int> GetListElements(List<int> list)
        //{
        //    List<int> new_list = new List<int>();
        //    foreach (var i in list)
        //    {
        //        new_list.ad
        //    }
        //}
        static void Main(string[] args)
        {
            Console.Write("Введите кол-во кластеров: ");
            count_k = Convert.ToInt32(Console.ReadLine());
           switch(count_k)
           {
                case 2:
                    K2(); break;
                case 3:
                    K3(); break;
                case 4:
                    K4(); break;
           }
        }
    }
}
