using Game.Combat.Ability;

namespace Game.Combat.Event
{
    public class ReceiveDamageEvent : CombatEvent
    {
        public Entity Target { get; }
        public Ability.Ability Ability { get; }
        public int Damage { get; }
        public DamageType Type { get; }

        public ReceiveDamageEvent(int depth, Combat combat, Entity target, Ability.Ability ability, int damage, DamageType type) : base(depth, combat)
        {
            Target = target;
            Ability = ability;
            Damage = damage;
            Type = type;
        }
    }
}