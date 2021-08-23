using System.Collections.Generic;
using Game.Combat.Event;

namespace Game.Combat {
    public class Entity : ICombatEventListener {

        public string Name { get; }
        public virtual string Look { get; }
        public Dictionary<Stat, float> Stats { get; } = new Dictionary<Stat, float>();
        public List<ICombatEventListener> Passives { get; } = new List<ICombatEventListener>();

        public Entity(string name, Dictionary<Stat, float> stats){
            Name = name;
            Stats = stats;
        }

        public void Receive(CombatEvent ev) 
        {
            ev.Broadcast(Passives);
        }
    }
}
