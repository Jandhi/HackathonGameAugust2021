namespace Game.Combat.Action
{
    public interface ICombatAction
    {
        void Do();
        void Undo();
    }
}