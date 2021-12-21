using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeGrant
{
    public class BridgeSearch
    {
        public void Search(List<Transition> transitions, List<Entity> entities)
        {
            var transitionsSubject = transitions.Where(x => x.FromEntity is Subject && x.Action == ActionEnum.Take);

            foreach (var transition in transitionsSubject)
            {
                Entity entity;
                if (transition.InEntity is Subject && transition.FromEntity is Subject)
                {
                    continue;
                }

                var bridge = Case1(transition);
                if (bridge.Item1)
                {
                    Console.WriteLine("Мост t→* : " + bridge.Item2);
                }
                if (transition.FromEntity.Name=="s4")
                {
                    bridge = Case2(transition);
                    if (bridge.Item1)
                    {
                        ShowBridge(transition.FromEntity.Name);
                    }
                }
                

                if (transition.InEntity is Subject)
                {

                }
                else
                {

                }
            }
        }
        public string bridge;

        public void ShowBridge(string s)
        {
            var dd = resultS.Values.Where(x => x.Contains("o")).ToList();
            dd.AddRange(resultS.Keys.Where(x => x.Contains("o")));
            dd = dd.Distinct().ToList();
            Console.WriteLine("Вершины моста");
            foreach (var item in dd)
            {
                Console.Write(item);
            }
            Console.WriteLine();
            dd = resultS.Values.Where(x => x.Contains("s")).ToList();
            dd.AddRange(resultS.Keys.Where(x => x.Contains("s")));
            dd = dd.Distinct().ToList();
            foreach (var item in dd)
            {
                Console.Write(item);
            }
            Console.WriteLine();
        }

        public (bool, string) Case1(Transition transition)
        {
            string s = "";
            if (transition == default)
            {
                return (false, s);
            }
            if (transition.InEntity is Subject && transition.Action == ActionEnum.Take)
            {
                return (true, transition.InEntity.Name.ToString());
            }
            if (transition.Action == ActionEnum.Take)
            {
                foreach (var item in transition.InEntity.Transitions.Where(x => x.FromEntity == transition.InEntity))
                {
                    s = transition.FromEntity.Name + transition.InEntity.Name;
                    var result = Case1(item);
                    if (result.Item1)
                    {
                        s += result.Item2;
                        return (true, s);
                    }

                }
                //s += Case1(transition.InEntity.Transitions.FirstOrDefault(x=>x.FromEntity==transition.InEntity));
            }
            return (false, s);
        }
        public ConcurrentDictionary<string,string> resultS = new();

        public (bool, string) Case2(Transition transition)
        {
            string s = "";
            if (transition == default)
            {
                return (false, s);
            }
            if (transition.InEntity is Subject && transition.Action == ActionEnum.Take)
            {
                return (true, transition.InEntity.Name.ToString());
            }
            if (transition.Action == ActionEnum.Grand)
            {
                foreach (var item in transition.FromEntity.Transitions.Where(x => x.InEntity == transition.FromEntity && x.Action==ActionEnum.Take))
                {
                    s = transition.FromEntity.Name + transition.InEntity.Name;
                    var result = Case2_2(item);
                    if (result.Item1)
                    {
                        resultS.TryAdd(transition.FromEntity.Name, transition.InEntity.Name);
                        s += result.Item2;
                        return (true, s);
                    }

                }
                //s += Case1(transition.InEntity.Transitions.FirstOrDefault(x=>x.FromEntity==transition.InEntity));
            }
            if (transition.Action == ActionEnum.Take)
            {
                var rr = transition.InEntity.Transitions.Where(x => x.FromEntity == transition.InEntity);
                if (rr.Any())
                {
                    foreach (var item in rr)
                    {
                        s = transition.FromEntity.Name + transition.InEntity.Name;
                        var result = Case2(item);
                        if (result.Item1)
                        {
                            resultS.TryAdd(transition.FromEntity.Name, transition.InEntity.Name);
                            s += result.Item2;
                            return (true, s);
                        }

                    }
                }

                if (!rr.Any())
                {
                    rr = transition.InEntity.Transitions.Where(x => x.InEntity == transition.InEntity && x.Action == ActionEnum.Grand);
                    foreach (var item in rr)
                    {
                        s = transition.FromEntity.Name + transition.InEntity.Name;
                        var result = Case2(item);
                        if (result.Item1)
                        {
                            resultS.TryAdd(transition.FromEntity.Name, transition.InEntity.Name);
                            s += result.Item2;
                            return (true, s);
                        }

                    }
                }

                //s += Case1(transition.InEntity.Transitions.FirstOrDefault(x=>x.FromEntity==transition.InEntity));
            }
            
            return (false, s);
        }

        public (bool, string) Case2_2(Transition transition)
        {
            string s = "";
            if (transition == default)
            {
                return (false, s);
            }
            if (transition.InEntity is Subject && transition.Action == ActionEnum.Take)
            {
                resultS.TryAdd(transition.FromEntity.Name, transition.InEntity.Name);
                return (true, transition.InEntity.Name.ToString());
            }
            if (transition.Action == ActionEnum.Take)
            {
                var rr = transition.FromEntity.Transitions.Where(x => x.InEntity == transition.FromEntity && x.Action == ActionEnum.Take);
                foreach (var item in rr)
                {
                    s = transition.FromEntity.Name + transition.InEntity.Name;
                    var result = Case2_2(item);
                    if (result.Item1)
                    {
                        resultS.TryAdd(transition.FromEntity.Name, transition.InEntity.Name);
                        s += result.Item2;
                        return (true, s);
                    }

                }
                if (!rr.Any())
                {
                    resultS.TryAdd(transition.FromEntity.Name, transition.InEntity.Name);
                }
                //s += Case1(transition.InEntity.Transitions.FirstOrDefault(x=>x.FromEntity==transition.InEntity));
            }
            return (false, s);
        }
    }
}
