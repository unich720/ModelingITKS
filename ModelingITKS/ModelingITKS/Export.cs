using ModelingITKS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace GraphLabs
{
    class Export
    {
        int max;
        string pathToFile;
        public Export(int max,string pathToFile)
        {
            this.max = max;
            this.pathToFile = pathToFile;
        }

        public void ExportGraph()
        {
            Excel.Application excel_app = new Excel.Application();
            //excel_app.Visible = true;
            excel_app.Workbooks.Add();
            Excel._Worksheet workSheet = excel_app.ActiveSheet;
            // Установить заголовки столбцов в ячейках
            workSheet.Cells[1, "A"] = "Source";
            workSheet.Cells[1, "B"] = "Target";
            //workSheet.Cells[1, "C"] = "Weight";
            int str = 2;

            foreach (var item in Routing.InfoExtlList)
            {
                workSheet.Cells[str, "A"] = item.Key;
                workSheet.Cells[str, "B"] = item.Value;
                str++;
            }
            //for (int i = 1; i < Routing.InfoExtlList.Count; i++)
            //{
            //    workSheet.Cells[str + 1, "A"]=

            //    for (int l = 0; l < graph[i].Length; l++)
            //    {
            //        Random rn = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            //        workSheet.Cells[str + 1, "A"] = i.ToString();
            //        workSheet.Cells[str + 1, "B"] = graph[i][l].ToString();
            //        //workSheet.Cells[str + 1, "C"] = rn.Next(2, 100).ToString();
            //        str++;
            //    }
            //    //textBox1.Text += Environment.NewLine;
            //}
            //excel_app.DisplayAlerts = false;
            workSheet.SaveAs(pathToFile);
            excel_app.Quit();
        }

        public void ExportGraphStruct(List<GraphStruct> graph)
        {
            Excel.Application excel_app = new Excel.Application();
            //excel_app.Visible = true;
            excel_app.Workbooks.Add();
            Excel._Worksheet workSheet = excel_app.ActiveSheet;
            // Установить заголовки столбцов в ячейках
            workSheet.Cells[1, "A"] = "Source";
            workSheet.Cells[1, "B"] = "Target";
            workSheet.Cells[1, "C"] = "Weight";
            int str = 1;
            for (int i = 0; i < graph.Count; i++)
            {
                //Random rn = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                workSheet.Cells[str + 1, "A"] = graph[i].Source.ToString();
                workSheet.Cells[str + 1, "B"] = graph[i].Target.ToString();
                workSheet.Cells[str + 1, "C"] = graph[i].Weight.ToString();
                str++;
                //textBox1.Text += Environment.NewLine;
            }
            //excel_app.DisplayAlerts = false;
            workSheet.SaveAs(pathToFile);
            excel_app.Quit();
        }
        public void ExportGraphD(int[,] graph)
        {
            Excel.Application excel_app = new Excel.Application();
            //excel_app.Visible = true;
            excel_app.Workbooks.Add();
            Excel._Worksheet workSheet = excel_app.ActiveSheet;
            // Установить заголовки столбцов в ячейках
            workSheet.Cells[1, "A"] = "Source";
            workSheet.Cells[1, "B"] = "Target";
            workSheet.Cells[1, "C"] = "Weight";
            int str = 1;
            for (int i = 0; i < max; i++)
            {
                for (int l = 0; l < max; l++)
                {
                    Random rn = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                    workSheet.Cells[str + 1, "A"] = i.ToString();
                    workSheet.Cells[str + 1, "B"] = (l+max).ToString();
                    workSheet.Cells[str + 1, "C"] = graph[i,l];
                    str++;
                }
                //textBox1.Text += Environment.NewLine;
            }
            //excel_app.DisplayAlerts = false;
            workSheet.SaveAs(pathToFile);
            excel_app.Quit();
        }


        public void ExportGraphDv2(int[,] graph)
        {
            Excel.Application excel_app = new Excel.Application();
            //excel_app.Visible = true;
            excel_app.Workbooks.Add();
            Excel._Worksheet workSheet = excel_app.ActiveSheet;
            // Установить заголовки столбцов в ячейках
            workSheet.Cells[1, "A"] = "Source";
            workSheet.Cells[1, "B"] = "Target";
            workSheet.Cells[1, "C"] = "Weight";
            int str = 1;
            for (int i = 0; i < max; i++)
            {
                for (int l = 0; l < max; l++)
                {
                    if (i==l)
                    {
                        continue;
                    }
                    Random rn = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                    workSheet.Cells[str + 1, "A"] = i.ToString();
                    workSheet.Cells[str + 1, "B"] = (l).ToString();
                    workSheet.Cells[str + 1, "C"] = graph[i, l];
                    str++;
                }
                //textBox1.Text += Environment.NewLine;
            }
            //excel_app.DisplayAlerts = false;
            workSheet.SaveAs(pathToFile);
            excel_app.Quit();
        }

        public void ExportGraphDv3(int[,] graph)
        {
            Excel.Application excel_app = new Excel.Application();
            //excel_app.Visible = true;
            excel_app.Workbooks.Add();
            Excel._Worksheet workSheet = excel_app.ActiveSheet;
            // Установить заголовки столбцов в ячейках
            workSheet.Cells[1, "A"] = "Source";
            workSheet.Cells[1, "B"] = "Target";
            workSheet.Cells[1, "C"] = "Weight";
            int str = 1;
            for (int i = 1; i < max; i++)
            {
                for (int l = 0; l < max-1; l++)
                {
                    Random rn = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                    workSheet.Cells[str + 1, "A"] = i.ToString();
                    workSheet.Cells[str + 1, "B"] = (l).ToString();
                    workSheet.Cells[str + 1, "C"] = graph[i, l];
                    str++;
                }
                //textBox1.Text += Environment.NewLine;
            }
            //excel_app.DisplayAlerts = false;
            workSheet.SaveAs(pathToFile);
            excel_app.Quit();
        }

    }
}
