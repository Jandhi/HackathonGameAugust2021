using System.Collections.Generic;

namespace Game.Combat.Action
{
    public class AbilityResult : CompositeCombatAction
    {
        public Entity RootEntity { get; }
        public List<PassiveBase> ActivatedPassives { get; } = new List<PassiveBase>();

        public AbilityResult(Entity rootEntity)
        {
            RootEntity = rootEntity;
        }
    }
}