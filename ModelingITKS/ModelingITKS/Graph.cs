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
        public PriorityQueue Queue;
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
            Queue = new PriorityQueue();
            QueueMap = new Dictionary<int, int>();
        }

        public void AddMessage(Message message)
        {
            Queue.AddMessage(message,NumberRouter);
            //if (Queue.Count != QueueSize)
            //{
            //    Console.WriteLine($"Router { NumberRouter} add message { message.TimeOfProcessing:F6}, to { message.NumberRouter}");
            //    Queue.Add(message);
            //}
            //else
            //{
            //    var first = Queue.First();
            //    if (first.Priority < message.Priority)
            //    {
            //        Queue.RemoveAt(0);
            //        Console.WriteLine($"Queue is full. Router { NumberRouter} add message { message.TimeOfProcessing:F6}, to { message.NumberRouter}, delete last queue message { first.Priority} < { message.Priority}");
            //        Queue.Add(message);
            //    }

            //    Routing.CountLoseMessage++;
            //}
            //Queue = Queue.OrderBy(x => x.Priority).ToList();
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
                        Guid= Guid.NewGuid(),
                        NumberRouter = rnd.Next(0, max),
                        Priority = rnd.Next(0, 3),
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
        public Guid Guid { get; set; }
        public int NumberRouter { get; set; }
        public double TimeOfProcessing { get; set; }
        public bool IsDown { get; set; }
        public int Priority { get; set; }
    }

    public class PriorityQueue
    {
        private Dictionary<int, int> PriorityAmount;
        private Dictionary<int, List<Message>> MessagesP;
        public PriorityQueue()
        {
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            PriorityAmount = new Dictionary<int, int>();
            MessagesP= new Dictionary<int, List<Message>>();
            int amount = 2;
            for (int i = 0; i <= amount; i++)
            {
                PriorityAmount.Add(i, rnd.Next(2, 4));
                MessagesP.Add(i,new List<Message>());
            }
        }
        public List<Message> Messages => GetMessages();

        private List<Message> GetMessages()
        {
            List<Message> messages = new List<Message>();
            foreach (var item in MessagesP)
            {
                messages.AddRange(item.Value);
            }

            return messages;
        }

        public void DeleteMessage(Message message)
        {
            foreach (var item in MessagesP)
            {
                if (item.Value.Contains(message))
                {
                    item.Value.Remove(message);
                }
            }
        }

        public void AddMessage(Message message, int numberRouter)
        {
            var mes = MessagesP[message.Priority];
            if (mes.Count < PriorityAmount[message.Priority])
            {
                mes.Add(message);
            }
            else
            {
                Routing.CountLoseMessage++;
                Console.WriteLine($"Queue is full. Router { numberRouter} add message { message.TimeOfProcessing:F6}, to { message.NumberRouter},Priority { message.Priority}");
            }
        }
    }

}
