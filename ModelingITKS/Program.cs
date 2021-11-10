using GraphLabs;
using System;
using System.Collections.Generic;

namespace ModelingITKS
{
    class Program
    {
        static void Main(string[] args)
        {
            const int MAX = 10;
            string pathToFile = string.Format(@"{0}\Graph.xlsx", Environment.CurrentDirectory);
            string pathToFile2 = string.Format(@"{0}\Graph2.xlsx", Environment.CurrentDirectory);
            string pathToFile3 = string.Format(@"{0}\Graph3.xlsx", Environment.CurrentDirectory);
            Generate k = new Generate(MAX);

            var tempGraphRing = k.GenerateGraphv4Ring();
            var tempGraphStar = k.GenerateGraphv4Star();
            var tempGraphRandom = k.GenerateGraphv4M(3);

            //var tempGraphRing = k.GenerateGraphv3Star();
            //var tempGraphCircle = k.GenerateGraphv3Ring();
            //var tempGraphRandom = k.GenerateGraphv3(3);

            Routing routing = new Routing();
            routing.PrepareMRandom(tempGraphRandom);

            Console.WriteLine("Граф создан");

            //Export ex1 = new Export(MAX, pathToFile);
            //ex1.ExportGraphStruct(tempGraphRing);
            //Export ex2 = new Export(MAX, pathToFile2);
            //ex2.ExportGraphStruct(tempGraphCircle);
            //Export ex3 = new Export(MAX, pathToFile3);
            //ex3.ExportGraphStruct(tempGraphRandom);

            Console.WriteLine("Граф сохранен в файл");


            Console.ReadKey();
        }
    }
}
