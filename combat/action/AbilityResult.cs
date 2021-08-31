using System.Collections.Generic;
using Game.UI;

namespace Game.Combat.Action
{
    public class AbilityResult : CompositeCombatAction
    {
        public Entity RootEntity { get; }
        public List<string> PassiveMessages { get; } = new List<string>();

        public AbilityResult(Entity rootEntity)
        {
            RootEntity = rootEntity;
        }
    }
}