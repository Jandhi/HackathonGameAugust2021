using Game.Combat.Event;
using System.Collections.Generic;
using System;
using System.Linq;
using Game.UI;

namespace Game.Combat
{
    public class Passive : Passive<CombatEvent>
    {
        public Passive(string name, Entity parent, Func<CombatEvent, string> messageCreator) : base(name, parent, messageCreator)
        {
        }
    }

    public class Passive<T> : PassiveBase where T : CombatEvent
    {
        public List<Func<T, bool>> Filters { get; } = new List<Func<T, bool>>();
        public List<Action<T>> Modifiers { get; } = new List<Action<T>>();
        public Func<T, string> MessageCreator { get; }
        public Passive(string name, Entity parent, Func<T, string> messageCreator) : base(name, parent)
        {
            MessageCreator = messageCreator;
        }

        public override void Receive(CombatEvent ev)
        {
            if( !(ev is T) )
            {
                return; // wrong event type
            }

            var receivedEvent = ev as T;

            if(Filters.All(filter => filter(receivedEvent)))
            {
                ev.Root.PassiveMessages.Add($"{Name.ToString()}: {MessageCreator(receivedEvent)}");
                ev.MakeModification(this, () => {
                    foreach(var modifier in Modifiers)
                    {
                        modifier(receivedEvent);
                    }
                });
            }
        }
    }    

    public abstract class PassiveBase : ICombatEventListener, INamed
    {
        public string Name { get; }
        public Entity Parent { get; set; }

        public PassiveBase(string name, Entity parent)
        {   
            Name = name;
            Parent = parent;
            Parent.Passives.Add(this);
        }

        public abstract void Receive(CombatEvent ev);
    }
}