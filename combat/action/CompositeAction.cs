using System.Collections.Generic;

namespace Game.Combat.Action
{
    public class CompositeCombatAction : ICombatAction
    {

        public List<ICombatAction> Actions { get; }

        public CompositeCombatAction(List<ICombatAction> actions = null)
        {
            Actions = actions ?? new List<ICombatAction>();
        }

        public void Do(bool isDisplaying) 
        {
            foreach(var action in Actions)
            {
                action.Do(isDisplaying);
            }
        }
        
        public void Undo(bool isDisplaying)
        {
            var reversedActions = new List<ICombatAction>(Actions);
            reversedActions.Reverse();

            foreach(var action in reversedActions) 
            {
                action.Undo(isDisplaying);
            }
        }
    }
}