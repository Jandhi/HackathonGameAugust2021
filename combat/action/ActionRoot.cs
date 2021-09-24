using System.Collections.Generic;
using Game.Combat.Ability;

namespace Game.Combat.Action
{
    public class ActionRoot : CompositeCombatAction
    {
        public Ability.Ability Ability { get; }
        public Entity Entity { get; }
        public List<string> PassiveMessages { get; } = new List<string>();
        public HashSet<Passive> ActivatedPassives { get; } = new HashSet<Passive>();
        public Queue<ICombatAction> ActionQueue { get; } = new Queue<ICombatAction>(); 
        public override List<ICombatAction> Actions => new List<ICombatAction>(ActionQueue);

        public ActionRoot(Ability.Ability rootAbility, Entity rootEntity)
        {
            Ability = rootAbility;
            Entity = rootEntity;
        }
    }
}