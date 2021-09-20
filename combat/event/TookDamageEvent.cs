using Game.Combat.Action;

namespace Game.Combat.Event
{
    public class TookDamageEvent : CombatEvent
    {
        public Entity Caster { get; }
        public Entity Receiver { get; }
        public Ability.Ability Ability { get; }
        public float Damage { get; set; }
        public DamageType Type { get; }

        public TookDamageEvent(int depth, Combat combat, AbilityResult root, Entity caster, Entity receiver, Ability.Ability ability, int damage, DamageType type) : base(depth, combat, root)
        {
            Caster = caster;
            Receiver = receiver;
            Ability = ability;
            Damage = damage;
            Type = type;
        }

        public TookDamageEvent(ReceiveDamageEvent ev) : base(ev.Depth, ev.Combat, ev.Root)
        {
            Caster = ev.Caster;
            Receiver = ev.Receiver;
            Ability = ev.Ability;
            Damage = ev.Damage;
            Type = ev.Type;
        }
    }
}