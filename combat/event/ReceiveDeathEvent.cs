namespace Game.Combat.Event
{
    public class ReceiveDeathEvent
    {
        public Entity Receiver { get; }
        public Entity Killer { get; }
        public bool IsGoingThrough { get; set; } = true;

        public ReceiveDeathEvent(Entity receiver, Entity killer)
        {
            Receiver = receiver;
            Killer = killer;
        }
    }
}