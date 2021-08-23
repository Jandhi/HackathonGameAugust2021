namespace Game.Combat.Event
{
    public class DeathEvent
    {
        public Entity Victim { get; }
        public Entity Killer { get; }

        public DeathEvent(Entity receiver, Entity killer)
        {
            Victim = receiver;
            Killer = killer;
        }
    }
}