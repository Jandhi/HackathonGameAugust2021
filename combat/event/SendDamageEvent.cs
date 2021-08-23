using Game.Combat.Ability;
using System.Collections.Generic;

namespace Game.Combat.Event
{
    public class SendDamageEvent : CombatEvent
    {
        public Entity Caster { get; }
        public List<Entity> Targets { get; }
        public Ability.Ability Ability { get; }
        public float Damage { get; set; }
        public DamageType Type { get; }
        public bool IsGoingThrough { get; set; } = true;

        public SendDamageEvent(int depth, Combat combat, Entity caster, List<Entity> targets, Ability.Ability ability, float damage, DamageType type) : base(depth, combat)
        {
            Caster = caster;
            Targets = targets;
            Ability = ability;
            Damage = damage;
            Type = type;
        }
    }
}