namespace Game.Combat.Event
{
    public class UseAbilityEvent : CombatEvent
    {
        public Entity Caster { get; }

        public UseAbilityEvent(int depth, Combat combat, Entity caster) : base(depth, combat)
        {
            Caster = caster;
            
        }
    }
}