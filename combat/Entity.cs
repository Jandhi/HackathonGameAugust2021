using System.Collections.Generic;
using Game.Combat.Event;
using Game.UI;
using Game.Combat.Action;

namespace Game.Combat {
    public class Entity : ICombatEventListener, INamed {

        public ColoredString Name { get; }
        public virtual string Look { get; }
        public StatBlock Stats { get; }
        public List<ICombatEventListener> Passives { get; } = new List<ICombatEventListener>();
        public List<Tag> Tags { get; }
        public bool IsDead => Stats[Stat.Health] <= 0;
        public List<Ability.Ability> Abilities { get; } = new List<Ability.Ability>();

        public Entity(ColoredString name, StatBlock stats, List<Tag> tags = null){
            Name = name;
            Stats = stats ?? new StatBlock();
            Stats.Parent = this;
            Tags = tags ?? new List<Tag>();
        }

        public void Receive(CombatEvent ev) 
        {
            ev.Broadcast(Passives);
        }

        public TookDamageEvent ReceiveDamage(SendDamageEvent ev)
        {
            var receiveDamage = new ReceiveDamageEvent(ev, this);
            receiveDamage.Broadcast();

            if(receiveDamage.IsGoingThrough)
            {
                var health = Stats[Stat.Health];
                ev.Root.ActionQueue.Enqueue(new StatChange(this, Stat.Health, health, health - receiveDamage.Damage));
                return new TookDamageEvent(receiveDamage);
            }
            else
            {
                return null;
            }
        }
    }
}
