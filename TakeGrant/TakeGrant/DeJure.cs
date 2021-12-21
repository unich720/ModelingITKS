using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeGrant
{
    public class DeJure
    {
        private List<Transition> Transitions { get; set; }
        private List<Transition> NewTransitions { get; set; }
        private List<Entity> Entities { get; set; }

        public DeJure(List<Transition> transitions, List<Entity> entities)
        {
            Transitions = transitions;
            Entities = entities;
            NewTransitions = new List<Transition>(transitions);
        }


        public void Search()
        {
            Take();
            Grant();
        }

        public void Take()
        {
            var transitionsSubject = Transitions.Where(x => x.FromEntity is Subject && x.InEntity is Object && x.Action==ActionEnum.Take);
            Console.WriteLine("Take");
            foreach (var transition in transitionsSubject)
            {
                foreach (var item in transition.InEntity.Transitions.Where(x => x.InEntity is Object && x.FromEntity is Object))
                {
                    if (transition.InEntity.Name != item.InEntity.Name)
                    {
                        Console.WriteLine("{0} => {1}, new {0} => {2}, {3}", transition.FromEntity.Name, transition.InEntity.Name, item.InEntity.Name, item.Action);
                        NewTransitions.Add(new Transition(Entities.First(x => x.Name == transition.FromEntity.Name), Entities.First(x => x.Name == item.InEntity.Name), item.Action));
                    }

                }

            }
        }

        public void Grant()
        {
            var transitionsSubject = Transitions.Where(x => x.FromEntity is Subject && x.InEntity is Object && x.Action == ActionEnum.Grand);

            Console.WriteLine("Grand");
            foreach (var transition in transitionsSubject)
            {
                var sad = transition.FromEntity.Transitions.Where(x=>x.InEntity is Object);
                foreach (var item in sad)
                {
                    if (transition.InEntity.Name != item.InEntity.Name)
                    {
                        Console.WriteLine("{0} => {1}, new {1} => {2}, {3}", transition.FromEntity.Name, transition.InEntity.Name, item.InEntity.Name, item.Action);
                        NewTransitions.Add(new Transition(Entities.First(x => x.Name == transition.InEntity.Name), Entities.First(x => x.Name == item.InEntity.Name), item.Action));
                    }
                }
            }
        }
    }
}
