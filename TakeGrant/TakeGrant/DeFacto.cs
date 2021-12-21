using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeGrant
{
    public class DeFacto
    {
        private List<Transition> Transitions { get; set; }
        private List<Transition> NewTransitions { get; set; }
        private List<Entity> Entities { get; set; }

        public DeFacto(List<Transition> transitions, List<Entity> entities)
        {
            Transitions = transitions;
            Entities = entities;
            NewTransitions = new List<Transition>();
        }
        public void Search()
        {
            FirstRule();
            SecondRule();
            Spy();
            Find();
            Post();
            Pass();
        }

        public void FirstRule()
        {
            Console.WriteLine("FirstRule");
            var transitionsSubject = Transitions.Where(x => x.FromEntity is Subject && x.InEntity is Object && x.Action == ActionEnum.Read);
            foreach (var transition in transitionsSubject)
            {
                NewTransitions.Add(new Transition(
                        Entities.First(x => x.Name == transition.FromEntity.Name),
                        Entities.First(x => x.Name == transition.InEntity.Name),
                        transition.Action));
                NewTransitions.Add(new Transition(
                    Entities.First(x => x.Name == transition.InEntity.Name),
                    Entities.First(x => x.Name == transition.FromEntity.Name),
                    ActionEnum.Write));
                Console.WriteLine("Added {0} => {1} {2}",
                    Entities.First(x => x.Name == transition.FromEntity.Name),
                    Entities.First(x => x.Name == transition.InEntity.Name),
                    transition.Action);
                Console.WriteLine("Added {0} => {1} {2}",
                    Entities.First(x => x.Name == transition.InEntity.Name),
                    Entities.First(x => x.Name == transition.FromEntity.Name),
                    ActionEnum.Write);

            }
        }
        public void SecondRule()
        {
            Console.WriteLine("SecondRule");
            var transitionsSubject = Transitions.Where(x => x.FromEntity is Subject && x.InEntity is Object && x.Action == ActionEnum.Write);
            foreach (var transition in transitionsSubject)
            {
                NewTransitions.Add(new Transition(
                        Entities.First(x => x.Name == transition.FromEntity.Name),
                        Entities.First(x => x.Name == transition.InEntity.Name),
                        transition.Action));
                NewTransitions.Add(new Transition(
                    Entities.First(x => x.Name == transition.InEntity.Name),
                    Entities.First(x => x.Name == transition.FromEntity.Name),
                    ActionEnum.Read));
                Console.WriteLine("Added {0} => {1} {2}",
                    Entities.First(x => x.Name == transition.FromEntity.Name),
                    Entities.First(x => x.Name == transition.InEntity.Name),
                    transition.Action);
                Console.WriteLine("Added {0} => {1} {2}",
                    Entities.First(x => x.Name == transition.InEntity.Name),
                    Entities.First(x => x.Name == transition.FromEntity.Name),
                    ActionEnum.Read);

            }
        }

        public void Spy()
        {
            Console.WriteLine("Spy");

            var transitionsSubject = Transitions.Where(x => x.FromEntity is Subject && x.InEntity is Subject && x.Action == ActionEnum.Read);

            foreach (var transition in transitionsSubject)
            {
                foreach (var item in transition.InEntity.Transitions.Where(x => x.InEntity is Object && x.FromEntity is Subject && x.Action == ActionEnum.Read))
                {
                    if (transition.InEntity.Name != item.InEntity.Name)
                    {
                        NewTransitions.Add(new Transition(
                            Entities.First(x => x.Name == transition.FromEntity.Name),
                            Entities.First(x => x.Name == item.InEntity.Name),
                            ActionEnum.Read));
                        NewTransitions.Add(new Transition(
                            Entities.First(x => x.Name == item.InEntity.Name),
                            Entities.First(x => x.Name == transition.FromEntity.Name),
                            ActionEnum.Write));
                        Console.WriteLine("Added {0} => {1} {2}",
                            Entities.First(x => x.Name == transition.FromEntity.Name),
                            Entities.First(x => x.Name == item.InEntity.Name),
                            ActionEnum.Read);
                        Console.WriteLine("Added {0} => {1} {2}",
                            Entities.First(x => x.Name == item.InEntity.Name),
                            Entities.First(x => x.Name == transition.FromEntity.Name),
                            ActionEnum.Write);
                    }

                }

            }
        }

        public void Find()
        {
            Console.WriteLine("Find");

            var transitionsSubject = Transitions.Where(x => x.FromEntity is Subject && x.InEntity is Subject && x.Action == ActionEnum.Write);

            foreach (var transition in transitionsSubject)
            {
                foreach (var item in transition.InEntity.Transitions.Where(x => x.InEntity is Object && x.FromEntity is Subject && x.Action== ActionEnum.Write))
                {
                    if (transition.InEntity.Name != item.InEntity.Name)
                    {
                        NewTransitions.Add(new Transition(
                            Entities.First(x => x.Name == transition.FromEntity.Name),
                            Entities.First(x => x.Name == item.InEntity.Name),
                            ActionEnum.Write));
                        NewTransitions.Add(new Transition(
                            Entities.First(x => x.Name == item.InEntity.Name),
                            Entities.First(x => x.Name == transition.FromEntity.Name),
                            ActionEnum.Read));
                        Console.WriteLine("Added {0} => {1} {2}",
                            Entities.First(x => x.Name == transition.FromEntity.Name),
                            Entities.First(x => x.Name == item.InEntity.Name),
                            ActionEnum.Write);
                        Console.WriteLine("Added {0} => {1} {2}",
                            Entities.First(x => x.Name == item.InEntity.Name),
                            Entities.First(x => x.Name == transition.FromEntity.Name),
                            ActionEnum.Read);
                    }

                }

            }
        }

        public void Post()
        {
            Console.WriteLine("Post");

            var transitionsSubject = Transitions.Where(x => x.FromEntity is Subject && x.InEntity is Subject && x.Action == ActionEnum.Read);

            foreach (var transition in transitionsSubject)
            {
                foreach (var item in transition.InEntity.Transitions.Where(x => x.InEntity is Subject && x.FromEntity is Object && x.Action == ActionEnum.Write))
                {
                    if (transition.InEntity.Name != item.FromEntity.Name)
                    {
                        NewTransitions.Add(new Transition(
                            Entities.First(x => x.Name == transition.FromEntity.Name),
                            Entities.First(x => x.Name == item.FromEntity.Name),
                            ActionEnum.Read));
                        NewTransitions.Add(new Transition(
                            Entities.First(x => x.Name == item.FromEntity.Name),
                            Entities.First(x => x.Name == transition.FromEntity.Name),
                            ActionEnum.Write));
                        Console.WriteLine("Added {0} => {1} {2}",
                            Entities.First(x => x.Name == transition.FromEntity.Name),
                            Entities.First(x => x.Name == item.FromEntity.Name),
                            ActionEnum.Read);
                        Console.WriteLine("Added {0} => {1} {2}",
                             Entities.First(x => x.Name == item.FromEntity.Name),
                            Entities.First(x => x.Name == transition.FromEntity.Name),
                            ActionEnum.Write);
                    }

                }

            }
        }

        public void Pass()
        {
            Console.WriteLine("Pass");

            var transitionsSubject = Transitions.Where(x => x.FromEntity is Subject && x.InEntity is Object && x.Action == ActionEnum.Write);

            foreach (var transition in transitionsSubject)
            {
                foreach (var item in transition.FromEntity.Transitions.Where(x => x.InEntity is Object && x.FromEntity is Subject && x.Action == ActionEnum.Read))
                {
                    if (transition.InEntity.Name != item.InEntity.Name)
                    {
                        NewTransitions.Add(new Transition(
                            Entities.First(x => x.Name == transition.InEntity.Name),
                            Entities.First(x => x.Name == item.InEntity.Name),
                            ActionEnum.Read));
                        NewTransitions.Add(new Transition(
                            Entities.First(x => x.Name == item.InEntity.Name),
                            Entities.First(x => x.Name == transition.InEntity.Name),
                            ActionEnum.Write));
                        Console.WriteLine("Added {0} => {1} {2}",
                            Entities.First(x => x.Name == transition.InEntity.Name),
                            Entities.First(x => x.Name == item.InEntity.Name),
                            ActionEnum.Read);
                        Console.WriteLine("Added {0} => {1} {2}",
                            Entities.First(x => x.Name == item.InEntity.Name),
                            Entities.First(x => x.Name == transition.InEntity.Name),
                            ActionEnum.Write);
                    }

                }

            }
        }
    }
}
