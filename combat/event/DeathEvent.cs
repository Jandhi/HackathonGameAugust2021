using Game.Combat.Action;

namespace Game.Combat.Event
{
    public class DeathEvent : CombatEvent
    {
        public Entity Victim { get; }
        public Entity Killer { get; }

        public DeathEvent(int depth, Combat combat, AbilityResult root, Entity receiver, Entity killer) : base(depth, combat, root)
        {
            Victim = receiver;
            Killer = killer;
        }
    }
}