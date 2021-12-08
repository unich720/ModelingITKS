using System;
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


                if (transition.InEntity is Subject)
                {

                }
                else
                {

                }
            }
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
    }
}
