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
        }

        public void AddMessage(Message message)
        {
            if (Queue.Count != QueueSize)
            {
                Console.WriteLine($"Router { NumberRouter} add message { message.TimeOfProcessing :F6}, to { message.NumberRouter}");
                Queue.Add(message);
            }
            else
            {
                Routing.CountLoseMessage++;
            }
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
                    });
                    await Task.Delay(TimeSpan.FromSeconds(timeSpawn));
                }
            });
        }

        public int WayTo(int numberRouter)
        {
            foreach (var item in Target)
            {
                if (numberRouter== item)
                {
                    return item;
                }
            }



            return default;
        }

        public override string ToString()
        {
            return String.Format("NumberRouter: {0}, QueueSize: {1}, TimeOfProcessing: {2}", NumberRouter, QueueSize, TimeOfProcessing);
        }
    }

    public class Message
    {
        public int NumberRouter;
        public double TimeOfProcessing;
        public bool IsDown;
    }

    public class WayClass
    {
        public int Amount;
        public List<int> TimeOfProcessing;
        public bool IsDown;
    }
}
