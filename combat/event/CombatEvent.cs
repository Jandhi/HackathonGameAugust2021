using System.Collections.Generic;

namespace Game.Combat.Event
{
    public class CombatEvent
    {
        public int Depth { get; }
        public Combat Combat { get; }
        
        public HashSet<ICombatEventListener> Visited { get; } = new HashSet<ICombatEventListener>(); // List of listeners it already visited

        public CombatEvent(int depth, Combat combat)
        {
            Depth = depth;
            Combat = combat;
        }

        public void Broadcast(IEnumerable<ICombatEventListener> listeners)
        {
            foreach(var listener in listeners)
            {
                if(!Visited.Contains(listener))
                {
                    listener.Receive(this);
                }
            }
        }
    }
}