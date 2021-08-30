using System.Collections.Generic;
using Game.Combat.Event;
using Game.UI;
using System;
using System.Linq;

namespace Game.Combat {
    public class Entity : ICombatEventListener {

        public ColoredString Name { get; }
        public virtual string Look { get; }
        public Dictionary<Stat, float> Stats { get; } = new Dictionary<Stat, float>();
        public List<ICombatEventListener> Passives { get; } = new List<ICombatEventListener>();
        public List<Tag> Tags { get; }

        public Entity(ColoredString name, Dictionary<Stat, float> stats, List<Tag> tags = null){
            Name = name;
            Stats = stats ?? new Dictionary<Stat, float>();
            Tags = tags ?? new List<Tag>();

            FilloutStats();
        }

        public void Receive(CombatEvent ev) 
        {
            ev.Broadcast(Passives);
        }

        public void FilloutStats()
        {
            foreach(var stat in Enum.GetValues(typeof(Stat)).Cast<Stat>())
            {
                if(!Stats.ContainsKey(stat))
                {
                    Stats[stat] = 0;
                }
            }  
        }
    }
}
