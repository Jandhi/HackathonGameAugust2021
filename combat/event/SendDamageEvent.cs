using Game.Combat.Ability;

namespace Game.Combat.Event
{
    public class SendDamageEvent : CombatEvent
    {
        public Entity Caster { get; }
        public Ability.Ability Ability { get; }
        public int Damage { get; }
        public DamageType Type { get; }

        public SendDamageEvent(int depth, Combat combat, Entity caster, Ability.Ability ability, int damage, DamageType type) : base(depth, combat)
        {
            Caster = caster;
            Ability = ability;
            Damage = damage;
            Type = type;
        }
    }
}