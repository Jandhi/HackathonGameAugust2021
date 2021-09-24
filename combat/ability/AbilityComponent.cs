using Game.Combat.Action;
using System.Collections.Generic;

namespace Game.Combat.Ability
{
    public abstract class AbilityComponent
    {
        public abstract void Execute(int seed, int depth, Combat combat, ActionRoot root, Entity caster, List<Entity> targets);
    }
}