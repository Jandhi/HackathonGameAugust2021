using Game.Combat.Event;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Game.Combat
{
    public abstract class Passive : ICombatEventListener
    {
        public Entity Parent { get; }

        public Passive(Entity parent)
        {   
            Parent = parent;
        }

        public abstract void Receive(CombatEvent ev);
    }

    public class Passive<T> : Passive where T : CombatEvent
    {
        public List<Func<T, bool>> Filters { get; } = new List<Func<T, bool>>();
        public List<Action<T>> Modifiers { get; } = new List<Action<T>>();
        public Passive(Entity parent) : base(parent)
        {
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
                ev.MakeModification(this, () => {
                    foreach(var modifier in Modifiers)
                    {
                        modifier(receivedEvent);
                    }
                });
            }
        }
    }    
}