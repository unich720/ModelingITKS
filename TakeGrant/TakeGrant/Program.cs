using System;
using System.Collections.Generic;
using System.Linq;

namespace TakeGrant
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            List<Entity> entities = new List<Entity>();
            List<string> list = new() { "s1","s2", "s3", "s4", "s5", "s6", "o1", "o2", "o3", "o4", "o5" };
            foreach (var item in list)
            {
                if (item.Contains("s"))
                {
                    entities.Add(new Subject {Name=item });
                }
                else
                {
                    entities.Add(new Object { Name = item });
                }
            }

            var transitions = Testing(entities);

            foreach (var item in entities)
            {
                //var ads = transitions.Where(x => x.FromEntity.Name == item.Name || x.InEntity.Name == item.Name);
                item.AddTran(transitions.Where(x => x.FromEntity.Name == item.Name || x.InEntity.Name == item.Name));
            }


            BridgeSearch bridgeSearch = new BridgeSearch();
            bridgeSearch.Search(transitions, entities);

            Console.ReadKey();
        }


        public static List<Transition> Testing(List<Entity> entities)
        {
            List<Transition> transitions = new List<Transition>();

            transitions.Add(new Transition(entities.First(x => x.Name == "s5"), entities.First(x => x.Name == "s6"), ActionEnum.Take));
            transitions.Add(new Transition(entities.First(x => x.Name == "s6"), entities.First(x => x.Name == "o3"), ActionEnum.Take));
            transitions.Add(new Transition(entities.First(x => x.Name == "o3"), entities.First(x => x.Name == "o2"), ActionEnum.Grand));
            transitions.Add(new Transition(entities.First(x => x.Name == "o5"), entities.First(x => x.Name == "o1"), ActionEnum.Grand));
            transitions.Add(new Transition(entities.First(x => x.Name == "o1"), entities.First(x => x.Name == "o2"), ActionEnum.Take));
            transitions.Add(new Transition(entities.First(x => x.Name == "s1"), entities.First(x => x.Name == "o4"), ActionEnum.Take));
            transitions.Add(new Transition(entities.First(x => x.Name == "s1"), entities.First(x => x.Name == "s2"), ActionEnum.Grand));
            transitions.Add(new Transition(entities.First(x => x.Name == "s1"), entities.First(x => x.Name == "s3"), ActionEnum.Grand));
            transitions.Add(new Transition(entities.First(x => x.Name == "s2"), entities.First(x => x.Name == "o4"), ActionEnum.Take));
            transitions.Add(new Transition(entities.First(x => x.Name == "s2"), entities.First(x => x.Name == "s4"), ActionEnum.Take));
            transitions.Add(new Transition(entities.First(x => x.Name == "s2"), entities.First(x => x.Name == "s3"), ActionEnum.Take));
            transitions.Add(new Transition(entities.First(x => x.Name == "s3"), entities.First(x => x.Name == "s4"), ActionEnum.Grand));
            transitions.Add(new Transition(entities.First(x => x.Name == "s4"), entities.First(x => x.Name == "o1"), ActionEnum.Take));

            //transitions.Add(new Transition(entities.First(x => x.Name == "o1"), entities.First(x => x.Name == "s1"), ActionEnum.Take));

            return transitions;
        }
    }
}
