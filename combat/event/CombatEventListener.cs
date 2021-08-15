namespace Game.Combat.Event
{
    public interface ICombatEventListener
    {
        void Receive(CombatEvent ev);
    }
}