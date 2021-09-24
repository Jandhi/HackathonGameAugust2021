using Game.Combat.Action;

namespace Game.Combat.Event
{
    public class ReceiveDeathEvent : CombatEvent
    {
        public Entity Receiver { get; }
        public Entity Killer { get; }
        public bool IsGoingThrough { get; set; } = true;

        public ReceiveDeathEvent(int depth, Combat combat, ActionRoot root, Entity receiver, Entity killer) : base(depth, combat, root)
        {
            Receiver = receiver;
            Killer = killer;
        }
    }
}