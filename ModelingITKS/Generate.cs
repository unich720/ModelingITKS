using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLabs
{
    class Generate
    {
        int[][] graph;
        int max;
        public Generate(int m)
        {
            max = m;
        }



        public int[][] GenerateGraphv2(int count)
        {
            Random rnd = new Random();
            int[][] graph = new int[max][];
            /*for (int i = 0; i < max; i++)
            {
                graph[i] = new int[count+2];
            }*/
            for (int i = 0; i < max; i++)
            {
                int Countlife = rnd.Next(count - 2, count + 4);
                if (Countlife > i && i != 0)
                {
                    if (i == 2)
                    {
                        graph[i] = new int[1];
                        Countlife = 1;
                    }
                    else
                    {
                        graph[i] = new int[i];
                    }
                }
                else
                {
                    if (i == 0 || i == 2)
                    {
                        graph[i] = new int[1];
                        Countlife = 1;
                    }
                    else
                    {
                        graph[i] = new int[Countlife];
                    }

                }
                if (i == 0)
                {
                    int k = 0;
                    for (int l = 2; 0 != Countlife; l++)
                    {
                        //graph[i][k] = l;
                        k++;
                        Countlife--;
                    }
                }
                else
                {
                    int k = 0;
                    for (int l = i; 0 != Countlife; l--)
                    {
                        if (l == 0)
                        {
                            break;
                        }
                        if (k == 0)
                        {
                            graph[i][k] = l - 1;
                            Countlife--;
                            k++;
                            continue;
                        }
                        if (rnd.Next(1, 100) >= 50 && i > 15)
                        {
                            graph[i][k] = l - 1;
                            Countlife--;
                            k++;
                        }
                        else if (i <= 15)
                        {
                            graph[i][k] = l - 1;
                            Countlife--;
                            k++;
                        }



                    }
                }
            }
            //for (int i = 0; i < max; i++)
            //{
            //    Console.Write(i.ToString() + ": ");
            //    for (int l = 0; l < graph[i].Length; l++)
            //    {
            //        Console.Write(graph[i][l].ToString() + ",");
            //    }
            //    Console.WriteLine();
            //}


            return graph;
        }
        public int[][] GenerateDirected(int count)
        {
            Random rnd = new Random();
            List<GraphList> graph = new List<GraphList>();
            //int[][] graph = new int[max][];
            for (int i = 0; i < max; i++)
            {
                graph.Add(new GraphList());
            }
            for (int i = 0; i < max; i++)
            {
                int Countlife = rnd.Next(count, count + 2);
                if (i == 0)
                {
                    int k = 0;
                    for (int l = 2; 0 != Countlife; l++)
                    {
                        //graph[i].vyrex.Add(l - 1);
                        //graph[l].vyrex.Add(i);
                        k++;
                        Countlife--;
                    }
                }
                else
                {
                    int k = 0;
                    for (int l = i; 0 != Countlife; l--)
                    {
                        if (l == 0)
                        {
                            break;
                        }
                        if (k == 0)
                        {
                            graph[i].vyrtex.Add(l - 1);
                            graph[l - 1].vyrtex.Add(i);
                            Countlife--;
                            k++;
                            continue;
                        }
                        if (rnd.Next(1, 100) >= 50 && i > 10)
                        {
                            graph[i].vyrtex.Add(l - 1);
                            graph[l - 1].vyrtex.Add(i);
                            Countlife--;
                            k++;
                        }
                        else if (i <= 10)
                        {
                            graph[i].vyrtex.Add(l - 1);
                            graph[l - 1].vyrtex.Add(i);
                            Countlife--;
                            k++;
                        }



                    }
                }
            }
            int[][] temp = new int[max][];

            for (int i = 0; i < max; i++)
            {
                temp[i] = new int[graph[i].vyrtex.Count];
                graph[i].vyrtex.Sort();
                //textBox1.Text += i + ": ";
                for (int k = 0; k < graph[i].vyrtex.Count; k++)
                {
                    temp[i][k] = graph[i].vyrtex[k];
                    //textBox1.Text += graph[i].vyrex[k].ToString() + ",";
                }
                //textBox1.Text += Environment.NewLine;
            }
            return temp;
        }
        public int[][] ToArray(int max, List<GraphList> graph)
        {
            int[][] temp = new int[max][];

            for (int i = 0; i < max; i++)
            {
                temp[i] = new int[graph[i].vyrtex.Count];
                graph[i].vyrtex.Sort();
                //textBox1.Text += i + ": ";
                for (int k = 0; k < graph[i].vyrtex.Count; k++)
                {
                    temp[i][k] = graph[i].vyrtex[k];
                    //textBox1.Text += graph[i].vyrex[k].ToString() + ",";
                }
                //textBox1.Text += Environment.NewLine;
            }
            return temp;
        }

        public List<GraphStruct> GenerateGraphv3(int count)
        {
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            List<GraphStruct> graph = new List<GraphStruct>();
            //for (int i = 0; i < max; i++)
            //{
            //    graph.Add(new GraphStruct());
            //}
            //int beg = rnd.Next(1, max);
            //int end = rnd.Next(1, max);
            //while (beg < end)
            //{
            //    beg = rnd.Next(1, max);
            //    end = rnd.Next(1, max);
            //}
            for (int i = 0; i < max; i++)
            {
                //int Countlife = rnd.Next(count, count+1 );
                int Countlife = count;
                if (i == 0)
                {
                    continue;
                    //int k = 0;
                    //for (int l = 2; 0 != Countlife; l++)
                    //{
                    //    //graph[i][k] = l;
                    //    k++;
                    //    Countlife--;
                    //}
                }
                else
                {
                    int k = 0;
                    for (int l = i; 0 != Countlife; l--)
                    {
                        if (l == 0)
                        {
                            break;
                        }
                        //if (beg==i  && i > 15 || end == i)
                        //{
                        //    graph.Add(new GraphStruct(i, l - 1, 0));
                        //    Countlife--;
                        //    k++;
                        //    continue;
                        //}
                        if (Countlife == 1)
                        {
                            graph.Add(new GraphStruct(i, l - 1, rnd.Next(1, 100)));
                            Countlife--;
                            continue;
                        }
                        if (rnd.Next(1, 100) >= 50 && i > 15)
                        {
                            //if (rnd.Next(1, 100) >= 50 && i > 15)
                            //{
                            //    graph.Add(new GraphStruct(i, 0, rnd.Next(1, 100)));
                            //    Countlife--;
                            //    k++;
                            //    continue;
                            //}
                            graph.Add(new GraphStruct(i, l - 1, rnd.Next(1, 100)));
                            Countlife--;
                        }
                        else if (Countlife == i)
                        {
                            graph.Add(new GraphStruct(i, l - 1, rnd.Next(1, 100)));
                            Countlife--;
                        }
                        if (i <= 15)
                        {
                            graph.Add(new GraphStruct(i, l - 1, rnd.Next(1, 100)));
                            Countlife--;
                            k++;
                        }
                    }
                }
            }
            return graph;
        }

        public List<GraphStruct> GenerateGraphv3DirectedM(int count)
        {
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            List<RouterM> Routers = new List<RouterM>();
            List<GraphStruct> graph = new List<GraphStruct>();

            int beg = rnd.Next(1, max);
            int end = rnd.Next(1, max);
            while (beg < end)
            {
                beg = rnd.Next(1, max);
                end = rnd.Next(1, max);
            }
            for (int i = 0; i < max; i++)
            {
                int Countlife = rnd.Next(count - 2, count + 4);
                if (i == 0)
                {
                    int k = 0;
                    for (int l = 2; 0 != Countlife; l++)
                    {
                        k++;
                        Countlife--;
                    }
                }
                else
                {
                    int k = 0;
                    for (int l = i; 0 != Countlife; l--)
                    {
                        if (l == 0)
                        {
                            break;
                        }
                        if (Countlife == 1)
                        {
                            int temp = rnd.Next(1, 100);
                            graph.Add(new GraphStruct(i, l - 1, temp));
                            graph.Add(new GraphStruct(l - 1, i, temp));
                            Countlife--;
                            continue;
                        }
                        if (rnd.Next(1, 100) >= 50 && i > 15)
                        {
                            int temp = rnd.Next(1, 100);
                            graph.Add(new GraphStruct(i, l - 1, temp));
                            graph.Add(new GraphStruct(l - 1, i, temp));
                            Countlife--;
                        }
                        else if (i <= 15)
                        {
                            int temp = rnd.Next(1, 100);
                            graph.Add(new GraphStruct(i, l - 1, temp));
                            graph.Add(new GraphStruct(l - 1, i, temp));
                            Countlife--;
                            k++;
                        }
                    }
                }
            }
            return graph;
        }

        public List<RouterM> GenerateGraphv4M(int count)
        {
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            List<RouterM> Routers = new List<RouterM>();
            for (int i = 0; i < max; i++)
            {
                RouterM routerM = new RouterM(i, rnd.Next(1, 5), rnd.Next(1, 5), max, rnd.Next(5, 10));
                Routers.Add(routerM);
            }

            for (int i = max-1; i !=0; i--)
            {
                //RouterM routerM = new RouterM(i, rnd.Next(1, 5), rnd.Next(1, 5), max, rnd.Next(5, 10));
                int Countlife = rnd.Next(1, count);
                if (i<count)
                {
                    Countlife= rnd.Next(1, i);
                }
                
                List<int> re = new();
                for (int l = 0; l < Countlife; l++)
                {
                    var min = 1;
                    
                    var te = rnd.Next(min, i);
                    while (te==i || re.Contains(te))
                    {
                        if (min==i)
                        {
                            te--;
                            break;
                        }
                        te = rnd.Next(min, i);
                    }
                    Routers[i].AddTarget(te);
                    Routers[te].AddTarget(i);
                    re.Add(te);
                }
            }
            return Routers;
        }

        public List<GraphStruct> GenerateGraphv3Star()
        {
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            List<GraphStruct> graph = new List<GraphStruct>();
            for (int i = 0; i < max; i++)
            {
                if (i == 0)
                {
                    continue;
                }
                graph.Add(new GraphStruct(0, i, rnd.Next(1, 100)));
            }
            return graph;
        }

        public List<RouterM> GenerateGraphv4Star()
        {
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            List<RouterM> Routers = new List<RouterM>();
            RouterM first = null;
            for (int i = 0; i < max; i++)
            {
                RouterM routerM = new RouterM(i, rnd.Next(1, 5), rnd.Next(1, 5), max, rnd.Next(5, 10));
                if (i == 0)
                {
                    first = routerM;
                    Routers.Add(routerM);
                    continue;
                }
                routerM.AddTarget(0);
                first.AddTarget(i);
                Routers.Add(routerM);
            }
            Routers[0] = first;
            return Routers;
        }

        public List<GraphStruct> GenerateGraphv3Ring()
        {
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            List<GraphStruct> graph = new List<GraphStruct>();
            for (int i = 0; i <= max; i++)
            {
                if (i == max - 1)
                {
                    graph.Add(new GraphStruct(i, 0, rnd.Next(1, 100)));
                    break;
                }
                graph.Add(new GraphStruct(i, i + 1, rnd.Next(1, 100)));
            }
            return graph;
        }

        public List<RouterM> GenerateGraphv4Ring()
        {
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            List<RouterM> Routers = new List<RouterM>();
            for (int i = 0; i <= max; i++)
            {
                RouterM routerM = new RouterM(i, rnd.Next(1, 5), rnd.Next(1, 5), max, rnd.Next(5, 10));
                if (i == max - 1)
                {
                    routerM.AddTarget(0);
                    Routers.Add(routerM);
                    break;
                }
                routerM.AddTarget(i + 1);

                Routers.Add(routerM);
            }
            return Routers;
        }
        //public static RouterM routerMf;

        //public RouterM GenerateGraphv5Ring(int i = 1)
        //{
        //    Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
        //    RouterM routerM = new RouterM(i, rnd.Next(1, 5), rnd.Next(1, 5));
        //    if (i == max)
        //    {
        //        routerM.AddTarget(routerMf);
        //        return routerM;
        //    }
        //    if (i==1)
        //    {
        //        routerMf = routerM;
        //    }
        //    routerM.AddTarget(GenerateGraphv5Ring(i + 1));
        //    return routerM;
        //}



        public List<GraphStruct> GenerateGraphv3Random(int count)
        {
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            List<GraphStruct> graph = new List<GraphStruct>();

            for (int i = 0; i < max; i++)
            {
                int Countlife = count;
                if (i == 0)
                {
                    continue;
                }
                else
                {
                    int k = 0;
                    for (int l = i; 0 != Countlife; l--)
                    {
                        if (l == 0)
                        {
                            break;
                        }
                        if (Countlife == 1)
                        {
                            graph.Add(new GraphStruct(i, l - 1, rnd.Next(1, 100)));
                            Countlife--;
                            continue;
                        }
                        if (rnd.Next(1, 100) >= 50 && i > 15)
                        {
                            graph.Add(new GraphStruct(i, l - 1, rnd.Next(1, 100)));
                            Countlife--;
                        }
                        else if (Countlife == i)
                        {
                            graph.Add(new GraphStruct(i, l - 1, rnd.Next(1, 100)));
                            Countlife--;
                        }
                        if (i <= 15)
                        {
                            graph.Add(new GraphStruct(i, l - 1, rnd.Next(1, 100)));
                            Countlife--;
                            k++;
                        }
                    }
                }
            }
            return graph;
        }

        public List<GraphStruct> GenerateGraphv3Directed(int count)
        {
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            List<GraphStruct> graph = new List<GraphStruct>();

            //for (int i = 0; i < max; i++)
            //{
            //    graph.Add(new GraphStruct());
            //}
            int beg = rnd.Next(1, max);
            int end = rnd.Next(1, max);
            while (beg < end)
            {
                beg = rnd.Next(1, max);
                end = rnd.Next(1, max);
            }
            for (int i = 0; i < max; i++)
            {
                int Countlife = rnd.Next(count - 2, count + 4);
                if (i == 0)
                {
                    int k = 0;
                    for (int l = 2; 0 != Countlife; l++)
                    {
                        //graph[i][k] = l;
                        k++;
                        Countlife--;
                    }
                }
                else
                {
                    int k = 0;
                    for (int l = i; 0 != Countlife; l--)
                    {
                        if (l == 0)
                        {
                            break;
                        }
                        //if (beg==i  && i > 15 || end == i)
                        //{
                        //    graph.Add(new GraphStruct(i, l - 1, 0));
                        //    Countlife--;
                        //    k++;
                        //    continue;
                        //}
                        if (Countlife == 1)
                        {
                            int temp = rnd.Next(1, 100);
                            graph.Add(new GraphStruct(i, l - 1, temp));
                            graph.Add(new GraphStruct(l - 1, i, temp));
                            Countlife--;
                            continue;
                        }
                        if (rnd.Next(1, 100) >= 50 && i > 15)
                        {
                            //if (rnd.Next(1, 100) >= 50 && i > 15)
                            //{
                            //    graph.Add(new GraphStruct(i, 0, rnd.Next(1, 100)));
                            //    Countlife--;
                            //    k++;
                            //    continue;
                            //}
                            int temp = rnd.Next(1, 100);
                            graph.Add(new GraphStruct(i, l - 1, temp));
                            graph.Add(new GraphStruct(l - 1, i, temp));
                            Countlife--;
                        }
                        else if (i <= 15)
                        {
                            int temp = rnd.Next(1, 100);
                            graph.Add(new GraphStruct(i, l - 1, temp));
                            graph.Add(new GraphStruct(l - 1, i, temp));
                            Countlife--;
                            k++;
                        }
                    }
                }
            }
            return graph;
        }

        public int[,] GenerateGraphv4(int count)
        {
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            int[,] graph = new int[max, max];
            for (int i = 0; i < max; i++)
            {
                for (int l = 0; l < max; l++)
                {

                    graph[i, l] = rnd.Next(1, 100);
                }
            }
            return graph;
        }
        public int[,] GenerateGraphv5(int count)
        {
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            int[,] graph = new int[max, max];
            for (int i = 0; i < max; i++)
            {
                for (int l = 0; l < max - 1; l++)
                {
                    if (i == 0)
                    {
                        graph[i, l] = 0;
                        continue;
                    }
                    graph[i, l] = rnd.Next(1, 100);
                }
            }
            return graph;
        }

        public int[,] GenerateGraphv7(int count)
        {
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            int[,] graph = new int[max, max];
            for (int i = 0; i < max; i++)
            {
                for (int l = 0; l < max; l++)
                {
                    var temp = rnd.Next(1, 100);
                    graph[i, l] = temp;
                    graph[l, i] = temp;
                }
            }
            return graph;
        }

        public List<GraphStruct> GenerateGraphv6(int count)
        {
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            List<GraphStruct> graph = new List<GraphStruct>();
            //for (int i = 0; i < max; i++)
            //{
            //    graph.Add(new GraphStruct());
            //}
            //int beg = rnd.Next(1, max);
            //int end = rnd.Next(1, max);
            //while (beg < end)
            //{
            //    beg = rnd.Next(1, max);
            //    end = rnd.Next(1, max);
            //}
            for (int i = 0; i < max; i++)
            {
                //int Countlife = rnd.Next(count, count+1 );
                int Countlife = count;
                if (i == 0)
                {
                    continue;
                    //int k = 0;
                    //for (int l = 2; 0 != Countlife; l++)
                    //{
                    //    //graph[i][k] = l;
                    //    k++;
                    //    Countlife--;
                    //}
                }
                else
                {
                    int k = 0;
                    for (int l = i; 0 != Countlife; l--)
                    {
                        if (l == 0)
                        {
                            break;
                        }
                        //if (beg==i  && i > 15 || end == i)
                        //{
                        //    graph.Add(new GraphStruct(i, l - 1, 0));
                        //    Countlife--;
                        //    k++;
                        //    continue;
                        //}
                        if (Countlife == 1)
                        {
                            graph.Add(new GraphStruct(i, l - 1, rnd.Next(1, 100)));
                            Countlife--;
                            continue;
                        }
                        if (rnd.Next(1, 100) >= 50 && i > 15)
                        {
                            //if (rnd.Next(1, 100) >= 50 && i > 15)
                            //{
                            //    graph.Add(new GraphStruct(i, 0, rnd.Next(1, 100)));
                            //    Countlife--;
                            //    k++;
                            //    continue;
                            //}
                            graph.Add(new GraphStruct(i, l - 1, rnd.Next(1, 100)));
                            Countlife--;
                        }
                        else if (i <= 15)
                        {
                            graph.Add(new GraphStruct(i, l - 1, rnd.Next(1, 100)));
                            Countlife--;
                            k++;
                        }
                    }
                }
            }
            return graph;
        }
        public int[][] GenerateDirectedDFS(int count)
        {
            Random rnd = new Random();
            List<GraphList> graph = new List<GraphList>();
            //int[][] graph = new int[max][];
            for (int i = 0; i < max; i++)
            {
                graph.Add(new GraphList());
            }
            for (int i = 0; i < max; i++)
            {
                int Countlife = rnd.Next(0, count);
                if (i == 0)
                {
                    int k = 0;
                    for (int l = 2; 0 != Countlife; l++)
                    {
                        //graph[i].vyrex.Add(l - 1);
                        //graph[l].vyrex.Add(i);
                        k++;
                        Countlife--;
                    }
                }
                else
                {
                    int k = 0;
                    for (int l = i; 0 != Countlife; l--)
                    {
                        if (l == 0)
                        {
                            break;
                        }
                        if (k == 0)
                        {
                            graph[i].vyrtex.Add(l - 1);
                            graph[l - 1].vyrtex.Add(i);
                            Countlife--;
                            k++;
                            continue;
                        }
                        if (rnd.Next(1, 100) >= 50 && i > 10)
                        {
                            graph[i].vyrtex.Add(l - 1);
                            graph[l - 1].vyrtex.Add(i);
                            Countlife--;
                            k++;
                        }
                        else if (i <= 10)
                        {
                            graph[i].vyrtex.Add(l - 1);
                            graph[l - 1].vyrtex.Add(i);
                            Countlife--;
                            k++;
                        }



                    }
                }
            }
            int[][] temp = new int[max][];

            for (int i = 0; i < max; i++)
            {
                temp[i] = new int[graph[i].vyrtex.Count];
                graph[i].vyrtex.Sort();
                //textBox1.Text += i + ": ";
                for (int k = 0; k < graph[i].vyrtex.Count; k++)
                {
                    temp[i][k] = graph[i].vyrtex[k];
                    //textBox1.Text += graph[i].vyrex[k].ToString() + ",";
                }
                //textBox1.Text += Environment.NewLine;
            }
            return temp;
        }
        public List<GraphStruct> GenerateGraphv3DFS(int count)
        {
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            List<GraphStruct> graph = new List<GraphStruct>();
            //for (int i = 0; i < max; i++)
            //{
            //    graph.Add(new GraphStruct());
            //}
            //int beg = rnd.Next(1, max);
            //int end = rnd.Next(1, max);
            //while (beg < end)
            //{
            //    beg = rnd.Next(1, max);
            //    end = rnd.Next(1, max);
            //}
            for (int i = 0; i < max; i++)
            {
                //int Countlife = rnd.Next(count, count+1 );
                int Countlife = rnd.Next(0, count);
                if (i == 0)
                {
                    continue;
                    //int k = 0;
                    //for (int l = 2; 0 != Countlife; l++)
                    //{
                    //    //graph[i][k] = l;
                    //    k++;
                    //    Countlife--;
                    //}
                }
                else
                {
                    int k = 0;
                    for (int l = i; 0 != Countlife; l--)
                    {
                        if (l == 0)
                        {
                            break;
                        }
                        //if (beg==i  && i > 15 || end == i)
                        //{
                        //    graph.Add(new GraphStruct(i, l - 1, 0));
                        //    Countlife--;
                        //    k++;
                        //    continue;
                        //}
                        if (Countlife == 1)
                        {
                            graph.Add(new GraphStruct(i, l - 1, rnd.Next(1, 100)));
                            Countlife--;
                            continue;
                        }
                        if (rnd.Next(1, 100) >= 50 && i > 15)
                        {
                            //if (rnd.Next(1, 100) >= 50 && i > 15)
                            //{
                            //    graph.Add(new GraphStruct(i, 0, rnd.Next(1, 100)));
                            //    Countlife--;
                            //    k++;
                            //    continue;
                            //}
                            graph.Add(new GraphStruct(i, l - 1, rnd.Next(1, 100)));
                            Countlife--;
                        }
                        else if (Countlife == i)
                        {
                            graph.Add(new GraphStruct(i, l - 1, rnd.Next(1, 100)));
                            Countlife--;
                        }
                        if (i <= 15)
                        {
                            graph.Add(new GraphStruct(i, l - 1, rnd.Next(1, 100)));
                            Countlife--;
                            k++;
                        }
                    }
                }
            }
            return graph;
        }

    }
}
