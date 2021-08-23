using Game.Combat.Ability;

namespace Game.Combat.Event
{
    public class UseAbilityEvent : CombatEvent
    {
        public Entity Caster { get; }
        public Ability.Ability Ability { get; }
        public bool IsGoingThrough { get; set; } = true;

        public UseAbilityEvent(int depth, Combat combat, Entity caster, Ability.Ability ability) : base(depth, combat)
        {
            Caster = caster;
            Ability = ability;
        }
    }
}