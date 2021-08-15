using System.Collections.Generic;
using Game.Combat.Event;

namespace Game.Combat {
    public class Entity : ICombatEventListener {

        public string Name { get; }
        public Dictionary<Stat, int> Stats { get; } = new Dictionary<Stat, int>();

        public Entity(string name, Dictionary<Stat, int> stats){
            Name = name;
            Stats = stats;
        }

        public void Receive(CombatEvent ev) 
        {
            // do something with the event
        }
    }
}
