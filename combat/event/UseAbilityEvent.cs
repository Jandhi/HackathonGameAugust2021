using Game.Combat.Action;

namespace Game.Combat.Event
{
    public class UseAbilityEvent : CombatEvent
    {
        public Entity Caster { get; }
        public Ability.Ability Ability { get; }
        public bool IsGoingThrough { get; set; } = true;

        public UseAbilityEvent(int depth, Combat combat, AbilityResult root, Entity caster, Ability.Ability ability) : base(depth, combat, root)
        {
            Caster = caster;
            Ability = ability;
        }
    }
}