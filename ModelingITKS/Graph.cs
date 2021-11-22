using ModelingITKS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GraphLabs
{
    class GraphList
    {
        public List<int> vyrtex = new List<int>();
        //public Graph(int Source, int Target)
        //{
        //    this.Source = Source;
        //    this.Target = Target;
        //}
    }

    public struct GraphStruct
    {
        public int Source;
        public int Target;
        public int Weight;
        public GraphStruct(int s, int t, int w)
        {
            this.Source = s;
            this.Target = t;
            this.Weight = w;
        }
    }

    public class RouterM
    {
        public int NumberRouter;
        public List<int> Target;
        public int TimeOfProcessing;
        public int QueueSize;
        public List<Message> Queue;
        public int timeSpawn;
        public Dictionary<int, int> QueueMap;

        public int max;

        private Task gMessage;
        public bool IsStart;

        public RouterM(int NumberRouter, int TimeOfProcessing, int QueueSize, int max, int timeSpawn)
        {
            this.NumberRouter = NumberRouter;
            this.TimeOfProcessing = TimeOfProcessing;
            this.QueueSize = QueueSize;
            this.max = max;
            this.timeSpawn = timeSpawn;
            Target = new List<int>();
            Queue = new List<Message>(QueueSize);
            QueueMap = new Dictionary<int, int>();
        }

        public void AddMessage(Message message)
        {
            if (Queue.Count != QueueSize)
            {
                Console.WriteLine($"Router { NumberRouter} add message { message.TimeOfProcessing:F6}, to { message.NumberRouter}");
                Queue.Add(message);
            }
            else
            {
                var first = Queue.First();
                if (first.Priority < message.Priority)
                {
                    Queue.RemoveAt(0);
                    Console.WriteLine($"Queue is full. Router { NumberRouter} add message { message.TimeOfProcessing:F6}, to { message.NumberRouter}, delete last queue message { first.Priority} < { message.Priority}");
                    Queue.Add(message);
                }

                Routing.CountLoseMessage++;
            }
            Queue = Queue.OrderBy(x => x.Priority).ToList();
        }

        public void AddTarget(int router)
        {
            Target.Add(router);
            Target.Distinct();
        }

        public void GenerateMessage(CancellationToken token)
        {
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            IsStart = true;
            gMessage = Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    AddMessage(new Message
                    {
                        NumberRouter = rnd.Next(0, max),
                        Priority = rnd.Next(1, 10),
                    });
                    await Task.Delay(TimeSpan.FromSeconds(timeSpawn));
                }
            });
        }

        public void GenerateWay(List<RouterM> routerMs)
        {
            QueueMap.Add(NumberRouter, NumberRouter);
            foreach (var item in Target)
            {
                QueueMap.Add(item, item);
                if (Target.Count == 1)
                {
                    foreach (var item2 in Enumerable.Range(0, max))
                    {
                        QueueMap.TryAdd(item2, item);
                    }
                    return;
                }
            }
            bool twice = false;
            while (QueueMap.Count != max)
            {
                var range = new Dictionary<int, int>(QueueMap);
                foreach (var item in range)
                {
                    if (item.Key == NumberRouter)
                    {
                        continue;
                    }
                    foreach (var tar in routerMs[item.Key].Target)
                    {
                        if (!QueueMap.ContainsKey(tar))
                        {
                            if (twice)
                                QueueMap.TryAdd(tar, item.Value);
                            else
                                QueueMap.TryAdd(tar, item.Key);

                        }
                    }

                }
                twice = true;
            }
        }

        public override string ToString()
        {
            return String.Format("NumberRouter: {0}, QueueSize: {1}, TimeOfProcessing: {2}", NumberRouter, QueueSize, TimeOfProcessing);
        }
    }

    public class Message
    {
        public int NumberRouter { get; set; }
        public double TimeOfProcessing { get; set; }
        public bool IsDown { get; set; }
        public int Priority { get; set; }
    }

}
