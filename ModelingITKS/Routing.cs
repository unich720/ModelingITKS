using GraphLabs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModelingITKS
{
    public class Routing
    {
        public static int CountLoseMessage = 0;

        public static int CountMessageTrue = 0;
        public static double TimeMessageTrue = 0;

        private readonly CancellationTokenSource _cancellationTokenSource = new();
        public List<RouterM> routerMs;
        public List<Task> routers;
        private Task infoTask;
        private Task infoTask2;
        private Task infoTask3;

        public static Dictionary<int, double> InfoExtlList = new Dictionary<int, double>();

        public void PrepareMRing(List<RouterM> routerMs)
        {
            this.routerMs = routerMs;
            routers = new List<Task>();

            //routerMs.First().GenerateMessage(_cancellationTokenSource.Token);
            //routers.Add(Proces(routerMs.First(), _cancellationTokenSource.Token));
            foreach (var item in routerMs)
            {
                item.GenerateMessage(_cancellationTokenSource.Token);
                routers.Add(Task.Run(() => ProcesRing(item, _cancellationTokenSource.Token)));
            }
            Info(_cancellationTokenSource.Token);
            //InfoExc(_cancellationTokenSource.Token);
        }

        public void PrepareMStar(List<RouterM> routerMs)
        {
            this.routerMs = routerMs;
            List<Task> routers = new List<Task>();

            //routerMs.First().GenerateMessage(_cancellationTokenSource.Token);
            //routers.Add(Proces(routerMs.First(), _cancellationTokenSource.Token));
            foreach (var item in routerMs)
            {
                item.GenerateMessage(_cancellationTokenSource.Token);
                routers.Add(Task.Run(() => ProcesStar(item, _cancellationTokenSource.Token)));
            }
            Info(_cancellationTokenSource.Token);
        }

        public void PrepareMRandom(List<RouterM> routerMs)
        {
            this.routerMs = routerMs;
            routers = new List<Task>();

            //routerMs.First().GenerateMessage(_cancellationTokenSource.Token);
            //routerMs.First().GenerateWay(routerMs);
            //routers.Add(Task.Run(() => ProcesRandom(routerMs.First(), _cancellationTokenSource.Token)));
            foreach (var item in routerMs)
            {
                item.GenerateWay(routerMs);
                item.GenerateMessage(_cancellationTokenSource.Token);
                routers.Add(Task.Run(() => ProcesRandom(item, _cancellationTokenSource.Token)));
            }
            Info(_cancellationTokenSource.Token);
        }

        private Task ProcesRing(RouterM routerM, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (routerM.Queue.Any())
                {
                    var sw = Stopwatch.StartNew();
                    var message = routerM.Queue.First();
                    if (message != null)
                    {
                        Console.WriteLine($"Router {routerM.NumberRouter} start message timeOfProcessing {message.TimeOfProcessing:F6}, to {message.NumberRouter}");
                        if (routerM.NumberRouter == message.NumberRouter)
                        {
                            TimeMessageTrue += message.TimeOfProcessing;
                            var task = Task.Delay(TimeSpan.FromSeconds(routerM.TimeOfProcessing), token);
                            task.Wait();
                            message.TimeOfProcessing += sw.Elapsed.TotalSeconds;
                            CountMessageTrue++;
                            Console.WriteLine($"Router {routerM.NumberRouter} stop message timeOfProcessing {message.TimeOfProcessing:F6}, to {message.NumberRouter} is close");
                        }
                        else
                        {
                            int newtRouterM = 0;
                            var minTOP = 1000;
                            foreach (var router in routerM.Target)
                            {
                                if (routerMs[router].TimeOfProcessing < minTOP)
                                {
                                    newtRouterM = router;
                                }
                            }
                            message.TimeOfProcessing += sw.Elapsed.TotalSeconds;
                            Console.WriteLine($"Router {routerM.NumberRouter} stop message timeOfProcessing {message.TimeOfProcessing:F6}, to {message.NumberRouter}");
                            routerMs[newtRouterM].AddMessage(message);
                        }
                        routerM.Queue.RemoveAt(0);
                    }
                }
            };
            return Task.CompletedTask;
        }

        private Task ProcesStar(RouterM routerM, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (routerM.Queue.Any())
                {
                    var sw = Stopwatch.StartNew();
                    var message = routerM.Queue.First();
                    if (message != null)
                    {
                        Console.WriteLine($"Router {routerM.NumberRouter} start message timeOfProcessing {message.TimeOfProcessing:F6}, to {message.NumberRouter}");
                        if (routerM.NumberRouter == message.NumberRouter)
                        {
                            TimeMessageTrue += message.TimeOfProcessing;
                            var task = Task.Delay(TimeSpan.FromSeconds(routerM.TimeOfProcessing), token);
                            task.Wait();
                            message.TimeOfProcessing += sw.Elapsed.TotalSeconds;
                            CountMessageTrue++;
                            Console.WriteLine($"Router {routerM.NumberRouter} stop message timeOfProcessing {message.TimeOfProcessing:F6}, to {message.NumberRouter} is close");
                        }
                        else
                        {
                            int newtRouterM = 0;
                            foreach (var router in routerM.Target)
                            {
                                if (message.NumberRouter == router)
                                {
                                    newtRouterM = router;
                                    break;
                                }
                                else
                                {
                                    newtRouterM = router;
                                }
                            }
                            message.TimeOfProcessing += sw.Elapsed.TotalSeconds;
                            Console.WriteLine($"Router {routerM.NumberRouter} stop message timeOfProcessing {message.TimeOfProcessing:F6}, to {newtRouterM}");
                            routerMs[newtRouterM].AddMessage(message);
                        }
                        routerM.Queue.RemoveAt(0);
                    }
                }
            };
            return Task.CompletedTask;
        }

        public void InfoExc(CancellationToken token)
        {
            Export ex = new Export(default, string.Format(@"{0}\Stat.xlsx", Environment.CurrentDirectory));
            infoTask2 = Task.Run(async () =>
            {
                var i = 0;
                while (!token.IsCancellationRequested)
                {
                    Console.WriteLine("dasdasdas");
                    i++;
                    InfoExtlList.Add(i, TimeMessageTrue / CountMessageTrue);
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
            });

            infoTask3 = Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(TimeSpan.FromSeconds(30));
                    ex.ExportGraph();
                }
            });
        }


        public void Info(CancellationToken token)
        {
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            infoTask = Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine($"Среднее время нахождение сообщения {TimeMessageTrue / CountMessageTrue}");
                    Console.WriteLine($"Потеряных сообщений {CountLoseMessage}");
                    Console.WriteLine("-----------------------------------------");
                    await Task.Delay(TimeSpan.FromSeconds(5));

                    Console.WriteLine($"Конфигурация роутеров");
                    foreach (var item in routerMs)
                    {
                        Console.WriteLine(item.ToString());
                    }
                }
            });
        }

        private Task ProcesRandom(RouterM routerM, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (routerM.Queue.Any())
                {
                    var sw = Stopwatch.StartNew();
                    var message = routerM.Queue.First();
                    if (message != null)
                    {
                        //Console.WriteLine($"Router {routerM.NumberRouter} start message timeOfProcessing {message.TimeOfProcessing:F6}, to {message.NumberRouter}");
                        if (routerM.NumberRouter == message.NumberRouter)
                        {
                            TimeMessageTrue += message.TimeOfProcessing;
                            var task = Task.Delay(TimeSpan.FromSeconds(routerM.TimeOfProcessing), token);
                            task.Wait();
                            message.TimeOfProcessing += sw.Elapsed.TotalSeconds;
                            CountMessageTrue++;
                            Console.WriteLine($"Router {routerM.NumberRouter} stop message timeOfProcessing {message.TimeOfProcessing:F6}, to {message.NumberRouter} is close");
                        }
                        else
                        {
                            //передача пакета другому
                            var newRouterM = routerM.QueueMap.GetValueOrDefault(message.NumberRouter);

                            message.TimeOfProcessing += sw.Elapsed.TotalSeconds;
                            //Console.WriteLine($"Router {routerM.NumberRouter} stop message timeOfProcessing {message.TimeOfProcessing:F6}, to {message.NumberRouter}");
                            routerMs[newRouterM].AddMessage(message);
                        }
                        routerM.Queue.RemoveAt(0);
                    }
                }
            };
            return Task.CompletedTask;
        }








        public void Test(RouterM routerM, Message message)
        {
            Console.WriteLine($"Message {message.TimeOfProcessing}, {message.NumberRouter}");
            while (!message.IsDown)
            {
                if (routerM.NumberRouter == message.NumberRouter)
                {

                    var asd = routerM.TimeOfProcessing - message.TimeOfProcessing;
                    if (asd > 0)
                    {
                        Console.WriteLine("Message processed {0} seconds", asd);
                        message.IsDown = true;
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Message is broken, {0} seconds not enough {1}", -asd, message.TimeOfProcessing);
                        message.IsDown = true;
                        continue;
                    }
                }
                RouterM newtRouterM = default;
                var minTOP = 0;
                //foreach (var router in routerM.Target)
                //{
                //    if (router.TimeOfProcessing < minTOP)
                //    {
                //        newtRouterM = router;
                //    }
                //}
            }
        }
    }
}
