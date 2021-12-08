using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeGrant
{
    public class Transition
    {
        public Entity FromEntity { get; set; }

        public Entity InEntity { get; set; }

        public ActionEnum Action { get; set; }

        public Transition(Entity fromEntity, Entity inEntity, ActionEnum action)
        {
            FromEntity = fromEntity;
            InEntity = inEntity;
            Action = action;
        }
    }


    public class Entity
    {
        public string Name { get; set; }

        public List<Transition> Transitions { get; set; }

        public Entity()
        {
            Transitions = new List<Transition>();
        }
        public void AddTran(IEnumerable<Transition> transition)
        {
            Transitions.AddRange(transition);
        }
    }

    public class Object : Entity
    {
    }

    public class Subject : Entity
    {
    }

    public enum ActionEnum
    {
        Take = 1,
        Grand
    }
}
