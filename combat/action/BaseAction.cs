namespace Game.Combat.Action
{
    public abstract class BaseAction : ICombatAction
    {
        public abstract bool IsDisplayOnly { get; }
        public abstract void Do();
        public abstract void Undo();

        public void Do(bool isDisplaying)
        {
            if(!IsDisplayOnly || isDisplaying) 
            {
                Do();
            }
        }

        public void Undo(bool isDisplaying)
        {
            if(!IsDisplayOnly || isDisplaying)
            {
                Undo();
            }
        }
    }
}