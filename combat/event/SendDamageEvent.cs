using Game.Combat.Action;
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

        public SendDamageEvent(int depth, Combat combat, ActionRoot root, Entity caster, List<Entity> targets, float damage, DamageType type) : base(depth, combat, root)
        {
            Caster = caster;
            Targets = targets;
            Ability = root.Ability;
            Damage = damage;
            Type = type;
        }
    }
}