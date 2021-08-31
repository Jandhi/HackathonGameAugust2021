namespace Game.Combat.Action
{
    public interface ICombatAction
    {
        void Do(bool isDisplaying);
        void Undo(bool isDisplaying);
    }
}