using System.Collections.Generic;
using Game.Combat.Event;

namespace Game.Combat {
    public class Entity : ICombatEventListener {

        public string Name { get; }
        public Dictionary<Stats, int> Stats { get; } = new Dictionary<Stats, int>();

        public Entity(string name, Dictionary<Stats, int> stats){
            Name = name;
            Stats = stats;
        }

        public void Receive(CombatEvent ev) 
        {
            // do something with the event
        }
    }
}
