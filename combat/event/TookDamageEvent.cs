using Game.Combat.Ability;

namespace Game.Combat.Event
{
    public class TookDamageEvent : CombatEvent
    {
        public Entity Caster { get; }
        public Entity Receiver { get; }
        public Ability.Ability Ability { get; }
        public float Damage { get; set; }
        public DamageType Type { get; }

        public TookDamageEvent(int depth, Combat combat, Entity caster, Entity receiver, Ability.Ability ability, int damage, DamageType type) : base(depth, combat)
        {
            Caster = caster;
            Receiver = receiver;
            Ability = ability;
            Damage = damage;
            Type = type;
        }
    }
}