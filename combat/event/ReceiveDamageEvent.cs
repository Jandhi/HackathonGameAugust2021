using Game.Combat.Action;

namespace Game.Combat.Event
{
    public class ReceiveDamageEvent : CombatEvent
    {
        public Entity Caster { get; }
        public Entity Receiver { get; }
        public Ability.Ability Ability { get; }
        public float Damage { get; set; }
        public DamageType Type { get; }
        public bool IsGoingThrough { get; set; } = true;
        public ReceiveDamageEvent(int depth, Combat combat, AbilityResult root, Entity caster, Entity receiver, Ability.Ability ability, float damage, DamageType type) : base(depth, combat, root)
        {
            Caster = caster;
            Receiver = receiver;
            Ability = ability;
            Damage = damage;
            Type = type;
        }

        public ReceiveDamageEvent(SendDamageEvent ev, Entity receiver) : this(ev.Depth, ev.Combat, ev.Root, ev.Caster, receiver, ev.Ability, ev.Damage, ev.Type)
        {}
    }
}