using System.Collections.Generic;
using Game.Combat.Ability;

namespace Game.Combat.Action
{
    public class AbilityResult : CompositeCombatAction
    {
        public Ability.Ability RootAbility { get; }
        public Entity RootEntity { get; }
        public List<string> PassiveMessages { get; } = new List<string>();
        public HashSet<Passive> ActivatedPassives { get; } = new HashSet<Passive>();
        public Queue<ICombatAction> ActionQueue { get; } = new Queue<ICombatAction>(); 

        public AbilityResult(Ability.Ability rootAbility, Entity rootEntity)
        {
            RootAbility = rootAbility;
            RootEntity = rootEntity;
        }
    }
}