using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TakeGrant
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            List<Entity> entities = new List<Entity>();
            List<string> list = new() { "s1", "s2", "s3", "s4", "s5", "s6", "o1", "o2", "o3", "o4", "o5" };
            foreach (var item in list)
            {
                if (item.Contains("s"))
                {
                    entities.Add(new Subject { Name = item });
                }
                else
                {
                    entities.Add(new Object { Name = item });
                }
            }

            //значения для мостов и дю-ре
            //var transitions = Testing(entities);

            //значения для дефакто
            var transitions = TestingRW(entities);

            //ксв если нужно
            //var csv = CSV();
            //entities = csv.entities;
            //transitions = csv.transitions;

            foreach (var item in entities)
            {
                //var ads = transitions.Where(x => x.FromEntity.Name == item.Name || x.InEntity.Name == item.Name);
                item.AddTran(transitions.Where(x => x.FromEntity.Name == item.Name || x.InEntity.Name == item.Name));
            }

            BridgeSearch bridgeSearch = new BridgeSearch();
            bridgeSearch.Search(transitions, entities);

            DeJure deJure = new DeJure(transitions, entities);
            deJure.Search();

            DeFacto deFacto = new DeFacto(transitions, entities);
            deFacto.Search();

            Console.ReadKey();
        }

        public static List<Entity> GetEntities(List<string> list)
        {
            List<Entity> entities = new List<Entity>();
            foreach (var item in list)
            {
                if (item.Contains("s"))
                {
                    entities.Add(new Subject { Name = item });
                }
                else
                {
                    entities.Add(new Object { Name = item });
                }
            }
            return entities;
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

            //case1
            transitions.Add(new Transition(entities.First(x => x.Name == "o1"), entities.First(x => x.Name == "s1"), ActionEnum.Take));

            //grant
            transitions.Add(new Transition(entities.First(x => x.Name == "s4"), entities.First(x => x.Name == "o3"), ActionEnum.Grand));

            return transitions;
        }
        public static List<Transition> TestingRW(List<Entity> entities)
        {
            List<Transition> transitions = new List<Transition>();

            transitions.Add(new Transition(entities.First(x => x.Name == "s5"), entities.First(x => x.Name == "s6"), ActionEnum.Read));
            transitions.Add(new Transition(entities.First(x => x.Name == "s6"), entities.First(x => x.Name == "o3"), ActionEnum.Read));
            transitions.Add(new Transition(entities.First(x => x.Name == "o3"), entities.First(x => x.Name == "o2"), ActionEnum.Write));
            transitions.Add(new Transition(entities.First(x => x.Name == "o5"), entities.First(x => x.Name == "o1"), ActionEnum.Write));
            transitions.Add(new Transition(entities.First(x => x.Name == "o1"), entities.First(x => x.Name == "o2"), ActionEnum.Read));
            transitions.Add(new Transition(entities.First(x => x.Name == "s1"), entities.First(x => x.Name == "o4"), ActionEnum.Read));
            transitions.Add(new Transition(entities.First(x => x.Name == "s1"), entities.First(x => x.Name == "s2"), ActionEnum.Write));
            transitions.Add(new Transition(entities.First(x => x.Name == "s1"), entities.First(x => x.Name == "s3"), ActionEnum.Write));
            transitions.Add(new Transition(entities.First(x => x.Name == "s2"), entities.First(x => x.Name == "o4"), ActionEnum.Read));
            transitions.Add(new Transition(entities.First(x => x.Name == "s2"), entities.First(x => x.Name == "s4"), ActionEnum.Read));
            transitions.Add(new Transition(entities.First(x => x.Name == "s2"), entities.First(x => x.Name == "s3"), ActionEnum.Read));
            transitions.Add(new Transition(entities.First(x => x.Name == "s3"), entities.First(x => x.Name == "s4"), ActionEnum.Write));
            transitions.Add(new Transition(entities.First(x => x.Name == "s4"), entities.First(x => x.Name == "o1"), ActionEnum.Read));

            //case1
            //transitions.Add(new Transition(entities.First(x => x.Name == "o1"), entities.First(x => x.Name == "s1"), ActionEnum.Read));

            //grant
            //transitions.Add(new Transition(entities.First(x => x.Name == "s4"), entities.First(x => x.Name == "o3"), ActionEnum.Write));

            //post
            //transitions.Add(new Transition(entities.First(x => x.Name == "o4"), entities.First(x => x.Name == "s6"), ActionEnum.Write));

            return transitions;
        }

        public static (List<Entity> entities, List<Transition> transitions) CSV()
        {
            List<string> list = null;
            List<Entity> entities = null;
            List<Transition> transitions = new List<Transition>();

            using (var reader = new StreamReader(Directory.GetCurrentDirectory() + @"\takeGrant.csv"))
            {
                int c = 1;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    if (c == 1)
                    {
                        list = new(values);
                        entities = GetEntities(list);

                    }
                    else
                        transitions.Add(
                            new Transition(
                                entities.First(x => x.Name == values[0]),
                                entities.First(x => x.Name == values[1]),
                                values[2] == "T" ? ActionEnum.Take : ActionEnum.Grand));

                    c++;
                }
            }
            return (entities, transitions);
        }
    }
}
