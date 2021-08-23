using System.Collections.Generic;

namespace Game.Combat.Event
{
    public class CombatEvent
    {
        public static readonly int DEPTH_LIMIT = 100;

        public int Depth { get; }
        public Combat Combat { get; }
        
        public HashSet<ICombatEventListener> Visited { get; } = new HashSet<ICombatEventListener>(); // List of listeners it already visited
        public Queue<ICombatEventListener> BroadcastQueue { get; } = new Queue<ICombatEventListener>(); // Queue of upcoming broadcasts
        public HashSet<ICombatEventListener> BroadcastSet { get; } = new HashSet<ICombatEventListener>(); // Set of listeners to be broadcasted to, to avoid duplicate broadcasting
        public CombatEvent(int depth, Combat combat)
        {
            Depth = depth;
            Combat = combat;
        }

        public void Broadcast()
        {
            Broadcast(Combat.Combatants);
        }

        public void Broadcast(IEnumerable<ICombatEventListener> listeners)
        {
            foreach(var listener in listeners)
            {
                if(!BroadcastSet.Contains(listener))
                {
                    BroadcastSet.Add(listener);
                    BroadcastQueue.Enqueue(listener);
                }
            }

            while(BroadcastQueue.Count > 0 && Depth < DEPTH_LIMIT)
            {
                var listener = BroadcastQueue.Dequeue();
                BroadcastSet.Remove(listener);

                if(!Visited.Contains(listener))
                {
                    listener.Receive(this);
                }
            }
        }

        public void MakeModification(ICombatEventListener listener, System.Action modification)
        {
            modification();
            Visited.Add(listener);
            Broadcast();
        }
    }
}