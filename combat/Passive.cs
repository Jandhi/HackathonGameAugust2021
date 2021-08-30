using Game.Combat.Event;
using System.Collections.Generic;
using System;
using System.Linq;
using Game.UI;

namespace Game.Combat
{
    public class Passive : Passive<CombatEvent>
    {
        public Passive(ColoredString name, Entity parent) : base(name, parent)
        {
        }
    }

    public class Passive<T> : PassiveBase where T : CombatEvent
    {
        public List<Func<T, bool>> Filters { get; } = new List<Func<T, bool>>();
        public List<Action<T>> Modifiers { get; } = new List<Action<T>>();
        public Passive(ColoredString name, Entity parent) : base(name, parent)
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
                ev.Root.ActivatedPassives.Add(this);
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
        public ColoredString Name { get; }
        public Entity Parent { get; set; }

        public PassiveBase(ColoredString name, Entity parent)
        {   
            Name = name;
            Parent = parent;
            Parent.Passives.Add(this);
        }

        public abstract void Receive(CombatEvent ev);
    }
}